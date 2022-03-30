
namespace EliteFlower
{
    partial class Checker
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Checker));
            this.serialChecker = new System.IO.Ports.SerialPort(this.components);
            this.rtxtSerial = new System.Windows.Forms.RichTextBox();
            this.btnSerial = new System.Windows.Forms.Button();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.helpProv = new System.Windows.Forms.HelpProvider();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialChecker
            // 
            this.serialChecker.BaudRate = 115200;
            this.serialChecker.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialChecker_DataReceived);
            // 
            // rtxtSerial
            // 
            this.rtxtSerial.Location = new System.Drawing.Point(45, 98);
            this.rtxtSerial.Name = "rtxtSerial";
            this.rtxtSerial.ReadOnly = true;
            this.rtxtSerial.Size = new System.Drawing.Size(368, 78);
            this.rtxtSerial.TabIndex = 5;
            this.rtxtSerial.Text = "";
            this.rtxtSerial.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.rtxtSerial_HelpRequested);
            // 
            // btnSerial
            // 
            this.btnSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerial.Location = new System.Drawing.Point(198, 47);
            this.btnSerial.Name = "btnSerial";
            this.btnSerial.Size = new System.Drawing.Size(115, 30);
            this.btnSerial.TabIndex = 4;
            this.btnSerial.Text = "Connect";
            this.btnSerial.UseVisualStyleBackColor = true;
            this.btnSerial.Click += new System.EventHandler(this.btnSerial_Click);
            this.btnSerial.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnSerial_HelpRequested);
            // 
            // cbSerial
            // 
            this.cbSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(45, 49);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(141, 28);
            this.cbSerial.TabIndex = 3;
            this.cbSerial.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbSerial_HelpRequested);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(325, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Checker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 231);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtxtSerial);
            this.Controls.Add(this.btnSerial);
            this.Controls.Add(this.cbSerial);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Checker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Barcode Scanner";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Checker_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Checker_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Checker_FormClosed);
            this.Load += new System.EventHandler(this.Checker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialChecker;
        private System.Windows.Forms.RichTextBox rtxtSerial;
        private System.Windows.Forms.Button btnSerial;
        private System.Windows.Forms.ComboBox cbSerial;
        private System.Windows.Forms.HelpProvider helpProv;
        private System.Windows.Forms.Button button1;
    }
}