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
    public partial class HiStack2Config : UserControl
    {
        public int DaisyChainIndex
        {
            get { return multipleInstrumentsControl1.DaisyChainIndex; }
            set { multipleInstrumentsControl1.DaisyChainIndex = value; }
        }

        public string Stac1SerialNumber
        {
            get { return Stac1SerialNumTextBox.Text; }
            set { Stac1SerialNumTextBox.Text = value; }
        }

        public string Stac2SerialNumber
        {
            get { return Stac2SerialNumberTextBox.Text; }
            set { Stac2SerialNumberTextBox.Text = value; }
        }

        public double Stac1FlowRate
        {
            get { return double.Parse(Stac1FlowRateTextBox.Text); }
            set { Stac1FlowRateTextBox.Text = Stac1FlowRateTextBox.Text; }
        }

        public double Stac2FlowRate
        {
            get { return double.Parse(Stac2FlowRateTextBox.Text); }
            set { Stac2FlowRateTextBox.Text = Stac2FlowRateTextBox.Text; }
        }

        public HiStack2Config()
        {
            InitializeComponent();
        }
    }
}
