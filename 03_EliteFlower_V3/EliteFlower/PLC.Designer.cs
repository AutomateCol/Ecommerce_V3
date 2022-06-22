
namespace EliteFlower
{
    partial class PLC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLC));
            this.lblLeds = new System.Windows.Forms.Label();
            this.numMotor1 = new System.Windows.Forms.NumericUpDown();
            this.numMotor2 = new System.Windows.Forms.NumericUpDown();
            this.lblNumMotor1 = new System.Windows.Forms.Label();
            this.lblNumMotor2 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.numTimeLed = new System.Windows.Forms.NumericUpDown();
            this.gbTimer = new System.Windows.Forms.GroupBox();
            this.gbVelocity = new System.Windows.Forms.GroupBox();
            this.helpProv = new System.Windows.Forms.HelpProvider();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numPercentage2 = new System.Windows.Forms.NumericUpDown();
            this.numPercentage1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMotor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMotor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLed)).BeginInit();
            this.gbTimer.SuspendLayout();
            this.gbVelocity.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLeds
            // 
            this.lblLeds.AutoSize = true;
            this.lblLeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeds.Location = new System.Drawing.Point(15, 31);
            this.lblLeds.Name = "lblLeds";
            this.lblLeds.Size = new System.Drawing.Size(172, 20);
            this.lblLeds.TabIndex = 1;
            this.lblLeds.Text = "Temporizador leds [s]";
            // 
            // numMotor1
            // 
            this.numMotor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMotor1.Location = new System.Drawing.Point(279, 22);
            this.numMotor1.Name = "numMotor1";
            this.numMotor1.Size = new System.Drawing.Size(94, 32);
            this.numMotor1.TabIndex = 3;
            // 
            // numMotor2
            // 
            this.numMotor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMotor2.Location = new System.Drawing.Point(279, 60);
            this.numMotor2.Name = "numMotor2";
            this.numMotor2.Size = new System.Drawing.Size(94, 32);
            this.numMotor2.TabIndex = 4;
            // 
            // lblNumMotor1
            // 
            this.lblNumMotor1.AutoSize = true;
            this.lblNumMotor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumMotor1.Location = new System.Drawing.Point(15, 28);
            this.lblNumMotor1.Name = "lblNumMotor1";
            this.lblNumMotor1.Size = new System.Drawing.Size(239, 20);
            this.lblNumMotor1.TabIndex = 5;
            this.lblNumMotor1.Text = "Velocidad banda 1 [cajas/hora]";
            // 
            // lblNumMotor2
            // 
            this.lblNumMotor2.AutoSize = true;
            this.lblNumMotor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumMotor2.Location = new System.Drawing.Point(15, 66);
            this.lblNumMotor2.Name = "lblNumMotor2";
            this.lblNumMotor2.Size = new System.Drawing.Size(239, 20);
            this.lblNumMotor2.TabIndex = 6;
            this.lblNumMotor2.Text = "Velocidad banda 2 [cajas/hora]";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(305, 341);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 36);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnUpdate_HelpRequested);
            // 
            // numTimeLed
            // 
            this.numTimeLed.DecimalPlaces = 1;
            this.numTimeLed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTimeLed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numTimeLed.Location = new System.Drawing.Point(279, 25);
            this.numTimeLed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numTimeLed.Name = "numTimeLed";
            this.numTimeLed.Size = new System.Drawing.Size(94, 32);
            this.numTimeLed.TabIndex = 8;
            this.numTimeLed.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // gbTimer
            // 
            this.gbTimer.Controls.Add(this.numTimeLed);
            this.gbTimer.Controls.Add(this.lblLeds);
            this.gbTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.58F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTimer.Location = new System.Drawing.Point(42, 32);
            this.gbTimer.Name = "gbTimer";
            this.gbTimer.Size = new System.Drawing.Size(395, 71);
            this.gbTimer.TabIndex = 9;
            this.gbTimer.TabStop = false;
            this.gbTimer.Text = "Leds";
            this.gbTimer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.gbTimer_HelpRequested);
            // 
            // gbVelocity
            // 
            this.gbVelocity.Controls.Add(this.numMotor2);
            this.gbVelocity.Controls.Add(this.numMotor1);
            this.gbVelocity.Controls.Add(this.lblNumMotor1);
            this.gbVelocity.Controls.Add(this.lblNumMotor2);
            this.gbVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.58F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVelocity.Location = new System.Drawing.Point(42, 109);
            this.gbVelocity.Name = "gbVelocity";
            this.gbVelocity.Size = new System.Drawing.Size(395, 108);
            this.gbVelocity.TabIndex = 10;
            this.gbVelocity.TabStop = false;
            this.gbVelocity.Text = "Bandas";
            this.gbVelocity.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.gbVelocity_HelpRequested);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numPercentage2);
            this.groupBox1.Controls.Add(this.numPercentage1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.58F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(42, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 108);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Addons";
            // 
            // numPercentage2
            // 
            this.numPercentage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPercentage2.Location = new System.Drawing.Point(279, 60);
            this.numPercentage2.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numPercentage2.Name = "numPercentage2";
            this.numPercentage2.Size = new System.Drawing.Size(94, 32);
            this.numPercentage2.TabIndex = 4;
            this.numPercentage2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numPercentage1
            // 
            this.numPercentage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPercentage1.Location = new System.Drawing.Point(279, 22);
            this.numPercentage1.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numPercentage1.Name = "numPercentage1";
            this.numPercentage1.Size = new System.Drawing.Size(94, 32);
            this.numPercentage1.TabIndex = 3;
            this.numPercentage1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Porcentaje banda 1 [%]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Porcentaje banda 2 [%]";
            // 
            // PLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 400);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.gbVelocity);
            this.Controls.Add(this.gbTimer);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLC";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.PLC_HelpButtonClicked);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PLC_FormClosed);
            this.Load += new System.EventHandler(this.PLC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMotor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMotor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeLed)).EndInit();
            this.gbTimer.ResumeLayout(false);
            this.gbTimer.PerformLayout();
            this.gbVelocity.ResumeLayout(false);
            this.gbVelocity.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPercentage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblLeds;
        private System.Windows.Forms.NumericUpDown numMotor1;
        private System.Windows.Forms.NumericUpDown numMotor2;
        private System.Windows.Forms.Label lblNumMotor1;
        private System.Windows.Forms.Label lblNumMotor2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.NumericUpDown numTimeLed;
        private System.Windows.Forms.GroupBox gbTimer;
        private System.Windows.Forms.GroupBox gbVelocity;
        private System.Windows.Forms.HelpProvider helpProv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numPercentage2;
        private System.Windows.Forms.NumericUpDown numPercentage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}