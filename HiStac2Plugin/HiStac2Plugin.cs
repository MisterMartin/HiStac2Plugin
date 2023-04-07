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
        private int daisyChainIndex;

        private string Stac1SerialNumber;
        private double Stac1FlowRate;
        private string Stac2SerialNumber;
        private double Stac2FlowRate;

        private HiStac2DataView dataViewControl;
        private Panel dataViewPanel = null;

        // The decoded parameters
        private double STAC1_300 = 0.0;
        private double STAC1_500 = 0.0;
        private double STAC1_700 = 0.0;
        private double STAC1_1000 = 0.0;
        private double STAC1_3000 = 0.0;
        private double STAC1_Pump1 = 0.0;
        private double STAC1_Pump2 = 0.0;
        private double STAC1_Pump1_2Temp = 0.0;
        private double STAC1_VBat = 0.0;
        private double STAC1_SatTemp = 0.0;
        private double STAC1_IceJacketTemp = 0.0;
        private int    STAC1_AltStatus = 0;

        private double STAC2_300 = 0.0;
        private double STAC2_500 = 0.0;
        private double STAC2_700 = 0.0;
        private double STAC2_1000 = 0.0;
        private double STAC2_3000 = 0.0;
        private double STAC2_Pump1 = 0.0;
        private double STAC2_Pump2 = 0.0;
        private double STAC2_Pump1_2Temp = 0.0;
        private double STAC2_VBat = 0.0;
        private double STAC2_SatTemp = 0.0;

        private double STAC2_CondenserTemp = 0.0;
        private double HI_UpstreamTemp = 0.0;
        private double HI_MidstreamTemp = 0.0;
        private double HI_DownstreamTemp = 0.0;
        private double HI_VBat = 0.0;

        /**
         * The name of the plugin's instrument, to be shown in the GUI.  
         */
        override public string InstrumentName { get { return "HiStack2 Plugin v1.0"; } }

        /**
         * A sentence or two describing the instrument in more detail.  
         */
        override public string InstrumentDescription { get { return "STAC1, STAC2 and Heated Inlet"; } }

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
            Stac1SerialNumber = configControl.Stac1SerialNumber;
            Stac1FlowRate = configControl.Stac1FlowRate;
            Stac2SerialNumber = configControl.Stac2SerialNumber;
            Stac2FlowRate = configControl.Stac2FlowRate;
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
            xmlWriter.WriteElementString("stac1SerialNumber", Stac1SerialNumber);
            xmlWriter.WriteElementString("stac1FlowRate", Stac1FlowRate.ToString("F2"));
            xmlWriter.WriteElementString("stac2SerialNumber", Stac2SerialNumber);
            xmlWriter.WriteElementString("stac2FlowRate", Stac2FlowRate.ToString("F2"));
            xmlWriter.WriteElementString("daisyChainIndex", daisyChainIndex.ToString());
        }

        /**
         * For reprocessing flights, this method should parse the rawconfig xml file to restore any of the plugin's required metadata fields, and update the config GUI panel.  
         * This should also set the plugin's "Enabled" property if an appropriate xml field is located for the plugin's instrument.  
         */
        override public void ParseRawconfig(string filename)
        {
            int nMetaFields = 0;

            XDocument doc = XDocument.Load(filename);

            var elements = doc.Descendants("stac1SerialNumber");
            if (elements.Count() > 0)
            {
                Stac1SerialNumber = elements.First().Value;
                //also update the GUI if possible
                if (configControl != null) configControl.Stac1SerialNumber = Stac1SerialNumber;
                nMetaFields++;
            }

            elements = doc.Descendants("stac1FlowRate");
            if (elements.Count() > 0)
            {
                Stac1FlowRate = double.Parse(elements.First().Value);
                //also update the GUI if possible
                if (configControl != null) configControl.Stac1FlowRate = Stac1FlowRate;
                nMetaFields++;
            }

            elements = doc.Descendants("stac2SerialNumber");
            if (elements.Count() > 0)
            {
                Stac2SerialNumber = elements.First().Value;
                //also update the GUI if possible
                if (configControl != null) configControl.Stac2SerialNumber = Stac2SerialNumber;
                nMetaFields++;

            }

            elements = doc.Descendants("stac2FlowRate");
            if (elements.Count() > 0)
            {
                Stac2FlowRate = double.Parse(elements.First().Value);
                //also update the GUI if possible
                if (configControl != null) configControl.Stac2FlowRate = Stac2FlowRate;
                nMetaFields++;
            }

            var daisyChainIndexElements = doc.Descendants("daisyChainIndex");
            daisyChainIndex = 0;
            if (daisyChainIndexElements.Count() > 0)
            {
                daisyChainIndex = int.Parse(daisyChainIndexElements.First().Value);
                //update the GUI
                if (configControl != null) configControl.DaisyChainIndex = daisyChainIndex;
                nMetaFields++;
            }

            if (nMetaFields == 5)
            {
                //enable this plugin since there is XML data available
                this.Enabled = true;
            }
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
            if (instrumentID != 0x41) return;

            // There is are 2 extra bytes at the end of the packet. 
            if (packet.Length != 76) return;

            //parse the packet's daisy chain index, in case of duplicate instruments
            byte packetDaisyChainIndex = Byte.Parse(packet.Substring(8, 2), System.Globalization.NumberStyles.HexNumber);
            //if the user selected a daisy chain index other than "Any", there are multiple duplicate instruments so skip any packets that don't match our daisy chain index
            if (daisyChainIndex > 0)
            {
                if (packetDaisyChainIndex != daisyChainIndex) return;
            }

            int STAC1_300Int           = PluginHelper.IntFromMSBHexString(packet.Substring(10, 4));
            int STAC1_300_500Int       = PluginHelper.IntFromMSBHexString(packet.Substring(14, 2));
            int STAC1_500_700Int       = PluginHelper.IntFromMSBHexString(packet.Substring(16, 2));
            int STAC1_700_1000Int      = PluginHelper.IntFromMSBHexString(packet.Substring(18, 2));
            int STAC1_1000_3000Int     = PluginHelper.IntFromMSBHexString(packet.Substring(20, 2));
            int STAC1_Pump1Int         = PluginHelper.IntFromMSBHexString(packet.Substring(22, 2));
            int STAC1_Pump2Int         = PluginHelper.IntFromMSBHexString(packet.Substring(24, 2));
            int STAC1_Pump1_2TempInt   = PluginHelper.IntFromMSBHexString(packet.Substring(26, 2));
            int STAC1_VBatInt          = PluginHelper.IntFromMSBHexString(packet.Substring(28, 2));
            int STAC1_SatTempInt       = PluginHelper.IntFromMSBHexString(packet.Substring(30, 4));
            int STAC1_IceJacketTempInt = PluginHelper.IntFromMSBHexString(packet.Substring(34, 2));
            int STAC1_AltStatusInt     = PluginHelper.IntFromMSBHexString(packet.Substring(36, 2));

            int STAC2_300Int           = PluginHelper.IntFromMSBHexString(packet.Substring(38, 4));
            int STAC2_300_500Int       = PluginHelper.IntFromMSBHexString(packet.Substring(42, 2));
            int STAC2_500_700Int       = PluginHelper.IntFromMSBHexString(packet.Substring(44, 2));
            int STAC2_700_1000Int      = PluginHelper.IntFromMSBHexString(packet.Substring(46, 2));
            int STAC2_1000_3000Int     = PluginHelper.IntFromMSBHexString(packet.Substring(48, 2));
            int STAC2_Pump1Int         = PluginHelper.IntFromMSBHexString(packet.Substring(50, 2));
            int STAC2_Pump2Int         = PluginHelper.IntFromMSBHexString(packet.Substring(52, 2));
            int STAC2_Pump1_2TempInt   = PluginHelper.IntFromMSBHexString(packet.Substring(54, 2));
            int STAC2_VBatInt          = PluginHelper.IntFromMSBHexString(packet.Substring(56, 2));
            int STAC2_SatTempInt       = PluginHelper.IntFromMSBHexString(packet.Substring(58, 4));
            int STAC2_CondenserTempInt = PluginHelper.IntFromMSBHexString(packet.Substring(62, 2));

            int HI_UpstreamTempInt   = PluginHelper.IntFromMSBHexString(packet.Substring(64, 2));
            int HI_MidstreamTempInt  = PluginHelper.IntFromMSBHexString(packet.Substring(66, 2));
            int HI_DownstreamTempInt = PluginHelper.IntFromMSBHexString(packet.Substring(68, 2));
            int HI_VBatInt           = PluginHelper.IntFromMSBHexString(packet.Substring(70, 2));

            STAC1_300 = STAC1_300Int / (Stac1FlowRate * 1000.0 / 30.0);
            STAC1_500 = STAC1_300  -  STAC1_300_500Int/(Stac1FlowRate*1000.0/30.0);
            STAC1_700  = STAC1_500  -  STAC1_500_700Int/(Stac1FlowRate*1000.0/30.0);
            STAC1_1000 = STAC1_700  -  STAC1_700_1000Int/(Stac1FlowRate*1000.0/30.0);
            STAC1_3000 = STAC1_1000 - STAC1_1000_3000Int/(Stac1FlowRate * 1000.0 / 30.0);
            STAC1_Pump1 = STAC1_Pump1Int;
            STAC1_Pump2 = STAC1_Pump2Int;
            STAC1_Pump1_2Temp = STAC1_Pump1_2TempInt - 100.0;
            STAC1_VBat = STAC1_VBatInt/10.0;
            STAC1_SatTemp = STAC1_SatTempInt/100.0;
            STAC1_IceJacketTemp = STAC1_IceJacketTempInt/10.0 - 10.0;
            STAC1_AltStatus = STAC1_AltStatusInt;

            STAC2_300  =                   STAC2_300Int/(Stac2FlowRate*1000.0/30.0);
            STAC2_500  = STAC2_300  -  STAC2_300_500Int/(Stac2FlowRate*1000.0/30.0);
            STAC2_700  = STAC2_500  -  STAC2_500_700Int/(Stac2FlowRate*1000.0/30.0);
            STAC2_1000 = STAC2_700  -  STAC2_700_1000Int/(Stac2FlowRate*1000.0/30.0);
            STAC2_3000 = STAC2_1000 - STAC2_1000_3000Int/(Stac2FlowRate*1000.0/30.0);
            STAC2_Pump1 = STAC2_Pump1Int;
            STAC2_Pump2 = STAC2_Pump2Int;
            STAC2_Pump1_2Temp = STAC2_Pump1_2TempInt - 100.0;
            STAC2_VBat = STAC2_VBatInt/10.0;
            STAC2_SatTemp = STAC2_SatTempInt/100.0;
            STAC2_CondenserTemp = (STAC2_CondenserTempInt/10.0) - 10.0;
            HI_UpstreamTemp = HI_UpstreamTempInt;
            HI_MidstreamTemp = HI_MidstreamTempInt;
            HI_DownstreamTemp = HI_DownstreamTempInt;
            HI_VBat = HI_VBatInt/10.0;


            //update the GUI using the data view control
            dataViewControl.UpdateData(
                STAC1_300,
                STAC1_500,
                STAC1_700,
                STAC1_1000,
                STAC1_3000,
                STAC1_Pump1,
                STAC1_Pump2,
                STAC1_Pump1_2Temp,
                STAC1_VBat,
                STAC1_SatTemp,
                STAC1_IceJacketTemp,
                STAC1_AltStatus,
                STAC2_300,
                STAC2_500,
                STAC2_700,
                STAC2_1000,
                STAC2_3000,
                STAC2_Pump1,
                STAC2_Pump2,
                STAC2_Pump1_2Temp,
                STAC2_VBat,
                STAC2_SatTemp,
                STAC2_CondenserTemp,
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
            return
                string.Format("STAC1 Serial Number:, {0}\n", Stac1SerialNumber) +
                string.Format("STAC1 Flow Rate:, {0:0.00} [LPM]\n", Stac1FlowRate) +
                string.Format("STAC2 Serial Number:, {0}\n", Stac2SerialNumber) +
                string.Format("STAC2 Flow Rate:, {0:0.00} [LPM]", Stac2FlowRate);
        }

        /**
         * Output CSV header field names for the plugin's output fields.  Please include units in square brackets at the end of the name.  
         * Always start with a leading comma, and finish without a comma.  Example:
         * , fieldname1 [units1], fieldname2 [units2]
         */
        override public string OutputCSVHeaderSegment()
        {
            return ", STAC1_300 [#/cc], STAC1_500 [#/cc], STAC1_700 [#/cc], STAC1_1000 [#/cc], STAC1_3000 [#/cc], STAC1_Pump1 [mA], STAC1_Pump2 [mA], STAC1_Pump1_2T [deg C], STAC1_VBat [V], STAC1_SatT [deg C], STAC1_IceJacketT [deg C], STAC1_AltStatus [n], STAC2_300 [#/cc], STAC2_500 [#/cc], STAC2_700 [#/cc], STAC2_1000 [#/cc], STAC2_3000 [#/cc], STAC2_Pump1 [mA], STAC2_Pump2 [mA], STAC2_Pump1_2T [deg C], STAC2_VBat [V], STAC2_SatT [deg C], STAC2_CondenserT [deg C], HI_UpstreamT [deg C], HI_MidstreamT [deg C], HI_DownstreamT [deg C], HI_VBat [V]";
        }

        /**
         * Output the plugin's data matching the supplied UTC date time in a partial CSV row.  
         * The resulting string should be comma separated and begin with a comma, like this:
         * ", data1, data2, data3"
         */
        override public string OutputCSVRowSegment(DateTime dateTimeUTC, RadiosondeFields radiosondeFields)
        {
            return
                string.Format(", {0:0.00}", STAC1_300) +
                string.Format(", {0:0.00}", STAC1_500) +
                string.Format(", {0:0.00}", STAC1_700) +
                string.Format(", {0:0.00}", STAC1_1000) +
                string.Format(", {0:0.00}", STAC1_3000) +
                string.Format(", {0:0.00}", STAC1_Pump1) +
                string.Format(", {0:0.00}", STAC1_Pump2) +
                string.Format(", {0:0.00}", STAC1_Pump1_2Temp) +
                string.Format(", {0:0.00}", STAC1_VBat) +
                string.Format(", {0:0.00}", STAC1_SatTemp) +
                string.Format(", {0:0.00}", STAC1_IceJacketTemp) +
                string.Format(", {0:0.00}", STAC1_AltStatus) +
                string.Format(", {0:0.00}", STAC2_300) +
                string.Format(", {0:0.00}", STAC2_500) +
                string.Format(", {0:0.00}", STAC2_700) +
                string.Format(", {0:0.00}", STAC2_1000) +
                string.Format(", {0:0.00}", STAC2_3000) +
                string.Format(", {0:0.00}", STAC2_Pump1) +
                string.Format(", {0:0.00}", STAC2_Pump2) +
                string.Format(", {0:0.00}", STAC2_Pump1_2Temp) +
                string.Format(", {0:0.00}", STAC2_VBat) +
                string.Format(", {0:0.00}", STAC2_SatTemp) +
                string.Format(", {0:0.00}", STAC2_CondenserTemp) +
                string.Format(", {0:0.00}", HI_UpstreamTemp) +
                string.Format(", {0:0.00}", HI_MidstreamTemp) +
                string.Format(", {0:0.00}", HI_DownstreamTemp) +
                string.Format(", {0:0.00}", HI_VBat);
        }
    }
}
