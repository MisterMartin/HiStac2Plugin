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

        public string SerialNumber
        {
            get { return serialNumberTextBox.Text; }
            set { serialNumberTextBox.Text = value; }
        }

        public HiStack2Config()
        {
            InitializeComponent();
        }
    }
}
