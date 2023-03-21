namespace StacHiPlugin
{
    partial class StacHiConfig
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
            this.serialNumberTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.multipleInstrumentsControl1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serialNumberTextBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(608, 305);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "StacHi Configuration";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // multipleInstrumentsControl1
            // 
            this.multipleInstrumentsControl1.DaisyChainIndex = 0;
            this.multipleInstrumentsControl1.Location = new System.Drawing.Point(18, 83);
            this.multipleInstrumentsControl1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.multipleInstrumentsControl1.Name = "multipleInstrumentsControl1";
            this.multipleInstrumentsControl1.Size = new System.Drawing.Size(576, 102);
            this.multipleInstrumentsControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial Number:";
            // 
            // serialNumberTextBox
            // 
            this.serialNumberTextBox.Location = new System.Drawing.Point(176, 33);
            this.serialNumberTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.serialNumberTextBox.Name = "serialNumberTextBox";
            this.serialNumberTextBox.Size = new System.Drawing.Size(238, 31);
            this.serialNumberTextBox.TabIndex = 0;
            // 
            // StacHiConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "StacHiConfig";
            this.Size = new System.Drawing.Size(620, 200);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serialNumberTextBox;
        private PluginReference.MultipleInstrumentsControl multipleInstrumentsControl1;
    }
}
