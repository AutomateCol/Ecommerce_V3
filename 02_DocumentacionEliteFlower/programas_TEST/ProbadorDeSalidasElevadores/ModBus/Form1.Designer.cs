
namespace ModBus
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Conectar = new System.Windows.Forms.Button();
            this.SetMotor1 = new System.Windows.Forms.Button();
            this.SetMotor2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.GetMotor2 = new System.Windows.Forms.Button();
            this.GetMotor1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.gbMotores = new System.Windows.Forms.GroupBox();
            this.gbBanda1 = new System.Windows.Forms.GroupBox();
            this.B1Addon = new System.Windows.Forms.Button();
            this.B1Elevador3 = new System.Windows.Forms.Button();
            this.B1Elevador2 = new System.Windows.Forms.Button();
            this.B1Elevador1 = new System.Windows.Forms.Button();
            this.gbBanda2 = new System.Windows.Forms.GroupBox();
            this.B2Addon = new System.Windows.Forms.Button();
            this.B2Elevador2 = new System.Windows.Forms.Button();
            this.B2Elevador3 = new System.Windows.Forms.Button();
            this.B2Elevador1 = new System.Windows.Forms.Button();
            this.Bandas = new System.Windows.Forms.Button();
            this.gbBandas = new System.Windows.Forms.GroupBox();
            this.GetElevador = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.gbMotores.SuspendLayout();
            this.gbBanda1.SuspendLayout();
            this.gbBanda2.SuspendLayout();
            this.gbBandas.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "169.254.1.236";
            // 
            // Conectar
            // 
            this.Conectar.Location = new System.Drawing.Point(156, 27);
            this.Conectar.Name = "Conectar";
            this.Conectar.Size = new System.Drawing.Size(97, 23);
            this.Conectar.TabIndex = 1;
            this.Conectar.Text = "Connect";
            this.Conectar.UseVisualStyleBackColor = true;
            this.Conectar.Click += new System.EventHandler(this.button1_Click);
            // 
            // SetMotor1
            // 
            this.SetMotor1.Location = new System.Drawing.Point(29, 28);
            this.SetMotor1.Name = "SetMotor1";
            this.SetMotor1.Size = new System.Drawing.Size(75, 23);
            this.SetMotor1.TabIndex = 2;
            this.SetMotor1.Text = "W Motor 1";
            this.SetMotor1.UseVisualStyleBackColor = true;
            this.SetMotor1.Click += new System.EventHandler(this.button2_Click);
            // 
            // SetMotor2
            // 
            this.SetMotor2.Location = new System.Drawing.Point(29, 57);
            this.SetMotor2.Name = "SetMotor2";
            this.SetMotor2.Size = new System.Drawing.Size(75, 23);
            this.SetMotor2.TabIndex = 3;
            this.SetMotor2.Text = "W Motor 2";
            this.SetMotor2.UseVisualStyleBackColor = true;
            this.SetMotor2.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(110, 31);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown1.TabIndex = 6;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(110, 60);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown2.TabIndex = 7;
            // 
            // GetMotor2
            // 
            this.GetMotor2.Location = new System.Drawing.Point(29, 115);
            this.GetMotor2.Name = "GetMotor2";
            this.GetMotor2.Size = new System.Drawing.Size(75, 23);
            this.GetMotor2.TabIndex = 11;
            this.GetMotor2.Text = "R Motor 2";
            this.GetMotor2.UseVisualStyleBackColor = true;
            this.GetMotor2.Click += new System.EventHandler(this.button8_Click);
            // 
            // GetMotor1
            // 
            this.GetMotor1.Location = new System.Drawing.Point(29, 86);
            this.GetMotor1.Name = "GetMotor1";
            this.GetMotor1.Size = new System.Drawing.Size(75, 23);
            this.GetMotor1.TabIndex = 10;
            this.GetMotor1.Text = "R Motor 1";
            this.GetMotor1.UseVisualStyleBackColor = true;
            this.GetMotor1.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(110, 88);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(56, 20);
            this.textBox2.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(110, 117);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(56, 20);
            this.textBox3.TabIndex = 15;
            // 
            // gbMotores
            // 
            this.gbMotores.Controls.Add(this.textBox4);
            this.gbMotores.Controls.Add(this.GetElevador);
            this.gbMotores.Controls.Add(this.textBox3);
            this.gbMotores.Controls.Add(this.textBox2);
            this.gbMotores.Controls.Add(this.GetMotor2);
            this.gbMotores.Controls.Add(this.GetMotor1);
            this.gbMotores.Controls.Add(this.numericUpDown2);
            this.gbMotores.Controls.Add(this.numericUpDown1);
            this.gbMotores.Controls.Add(this.SetMotor2);
            this.gbMotores.Controls.Add(this.SetMotor1);
            this.gbMotores.Location = new System.Drawing.Point(43, 72);
            this.gbMotores.Name = "gbMotores";
            this.gbMotores.Size = new System.Drawing.Size(210, 240);
            this.gbMotores.TabIndex = 16;
            this.gbMotores.TabStop = false;
            this.gbMotores.Text = "Motores bandas";
            // 
            // gbBanda1
            // 
            this.gbBanda1.Controls.Add(this.B1Addon);
            this.gbBanda1.Controls.Add(this.B1Elevador3);
            this.gbBanda1.Controls.Add(this.B1Elevador2);
            this.gbBanda1.Controls.Add(this.B1Elevador1);
            this.gbBanda1.Location = new System.Drawing.Point(259, 72);
            this.gbBanda1.Name = "gbBanda1";
            this.gbBanda1.Size = new System.Drawing.Size(140, 160);
            this.gbBanda1.TabIndex = 17;
            this.gbBanda1.TabStop = false;
            this.gbBanda1.Text = "Banda 1";
            // 
            // B1Addon
            // 
            this.B1Addon.Location = new System.Drawing.Point(30, 115);
            this.B1Addon.Name = "B1Addon";
            this.B1Addon.Size = new System.Drawing.Size(75, 23);
            this.B1Addon.TabIndex = 3;
            this.B1Addon.Text = "Addons";
            this.B1Addon.UseVisualStyleBackColor = true;
            this.B1Addon.Click += new System.EventHandler(this.B1Addon_Click);
            // 
            // B1Elevador3
            // 
            this.B1Elevador3.Location = new System.Drawing.Point(30, 86);
            this.B1Elevador3.Name = "B1Elevador3";
            this.B1Elevador3.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador3.TabIndex = 2;
            this.B1Elevador3.Text = "Elevador 3";
            this.B1Elevador3.UseVisualStyleBackColor = true;
            this.B1Elevador3.Click += new System.EventHandler(this.B1Elevador3_Click);
            // 
            // B1Elevador2
            // 
            this.B1Elevador2.Location = new System.Drawing.Point(30, 57);
            this.B1Elevador2.Name = "B1Elevador2";
            this.B1Elevador2.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador2.TabIndex = 1;
            this.B1Elevador2.Text = "Elevador 2";
            this.B1Elevador2.UseVisualStyleBackColor = true;
            this.B1Elevador2.Click += new System.EventHandler(this.B1Elevador2_Click);
            // 
            // B1Elevador1
            // 
            this.B1Elevador1.Location = new System.Drawing.Point(30, 28);
            this.B1Elevador1.Name = "B1Elevador1";
            this.B1Elevador1.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador1.TabIndex = 0;
            this.B1Elevador1.Text = "Elevador 1";
            this.B1Elevador1.UseVisualStyleBackColor = true;
            this.B1Elevador1.Click += new System.EventHandler(this.B1Elevador1_Click);
            // 
            // gbBanda2
            // 
            this.gbBanda2.Controls.Add(this.B2Addon);
            this.gbBanda2.Controls.Add(this.B2Elevador2);
            this.gbBanda2.Controls.Add(this.B2Elevador3);
            this.gbBanda2.Controls.Add(this.B2Elevador1);
            this.gbBanda2.Location = new System.Drawing.Point(405, 72);
            this.gbBanda2.Name = "gbBanda2";
            this.gbBanda2.Size = new System.Drawing.Size(140, 160);
            this.gbBanda2.TabIndex = 18;
            this.gbBanda2.TabStop = false;
            this.gbBanda2.Text = "Banda 2";
            // 
            // B2Addon
            // 
            this.B2Addon.Location = new System.Drawing.Point(30, 115);
            this.B2Addon.Name = "B2Addon";
            this.B2Addon.Size = new System.Drawing.Size(75, 23);
            this.B2Addon.TabIndex = 7;
            this.B2Addon.Text = "Addons";
            this.B2Addon.UseVisualStyleBackColor = true;
            this.B2Addon.Click += new System.EventHandler(this.B2Addon_Click);
            // 
            // B2Elevador2
            // 
            this.B2Elevador2.Location = new System.Drawing.Point(30, 57);
            this.B2Elevador2.Name = "B2Elevador2";
            this.B2Elevador2.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador2.TabIndex = 5;
            this.B2Elevador2.Text = "Elevador 2";
            this.B2Elevador2.UseVisualStyleBackColor = true;
            this.B2Elevador2.Click += new System.EventHandler(this.B2Elevador2_Click);
            // 
            // B2Elevador3
            // 
            this.B2Elevador3.Location = new System.Drawing.Point(30, 86);
            this.B2Elevador3.Name = "B2Elevador3";
            this.B2Elevador3.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador3.TabIndex = 6;
            this.B2Elevador3.Text = "Elevador 3";
            this.B2Elevador3.UseVisualStyleBackColor = true;
            this.B2Elevador3.Click += new System.EventHandler(this.B2Elevador3_Click);
            // 
            // B2Elevador1
            // 
            this.B2Elevador1.Location = new System.Drawing.Point(30, 28);
            this.B2Elevador1.Name = "B2Elevador1";
            this.B2Elevador1.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador1.TabIndex = 4;
            this.B2Elevador1.Text = "Elevador 1";
            this.B2Elevador1.UseVisualStyleBackColor = true;
            this.B2Elevador1.Click += new System.EventHandler(this.B2Elevador1_Click);
            // 
            // Bandas
            // 
            this.Bandas.Location = new System.Drawing.Point(29, 29);
            this.Bandas.Name = "Bandas";
            this.Bandas.Size = new System.Drawing.Size(75, 23);
            this.Bandas.TabIndex = 8;
            this.Bandas.Text = "Bandas";
            this.Bandas.UseVisualStyleBackColor = true;
            this.Bandas.Click += new System.EventHandler(this.Bandas_Click);
            // 
            // gbBandas
            // 
            this.gbBandas.Controls.Add(this.Bandas);
            this.gbBandas.Location = new System.Drawing.Point(259, 238);
            this.gbBandas.Name = "gbBandas";
            this.gbBandas.Size = new System.Drawing.Size(286, 74);
            this.gbBandas.TabIndex = 19;
            this.gbBandas.TabStop = false;
            this.gbBandas.Text = "Bandas";
            // 
            // GetElevador
            // 
            this.GetElevador.Location = new System.Drawing.Point(29, 147);
            this.GetElevador.Name = "GetElevador";
            this.GetElevador.Size = new System.Drawing.Size(75, 23);
            this.GetElevador.TabIndex = 16;
            this.GetElevador.Text = "R Elevador";
            this.GetElevador.UseVisualStyleBackColor = true;
            this.GetElevador.Click += new System.EventHandler(this.GetElevador_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(110, 149);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(56, 20);
            this.textBox4.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 379);
            this.Controls.Add(this.gbBandas);
            this.Controls.Add(this.gbBanda2);
            this.Controls.Add(this.gbBanda1);
            this.Controls.Add(this.gbMotores);
            this.Controls.Add(this.Conectar);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.gbMotores.ResumeLayout(false);
            this.gbMotores.PerformLayout();
            this.gbBanda1.ResumeLayout(false);
            this.gbBanda2.ResumeLayout(false);
            this.gbBandas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Conectar;
        private System.Windows.Forms.Button SetMotor1;
        private System.Windows.Forms.Button SetMotor2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button GetMotor2;
        private System.Windows.Forms.Button GetMotor1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox gbMotores;
        private System.Windows.Forms.GroupBox gbBanda1;
        private System.Windows.Forms.Button B1Addon;
        private System.Windows.Forms.Button B1Elevador3;
        private System.Windows.Forms.Button B1Elevador2;
        private System.Windows.Forms.Button B1Elevador1;
        private System.Windows.Forms.GroupBox gbBanda2;
        private System.Windows.Forms.Button B2Addon;
        private System.Windows.Forms.Button B2Elevador2;
        private System.Windows.Forms.Button B2Elevador3;
        private System.Windows.Forms.Button B2Elevador1;
        private System.Windows.Forms.Button Bandas;
        private System.Windows.Forms.GroupBox gbBandas;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button GetElevador;
    }
}

