namespace HiStack2Plugin
{
    partial class HiStack2Config
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.multipleInstrumentsControl1 = new PluginReference.MultipleInstrumentsControl();
            this.label1 = new System.Windows.Forms.Label();
            this.Stac1SerialNumTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Stac2SerialNumberTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Stac1FlowRateTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Stac2FlowRateTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Stac2FlowRateTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Stac1FlowRateTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Stac2SerialNumberTextBox);
            this.groupBox1.Controls.Add(this.multipleInstrumentsControl1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Stac1SerialNumTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(670, 595);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HiStack2 Configuration";
            // 
            // multipleInstrumentsControl1
            // 
            this.multipleInstrumentsControl1.DaisyChainIndex = 0;
            this.multipleInstrumentsControl1.Location = new System.Drawing.Point(6, 319);
            this.multipleInstrumentsControl1.Margin = new System.Windows.Forms.Padding(12);
            this.multipleInstrumentsControl1.Name = "multipleInstrumentsControl1";
            this.multipleInstrumentsControl1.Size = new System.Drawing.Size(507, 102);
            this.multipleInstrumentsControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "STAC1 Serial Number:";
            // 
            // Stac1SerialNumTextBox
            // 
            this.Stac1SerialNumTextBox.Location = new System.Drawing.Point(288, 45);
            this.Stac1SerialNumTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.Stac1SerialNumTextBox.Name = "Stac1SerialNumTextBox";
            this.Stac1SerialNumTextBox.Size = new System.Drawing.Size(238, 31);
            this.Stac1SerialNumTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 170);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "STAC2 Serial Number:";
            // 
            // Stac2SerialNumberTextBox
            // 
            this.Stac2SerialNumberTextBox.Location = new System.Drawing.Point(288, 167);
            this.Stac2SerialNumberTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.Stac2SerialNumberTextBox.Name = "Stac2SerialNumberTextBox";
            this.Stac2SerialNumberTextBox.Size = new System.Drawing.Size(238, 31);
            this.Stac2SerialNumberTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "STAC1 Flow Rate [LPM]";
            // 
            // Stac1FlowRateTextBox
            // 
            this.Stac1FlowRateTextBox.Location = new System.Drawing.Point(288, 96);
            this.Stac1FlowRateTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.Stac1FlowRateTextBox.Name = "Stac1FlowRateTextBox";
            this.Stac1FlowRateTextBox.Size = new System.Drawing.Size(238, 31);
            this.Stac1FlowRateTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 221);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(244, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "STAC2 Flow Rate [LPM]";
            // 
            // Stac2FlowRateTextBox
            // 
            this.Stac2FlowRateTextBox.Location = new System.Drawing.Point(288, 218);
            this.Stac2FlowRateTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.Stac2FlowRateTextBox.Name = "Stac2FlowRateTextBox";
            this.Stac2FlowRateTextBox.Size = new System.Drawing.Size(238, 31);
            this.Stac2FlowRateTextBox.TabIndex = 7;
            // 
            // HiStack2Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "HiStack2Config";
            this.Size = new System.Drawing.Size(682, 607);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Stac1SerialNumTextBox;
        private PluginReference.MultipleInstrumentsControl multipleInstrumentsControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Stac2SerialNumberTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Stac1FlowRateTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Stac2FlowRateTextBox;
    }
}
