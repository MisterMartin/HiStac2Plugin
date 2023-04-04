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
                    double STAC1_300,
                    double STAC1_300_500,
                    double STAC1_500_700,
                    double STAC1_700_1000,
                    double STAC1_1000_3000,
                    double STAC1_Pump1,
                    double STAC1_Pump2,
                    double STAC1_Pump1_2Temp,
                    double STAC1_VBat,
                    double STAC1_SatTemp,
                    double STAC1_IceJacketTemp,
                    double STAC1_AltStatus,
                    double STAC2_300,
                    double STAC2_300_500,
                    double STAC2_500_700,
                    double STAC2_700_1000,
                    double STAC2_1000_3000,
                    double STAC2_Pump1,
                    double STAC2_Pump2,
                    double STAC2_Pump1_2Temp,
                    double STAC2_VBat,
                    double STAC2_SatTemp,
                    double STAC2_CondenserTemp,
                    double HI_UpstreamTemp,
                    double HI_MidstreamTemp,
                    double HI_DownstreamTemp,
                    double HI_VBat
                    );

        public void UpdateData(
                    double STAC1_300,
                    double STAC1_300_500,
                    double STAC1_500_700,
                    double STAC1_700_1000,
                    double STAC1_1000_3000,
                    double STAC1_Pump1,
                    double STAC1_Pump2,
                    double STAC1_Pump1_2Temp,
                    double STAC1_VBat,
                    double STAC1_SatTemp,
                    double STAC1_IceJacketTemp,
                    double STAC1_AltStatus,
                    double STAC2_300,
                    double STAC2_300_500,
                    double STAC2_500_700,
                    double STAC2_700_1000,
                    double STAC2_1000_3000,
                    double STAC2_Pump1,
                    double STAC2_Pump2,
                    double STAC2_Pump1_2Temp,
                    double STAC2_VBat,
                    double STAC2_SatTemp,
                    double STAC2_CondenserTemp,
                    double HI_UpstreamTemp,
                    double HI_MidstreamTemp,
                    double HI_DownstreamTemp,
                    double HI_VBat
                    )
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateDataDelegate(UpdateData), new object[] {
                                    STAC1_300,
                                    STAC1_300_500,
                                    STAC1_500_700,
                                    STAC1_700_1000,
                                    STAC1_1000_3000,
                                    STAC1_Pump1,
                                    STAC1_Pump2,
                                    STAC1_Pump1_2Temp,
                                    STAC1_VBat,
                                    STAC1_SatTemp,
                                    STAC1_IceJacketTemp,
                                    STAC1_AltStatus,
                                    STAC2_300,
                                    STAC2_300_500,
                                    STAC2_500_700,
                                    STAC2_700_1000,
                                    STAC2_1000_3000,
                                    STAC2_Pump1,
                                    STAC2_Pump2,
                                    STAC2_Pump1_2Temp,
                                    STAC2_VBat,
                                    STAC2_SatTemp,
                                    STAC2_CondenserTemp,
                                    HI_UpstreamTemp,
                                    HI_MidstreamTemp,
                                    HI_DownstreamTemp,
                                    HI_VBat});
            }
            else
            {
                STAC1_300Label.Text = string.Format("{0:0.00} [deg C]", STAC1_300);
                STAC1_300_500Label.Text = string.Format("{0:0.00} [deg C]", STAC1_300_500);
                STAC1_500_700Label.Text = string.Format("{0:0.00} [deg C]", STAC1_500_700);
                STAC1_700_1000Label.Text = string.Format("{0:0.00} [deg C]", STAC1_700_1000);
                STAC1_1000_3000Label.Text = string.Format("{0:0.00} [deg C]", STAC1_1000_3000);
                STAC1_Pump1CurrentLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_Pump1);
                STAC1_Pump2CurrentLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_Pump2);
                STAC1_Pump1_2TempLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_Pump1_2Temp);
                STAC1_VBatLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_VBat);
                STAC1_SaturatorTempLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_SatTemp);
                STAC1_IceJacketTempLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_IceJacketTemp);
                STAC1_AltSourceLabel.Text = string.Format("{0:0.00} [deg C]", STAC1_AltStatus);

                STAC2_300Label.Text = string.Format("{0:0.00} [deg C]", STAC2_300);
                STAC2_300_500Label.Text = string.Format("{0:0.00} [deg C]", STAC2_300_500);
                STAC2_500_700Label.Text = string.Format("{0:0.00} [deg C]", STAC2_500_700);
                STAC2_700_1000Label.Text = string.Format("{0:0.00} [deg C]", STAC2_700_1000);
                STAC2_1000_3000Label.Text = string.Format("{0:0.00} [deg C]", STAC2_1000_3000);
                STAC2_Pump1CurrentLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_Pump1);
                STAC2_Pump2CurrentLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_Pump2);
                STAC2_Pump1_2TempLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_Pump1_2Temp);
                STAC2_VBatLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_VBat);
                STAC2_SaturatorTempLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_SatTemp);
                STAC2_CondenserTempLabel.Text = string.Format("{0:0.00} [deg C]", STAC2_CondenserTemp);

                HI_UpstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_UpstreamTemp);
                HI_MidstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_MidstreamTemp);
                HI_DownstreamTempLabel.Text = string.Format("{0:0.00} [deg C]", HI_DownstreamTemp);
                HI_BatVLabel.Text = string.Format("{0:0.00} [deg C]", HI_VBat);
            }
        }
    }
}
