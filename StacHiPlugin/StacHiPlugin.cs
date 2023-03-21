using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using PluginReference;

namespace StacHiPlugin
{
    public class StacHiPlugin : PluginBase
    {
        private StacHiConfig configControl;
        private Panel configPanel = null;
        private string serialNumber;
        private int daisyChainIndex;

        private StacHiDataView dataViewControl;
        private Panel dataViewPanel = null;
        private double temperature, pressure;

        /**
         * The name of the plugin's instrument, to be shown in the GUI.  
         */
        override public string InstrumentName { get { return "StacHi"; } }

        /**
         * A sentence or two describing the instrument in more detail.  
         */
        override public string InstrumentDescription { get { return "STAC with Heated Inlet"; } }

        /**
         * Create and return a Windows Forms Panel containing any setup/configuration/metadata controls required by the plugin instrument.  
         */
        override public Panel GetConfigPanel()
        {
            if (configPanel == null)
            {
                configPanel = new Panel();
                configPanel.AutoSize = true;

                configControl = new StacHiConfig();
                configPanel.Controls.Add(configControl);
            }

            return configPanel;
        }

        /**
         * After the user has finished entering config values in the GUI and pressed "OK", this method will be called.  
         * The plugin should parse its own config panel controls and store the results.  
         * 
         * @param selectedDaisyChainIndex       The user-selected daisy chain index for this instrument in case multiple duplicate instruments are attached.  
         *                                      A value of 0 means to allow all daisy chain indices, no particular instrument has been selected so there is only one attached.  
         */
        override public void ParseConfigPanel()
        {
            serialNumber = configControl.SerialNumber;
            daisyChainIndex = configControl.DaisyChainIndex;
        }

        /**
         * Create and return a Windows Forms Panel for displaying real-time data from the plugin's instrument.  The ParsePacket method should update the panel's controls as data comes in.  
         */
        override public Panel GetDataViewPanel()
        {
            if (dataViewPanel == null)
            {
                dataViewPanel = new Panel();
                dataViewPanel.Dock = DockStyle.Fill;

                dataViewControl = new StacHiDataView();

                dataViewPanel.Controls.Add(dataViewControl);
                dataViewPanel.Size = dataViewControl.Size;//this line has been added to make the auto sizing work
            }

            return dataViewPanel;
        }

        /**
         * Output XML elements using XmlTextWriter's WriteElementString method for saving any of the plugin instrument's metadata fields in the flight's rawconfig file.  
         * 
         * Do not output any parent/surrounding elements, this will be handled by the calling code.  
         */
        override public void OutputRawconfigXML(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteElementString("StacHiSerialNumber", serialNumber);
            xmlWriter.WriteElementString("StacHiDaisyChainIndex", daisyChainIndex.ToString());
        }

        /**
         * For reprocessing flights, this method should parse the rawconfig xml file to restore any of the plugin's required metadata fields, and update the config GUI panel.  
         * This should also set the plugin's "Enabled" property if an appropriate xml field is located for the plugin's instrument.  
         */
        override public void ParseRawconfig(string filename)
        {
            XDocument doc = XDocument.Load(filename);

            var serialNumberElements = doc.Descendants("StacHiSerialNumber");
            if (serialNumberElements.Count() > 0)
            {
                serialNumber = serialNumberElements.First().Value;
                //also update the GUI if possible
                if (configControl != null) configControl.SerialNumber = serialNumber;

                //enable this plugin since there is XML data available
                this.Enabled = true;
            }

            var daisyChainIndexElements = doc.Descendants("StacHiDaisyChainIndex");
            daisyChainIndex = 0;
            if (daisyChainIndexElements.Count() > 0)
            {
                daisyChainIndex = int.Parse(daisyChainIndexElements.First().Value);
            }
            //update the GUI
            if (configControl != null) configControl.DaisyChainIndex = daisyChainIndex;
        }

        /**
         * Parse a real-time data packet if it matches the plugin's expected format, and display th e
         * @param packet    The ascii hexadecimal packet as received by SkySonde Server.  
         * @param dateTimeUTC   The UTC date/time (from the computer's system clock) when the packet was received by SkySonde Client.  
         * 
         * This could be (but currently isn't) called from an alternate thread, so maybe use BeginInvoke when updating the data view GUI panel.  
         */
        override public void ParsePacket(string packet, DateTime dateTimeUTC)
        {
            //check the XDATA instrument ID to see if this is our instrument's data packet
            byte instrumentID = Byte.Parse(packet.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            if (instrumentID != 0x11) return;
            if (packet.Length != 36) return;

            //parse the packet's daisy chain index, in case of duplicate instruments
            byte packetDaisyChainIndex = Byte.Parse(packet.Substring(8, 2), System.Globalization.NumberStyles.HexNumber);
            //if the user selected a daisy chain index other than "Any", there are multiple duplicate instruments so skip any packets that don't match our daisy chain index
            if (daisyChainIndex > 0)
            {
                if (packetDaisyChainIndex != daisyChainIndex) return;
            }

            //parse the pressure and temperature values from the XDATA instrument packet
            int pressureInt = PluginHelper.IntFromMSBHexString(packet.Substring(10, 8));
            Int16 ptempInt = (Int16)PluginHelper.IntFromMSBHexString(packet.Substring(10 + 8, 4));
            pressure = ((double)pressureInt) / 100;
            temperature = ((double)ptempInt) / 100;

            //update the GUI using the data view control
            dataViewControl.UpdateData(temperature, pressure);
        }

        /**
         * Output instrument metadata lines to the top of the CSV file in the format:
         * name1:, value1
         * name2:, value2
         */
        override public string OutputCSVMetadataLines()
        {
            return string.Format("StacHi Serial Number:, {0}", serialNumber);
        }

        /**
         * Output CSV header field names for the plugin's output fields.  Please include units in square brackets at the end of the name.  Always start with a leading comma, and finish without a comma.  Example:
         * , fieldname1 [units1], fieldname2 [units2]
         */
        override public string OutputCSVHeaderSegment()
        {
            return ", StacHi Temperature [deg C], StacHi Pressure [mb]";
        }

        /**
         * Output the plugin's data matching the supplied UTC date time in a partial CSV row.  
         * The resulting string should be comma separated and begin with a comma, like this:
         * ", data1, data2, data3"
         */
        override public string OutputCSVRowSegment(DateTime dateTimeUTC, RadiosondeFields radiosondeFields)
        {
            return string.Format(", {0:0.00}, {1:0.00}", temperature, pressure);
        }
    }
}
