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

        private delegate void UpdateDataDelegate(double temperature, double pressure);

        public void UpdateData(double temperature, double pressure)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateDataDelegate(UpdateData), new object[] { temperature, pressure });
            }
            else
            {
                temperatureLabel.Text = string.Format("{0:0.00} [deg C]", temperature);
                pressureLabel.Text = string.Format("{0:0.00} [mb]", pressure);
            }
        }
    }
}
