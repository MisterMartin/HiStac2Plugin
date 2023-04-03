using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiStack2Plugin
{
    public partial class HiStac2DataView : UserControl
    {
        public HiStac2DataView()
        {
            InitializeComponent();
        }

        private delegate void UpdateDataDelegate(
                    double OPC_300,
                    double OPC_300_500,
                    double OPC_500_700,
                    double OPC_700_1000,
                    double OPC_1000_3000,
                    double OPC_Pump1,
                    double OPC_Pump2,
                    double OPC_Pump1_2Temp,
                    double OPC_VBat,
                    double OPC_SatTemp,
                    double OPC_IceJacketTemp,
                    double OPC_AltStatus,
                    double CNC_300,
                    double CNC_300_500,
                    double CNC_500_700,
                    double CNC_700_1000,
                    double CNC_1000_3000,
                    double CNC_Pump1,
                    double CNC_Pump2,
                    double CNC_Pump1_2Temp,
                    double CNC_VBat,
                    double CNC_SatTemp,
                    double CNC_CondenserTemp,
                    double HI_UpstreamTemp,
                    double HI_MidstreamTemp,
                    double HI_DownstreamTemp,
                    double HI_VBat
                    );

        public void UpdateData(
                    double OPC_300,
                    double OPC_300_500,
                    double OPC_500_700,
                    double OPC_700_1000,
                    double OPC_1000_3000,
                    double OPC_Pump1,
                    double OPC_Pump2,
                    double OPC_Pump1_2Temp,
                    double OPC_VBat,
                    double OPC_SatTemp,
                    double OPC_IceJacketTemp,
                    double OPC_AltStatus,
                    double CNC_300,
                    double CNC_300_500,
                    double CNC_500_700,
                    double CNC_700_1000,
                    double CNC_1000_3000,
                    double CNC_Pump1,
                    double CNC_Pump2,
                    double CNC_Pump1_2Temp,
                    double CNC_VBat,
                    double CNC_SatTemp,
                    double CNC_CondenserTemp,
                    double HI_UpstreamTemp,
                    double HI_MidstreamTemp,
                    double HI_DownstreamTemp,
                    double HI_VBat
                    )
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateDataDelegate(UpdateData), new object[] {
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
                                    HI_VBat});
            }
            else
            {
                OPC_300Label.Text = string.Format("{0:0.00} [deg C]", OPC_300);
                OPC_300_500Label.Text = string.Format("{0:0.00} [deg C]", OPC_300_500);
                OPC_500_700Label.Text = string.Format("{0:0.00} [deg C]", OPC_500_700);
                OPC_700_1000Label.Text = string.Format("{0:0.00} [deg C]", OPC_700_1000);
                OPC_1000_3000Label.Text = string.Format("{0:0.00} [deg C]", OPC_1000_3000);
                OPC_Pump1CurrentLabel.Text = string.Format("{0:0.00} [deg C]", OPC_Pump1);
                OPC_Pump2CurrentLabel.Text = string.Format("{0:0.00} [deg C]", OPC_Pump2);
                OPC_Pump1_2TempLabel.Text = string.Format("{0:0.00} [deg C]", OPC_Pump1_2Temp);
                OPC_VBatLabel.Text = string.Format("{0:0.00} [deg C]", OPC_VBat);
                OPC_SaturatorTempLabel.Text = string.Format("{0:0.00} [deg C]", OPC_SatTemp);
                OPC_IceJacketTempLabel.Text = string.Format("{0:0.00} [deg C]", OPC_IceJacketTemp);
                OPC_AltSourceLabel.Text = string.Format("{0:0.00} [deg C]", OPC_AltStatus);

                CNC_300Label.Text = string.Format("{0:0.00} [deg C]", CNC_300);
                CNC_300_500Label.Text = string.Format("{0:0.00} [deg C]", CNC_300_500);
                CNC_500_700Label.Text = string.Format("{0:0.00} [deg C]", CNC_500_700);
                CNC_700_1000Label.Text = string.Format("{0:0.00} [deg C]", CNC_700_1000);
                CNC_1000_3000Label.Text = string.Format("{0:0.00} [deg C]", CNC_1000_3000);
                CNC_Pump1CurrentLabel.Text = string.Format("{0:0.00} [deg C]", CNC_Pump1);
                CNC_Pump2CurrentLabel.Text = string.Format("{0:0.00} [deg C]", CNC_Pump2);
                CNC_Pump1_2TempLabel.Text = string.Format("{0:0.00} [deg C]", CNC_Pump1_2Temp);
                CNC_VBatLabel.Text = string.Format("{0:0.00} [deg C]", CNC_VBat);
                CNC_SaturatorTempLabel.Text = string.Format("{0:0.00} [deg C]", CNC_SatTemp);
                CNC_CondenserTempLabel.Text = string.Format("{0:0.00} [deg C]", CNC_CondenserTemp);

                HI_UpstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_UpstreamTemp);
                HI_MidstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_MidstreamTemp);
                HI_DownstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_DownstreamTemp);
                HI_BatVLabel.Text = string.Format("{0:0.00} [deg C]", HI_VBat);
            }
        }
    }
}
