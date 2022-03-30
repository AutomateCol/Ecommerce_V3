
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
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.B1Addon = new System.Windows.Forms.Button();
            this.B1Elevador3 = new System.Windows.Forms.Button();
            this.B1Elevador2 = new System.Windows.Forms.Button();
            this.B1Elevador1 = new System.Windows.Forms.Button();
            this.gbBanda2 = new System.Windows.Forms.GroupBox();
            this.button29 = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.B2Addon = new System.Windows.Forms.Button();
            this.B2Elevador2 = new System.Windows.Forms.Button();
            this.B2Elevador3 = new System.Windows.Forms.Button();
            this.B2Elevador1 = new System.Windows.Forms.Button();
            this.gbBandas = new System.Windows.Forms.GroupBox();
            this.button33 = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.button36 = new System.Windows.Forms.Button();
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
            750,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(110, 60);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            750,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown2.TabIndex = 7;
            this.numericUpDown2.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
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
            this.gbMotores.Controls.Add(this.textBox3);
            this.gbMotores.Controls.Add(this.textBox2);
            this.gbMotores.Controls.Add(this.GetMotor2);
            this.gbMotores.Controls.Add(this.GetMotor1);
            this.gbMotores.Controls.Add(this.numericUpDown2);
            this.gbMotores.Controls.Add(this.numericUpDown1);
            this.gbMotores.Controls.Add(this.SetMotor2);
            this.gbMotores.Controls.Add(this.SetMotor1);
            this.gbMotores.Location = new System.Drawing.Point(12, 77);
            this.gbMotores.Name = "gbMotores";
            this.gbMotores.Size = new System.Drawing.Size(210, 259);
            this.gbMotores.TabIndex = 16;
            this.gbMotores.TabStop = false;
            this.gbMotores.Text = "Motores bandas";
            // 
            // gbBanda1
            // 
            this.gbBanda1.Controls.Add(this.button13);
            this.gbBanda1.Controls.Add(this.button14);
            this.gbBanda1.Controls.Add(this.button15);
            this.gbBanda1.Controls.Add(this.button16);
            this.gbBanda1.Controls.Add(this.button9);
            this.gbBanda1.Controls.Add(this.button10);
            this.gbBanda1.Controls.Add(this.button11);
            this.gbBanda1.Controls.Add(this.button12);
            this.gbBanda1.Controls.Add(this.button5);
            this.gbBanda1.Controls.Add(this.button6);
            this.gbBanda1.Controls.Add(this.button7);
            this.gbBanda1.Controls.Add(this.button8);
            this.gbBanda1.Controls.Add(this.button4);
            this.gbBanda1.Controls.Add(this.button3);
            this.gbBanda1.Controls.Add(this.button2);
            this.gbBanda1.Controls.Add(this.button1);
            this.gbBanda1.Controls.Add(this.B1Addon);
            this.gbBanda1.Controls.Add(this.B1Elevador3);
            this.gbBanda1.Controls.Add(this.B1Elevador2);
            this.gbBanda1.Controls.Add(this.B1Elevador1);
            this.gbBanda1.Location = new System.Drawing.Point(228, 77);
            this.gbBanda1.Name = "gbBanda1";
            this.gbBanda1.Size = new System.Drawing.Size(374, 179);
            this.gbBanda1.TabIndex = 17;
            this.gbBanda1.TabStop = false;
            this.gbBanda1.Text = "Banda 1";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(279, 138);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 19;
            this.button13.Text = "AddonON";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(279, 109);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 18;
            this.button14.Text = "AddonP3";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(279, 80);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 17;
            this.button15.Text = "AddonP2";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(279, 51);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 16;
            this.button16.Text = "AddonP1";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(198, 138);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 15;
            this.button9.Text = "E3ON";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(198, 109);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 14;
            this.button10.Text = "E3P3";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(198, 80);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 13;
            this.button11.Text = "E3P2";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(198, 51);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 12;
            this.button12.Text = "E3P1";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(117, 138);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "E2ON";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(117, 109);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "E2P3";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(117, 80);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "E2P2";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(117, 51);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "E2P1";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(36, 138);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "E1ON";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 109);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "E1P3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "E1P2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "E1P1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // B1Addon
            // 
            this.B1Addon.Location = new System.Drawing.Point(279, 22);
            this.B1Addon.Name = "B1Addon";
            this.B1Addon.Size = new System.Drawing.Size(75, 23);
            this.B1Addon.TabIndex = 3;
            this.B1Addon.Text = "AddonOFF";
            this.B1Addon.UseVisualStyleBackColor = true;
            this.B1Addon.Click += new System.EventHandler(this.B1Addon_Click);
            // 
            // B1Elevador3
            // 
            this.B1Elevador3.Location = new System.Drawing.Point(198, 22);
            this.B1Elevador3.Name = "B1Elevador3";
            this.B1Elevador3.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador3.TabIndex = 2;
            this.B1Elevador3.Text = "E3OFF";
            this.B1Elevador3.UseVisualStyleBackColor = true;
            this.B1Elevador3.Click += new System.EventHandler(this.B1Elevador3_Click);
            // 
            // B1Elevador2
            // 
            this.B1Elevador2.Location = new System.Drawing.Point(117, 22);
            this.B1Elevador2.Name = "B1Elevador2";
            this.B1Elevador2.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador2.TabIndex = 1;
            this.B1Elevador2.Text = "E2OFF";
            this.B1Elevador2.UseVisualStyleBackColor = true;
            this.B1Elevador2.Click += new System.EventHandler(this.B1Elevador2_Click);
            // 
            // B1Elevador1
            // 
            this.B1Elevador1.Location = new System.Drawing.Point(36, 22);
            this.B1Elevador1.Name = "B1Elevador1";
            this.B1Elevador1.Size = new System.Drawing.Size(75, 23);
            this.B1Elevador1.TabIndex = 0;
            this.B1Elevador1.Text = "E1OFF";
            this.B1Elevador1.UseVisualStyleBackColor = true;
            this.B1Elevador1.Click += new System.EventHandler(this.B1Elevador1_Click);
            // 
            // gbBanda2
            // 
            this.gbBanda2.Controls.Add(this.button29);
            this.gbBanda2.Controls.Add(this.button30);
            this.gbBanda2.Controls.Add(this.button31);
            this.gbBanda2.Controls.Add(this.button32);
            this.gbBanda2.Controls.Add(this.button25);
            this.gbBanda2.Controls.Add(this.button26);
            this.gbBanda2.Controls.Add(this.button27);
            this.gbBanda2.Controls.Add(this.button28);
            this.gbBanda2.Controls.Add(this.button21);
            this.gbBanda2.Controls.Add(this.button22);
            this.gbBanda2.Controls.Add(this.button23);
            this.gbBanda2.Controls.Add(this.button24);
            this.gbBanda2.Controls.Add(this.button17);
            this.gbBanda2.Controls.Add(this.button18);
            this.gbBanda2.Controls.Add(this.button19);
            this.gbBanda2.Controls.Add(this.button20);
            this.gbBanda2.Controls.Add(this.B2Addon);
            this.gbBanda2.Controls.Add(this.B2Elevador2);
            this.gbBanda2.Controls.Add(this.B2Elevador3);
            this.gbBanda2.Controls.Add(this.B2Elevador1);
            this.gbBanda2.Location = new System.Drawing.Point(608, 78);
            this.gbBanda2.Name = "gbBanda2";
            this.gbBanda2.Size = new System.Drawing.Size(371, 178);
            this.gbBanda2.TabIndex = 18;
            this.gbBanda2.TabStop = false;
            this.gbBanda2.Text = "Banda 2";
            // 
            // button29
            // 
            this.button29.Location = new System.Drawing.Point(273, 137);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(75, 23);
            this.button29.TabIndex = 23;
            this.button29.Text = "AddonON";
            this.button29.UseVisualStyleBackColor = true;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // button30
            // 
            this.button30.Location = new System.Drawing.Point(273, 108);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(75, 23);
            this.button30.TabIndex = 22;
            this.button30.Text = "AddonP3";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // button31
            // 
            this.button31.Location = new System.Drawing.Point(273, 79);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(75, 23);
            this.button31.TabIndex = 21;
            this.button31.Text = "AddonP2";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // button32
            // 
            this.button32.Location = new System.Drawing.Point(273, 50);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(75, 23);
            this.button32.TabIndex = 20;
            this.button32.Text = "AddonP1";
            this.button32.UseVisualStyleBackColor = true;
            this.button32.Click += new System.EventHandler(this.button32_Click);
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(30, 137);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(75, 23);
            this.button25.TabIndex = 19;
            this.button25.Text = "E1ON";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button26
            // 
            this.button26.Location = new System.Drawing.Point(30, 108);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(75, 23);
            this.button26.TabIndex = 18;
            this.button26.Text = "E1P3";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(30, 79);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(75, 23);
            this.button27.TabIndex = 17;
            this.button27.Text = "E1P2";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // button28
            // 
            this.button28.Location = new System.Drawing.Point(30, 50);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(75, 23);
            this.button28.TabIndex = 16;
            this.button28.Text = "E1P1";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(192, 137);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(75, 23);
            this.button21.TabIndex = 15;
            this.button21.Text = "E3ON";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(192, 108);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(75, 23);
            this.button22.TabIndex = 14;
            this.button22.Text = "E3P3";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(192, 79);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(75, 23);
            this.button23.TabIndex = 13;
            this.button23.Text = "E3P2";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button24
            // 
            this.button24.Location = new System.Drawing.Point(192, 50);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(75, 23);
            this.button24.TabIndex = 12;
            this.button24.Text = "E3P1";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(111, 137);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 11;
            this.button17.Text = "E2ON";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(111, 108);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 10;
            this.button18.Text = "E2P3";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(111, 79);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 9;
            this.button19.Text = "E2P2";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(111, 50);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 8;
            this.button20.Text = "E2P1";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // B2Addon
            // 
            this.B2Addon.Location = new System.Drawing.Point(273, 21);
            this.B2Addon.Name = "B2Addon";
            this.B2Addon.Size = new System.Drawing.Size(75, 23);
            this.B2Addon.TabIndex = 7;
            this.B2Addon.Text = "AddonOFF";
            this.B2Addon.UseVisualStyleBackColor = true;
            this.B2Addon.Click += new System.EventHandler(this.B2Addon_Click);
            // 
            // B2Elevador2
            // 
            this.B2Elevador2.Location = new System.Drawing.Point(111, 21);
            this.B2Elevador2.Name = "B2Elevador2";
            this.B2Elevador2.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador2.TabIndex = 5;
            this.B2Elevador2.Text = "E2OFF";
            this.B2Elevador2.UseVisualStyleBackColor = true;
            this.B2Elevador2.Click += new System.EventHandler(this.B2Elevador2_Click);
            // 
            // B2Elevador3
            // 
            this.B2Elevador3.Location = new System.Drawing.Point(192, 21);
            this.B2Elevador3.Name = "B2Elevador3";
            this.B2Elevador3.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador3.TabIndex = 6;
            this.B2Elevador3.Text = "E3OFF";
            this.B2Elevador3.UseVisualStyleBackColor = true;
            this.B2Elevador3.Click += new System.EventHandler(this.B2Elevador3_Click);
            // 
            // B2Elevador1
            // 
            this.B2Elevador1.Location = new System.Drawing.Point(30, 21);
            this.B2Elevador1.Name = "B2Elevador1";
            this.B2Elevador1.Size = new System.Drawing.Size(75, 23);
            this.B2Elevador1.TabIndex = 4;
            this.B2Elevador1.Text = "E1OFF";
            this.B2Elevador1.UseVisualStyleBackColor = true;
            this.B2Elevador1.Click += new System.EventHandler(this.B2Elevador1_Click);
            // 
            // gbBandas
            // 
            this.gbBandas.Controls.Add(this.button35);
            this.gbBandas.Controls.Add(this.button36);
            this.gbBandas.Controls.Add(this.button34);
            this.gbBandas.Controls.Add(this.button33);
            this.gbBandas.Location = new System.Drawing.Point(228, 262);
            this.gbBandas.Name = "gbBandas";
            this.gbBandas.Size = new System.Drawing.Size(751, 74);
            this.gbBandas.TabIndex = 19;
            this.gbBandas.TabStop = false;
            this.gbBandas.Text = "Bandas";
            // 
            // button33
            // 
            this.button33.Location = new System.Drawing.Point(36, 30);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(75, 23);
            this.button33.TabIndex = 9;
            this.button33.Text = "B1OFF";
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // button34
            // 
            this.button34.Location = new System.Drawing.Point(117, 30);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(75, 23);
            this.button34.TabIndex = 10;
            this.button34.Text = "B1ON";
            this.button34.UseVisualStyleBackColor = true;
            this.button34.Click += new System.EventHandler(this.button34_Click);
            // 
            // button35
            // 
            this.button35.Location = new System.Drawing.Point(279, 30);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(75, 23);
            this.button35.TabIndex = 12;
            this.button35.Text = "B2ON";
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // button36
            // 
            this.button36.Location = new System.Drawing.Point(198, 30);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(75, 23);
            this.button36.TabIndex = 11;
            this.button36.Text = "B2OFF";
            this.button36.UseVisualStyleBackColor = true;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 348);
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
        private System.Windows.Forms.GroupBox gbBandas;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.Button button33;
    }
}

