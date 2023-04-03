using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using PluginReference;

namespace HiStack2Plugin
{
    public class HiStac2Plugin : PluginBase
    {
        private HiStack2Config configControl;
        private Panel configPanel = null;
        private string serialNumber;
        private int daisyChainIndex;

        private HiStac2DataView dataViewControl;
        private Panel dataViewPanel = null;

        /**

        ** 1st STAC with reduced data
        OPC_300, 2);                                            //300nm Bin
        OPC_300 - OPC_500, 1);                                  //500nm difference
        OPC_500 - OPC_700, 1);                                  //700nm difference
        OPC_700 - OPC_1000, 1);                                 //1000nm difference
        OPC_1000 - OPC_3000, 1);                                //3000nm Bin
        byte(IPump1), 1);                                       //Pump1 current in mA
        byte(IPump2), 1);                                       //Pump1 current in mA
        byte((TempPump1 + TempPump2) / 2.0 + 100.0), 1);
        byte(VBat* 10.0), 1);                                   //Battery voltage *10
        short(TempCN* 100.0), 2);                               //Saturator T
        byte(TempIce + 10.0) * 10, 1);                          //Ice Jacket T
        byte(alt_status), 1);                                   //Status byte for altitude source

        ** 2nd STAC with reduced data 
        CNC_300, 2); //300nm Bin
        (CNC_300 - CNC_500), 1);                                //500nm Difference
        (CNC_500 - CNC_700), 1);                                //700nm Diffenence
        (CNC_700 - CNC_1000), 1);                               //1000nm Diffenence
        (CNC_1000 - CNC_3000), 1);                              //3000nm Diffenence
        byte(CN_IPump1), 1);                                    //Pump1 current in mA
        byte(CN_IPump2), 1);                                    //Pump1 current in mA
        byte((CN_TempPump1 + CN_TempPump2) / 2.0 + 100.0), 1);
        byte(CN_VBat), 1);                                      //Battery voltage *10
        short(CN_TempCN* 100.0), 2);                            //Saturator T
        byte((CN_TempIce + 10.0) * 10), 1);                     //Condenser T 1

        ** Heated Inlet
        byte(CN_T_UpStream), 1);
        byte(CN_T_Middle), 1);
        byte(CN_T_DownStream), 1);
        byte(CN_HI_V_Battery*10.0), 1);
        **/
        private double temperature, pressure;

        /**
         * The name of the plugin's instrument, to be shown in the GUI.  
         */
        override public string InstrumentName { get { return "HiStack2"; } }

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

                configControl = new HiStack2Config();
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

                dataViewControl = new HiStac2DataView();

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
            xmlWriter.WriteElementString("HiStack2SerialNumber", serialNumber);
            xmlWriter.WriteElementString("HiStack2DaisyChainIndex", daisyChainIndex.ToString());
        }

        /**
         * For reprocessing flights, this method should parse the rawconfig xml file to restore any of the plugin's required metadata fields, and update the config GUI panel.  
         * This should also set the plugin's "Enabled" property if an appropriate xml field is located for the plugin's instrument.  
         */
        override public void ParseRawconfig(string filename)
        {
            XDocument doc = XDocument.Load(filename);

            var serialNumberElements = doc.Descendants("HiStack2SerialNumber");
            if (serialNumberElements.Count() > 0)
            {
                serialNumber = serialNumberElements.First().Value;
                //also update the GUI if possible
                if (configControl != null) configControl.SerialNumber = serialNumber;

                //enable this plugin since there is XML data available
                this.Enabled = true;
            }

            var daisyChainIndexElements = doc.Descendants("HiStack2DaisyChainIndex");
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

            double OPC_300 = 0.0;
            double OPC_300_500 = 0.0;
            double OPC_500_700 = 0.0;
            double OPC_700_1000 = 0.0;
            double OPC_1000_3000 = 0.0;
            double OPC_Pump1 = 0.0;
            double OPC_Pump2 = 0.0;
            double OPC_Pump1_2Temp = 0.0;
            double OPC_VBat = 0.0;
            double OPC_SatTemp = 0.0;
            double OPC_IceJacketTemp = 0.0;
            double OPC_AltStatus = 0.0;
            double CNC_300 = 0.0;
            double CNC_300_500 = 0.0;
            double CNC_500_700 = 0.0;
            double CNC_700_1000 = 0.0;
            double CNC_1000_3000 = 0.0;
            double CNC_Pump1 = 0.0;
            double CNC_Pump2 = 0.0;
            double CNC_Pump1_2Temp = 0.0;
            double CNC_VBat = 0.0;
            double CNC_SatTemp = 0.0;
            double CNC_CondenserTemp = 0.0;
            double HI_UpstreamTemp = 0.0;
            double HI_MidstreamTemp = 0.0;
            double HI_DownstreamTemp = 0.0;
            double HI_VBat = 0.0;

            //update the GUI using the data view control
            dataViewControl.UpdateData(
                OPC_300,
                OPC_300_500,
                OPC_500_700,
                OPC_700_1000,
                OPC_1000_3000,
                OPC_Pump1,
                OPC_Pump2,
                OPC_Pump1_2Temp,
                OPC_VBat,
                OPC_SatTemp,
                OPC_IceJacketTemp,
                OPC_AltStatus,
                CNC_300,
                CNC_300_500,
                CNC_500_700,
                CNC_700_1000,
                CNC_1000_3000,
                CNC_Pump1,
                CNC_Pump2,
                CNC_Pump1_2Temp,
                CNC_VBat,
                CNC_SatTemp,
                CNC_CondenserTemp,
                HI_UpstreamTemp,
                HI_MidstreamTemp,
                HI_DownstreamTemp,
                HI_VBat);
        }

    /**
     * Output instrument metadata lines to the top of the CSV file in the format:
     * name1:, value1
     * name2:, value2
     */
    override public string OutputCSVMetadataLines()
        {
            return string.Format("HiStack2 Serial Number:, {0}", serialNumber);
        }

        /**
         * Output CSV header field names for the plugin's output fields.  Please include units in square brackets at the end of the name.  Always start with a leading comma, and finish without a comma.  Example:
         * , fieldname1 [units1], fieldname2 [units2]
         */
        override public string OutputCSVHeaderSegment()
        {
            return ", HiStack2 Temperature [deg C], HiStack2 Pressure [mb]";
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
