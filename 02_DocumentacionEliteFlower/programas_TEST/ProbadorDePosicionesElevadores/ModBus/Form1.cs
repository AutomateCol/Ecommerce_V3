using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;

namespace ModBus
{
    public partial class Form1 : Form
    {

        private ModbusClient modbusClient = new ModbusClient();
        readonly int COILS = 0x00;
        readonly int REGS = 0x31;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gpEstacion1.Enabled = false;
            gpEstacion2.Enabled = false;
            gpEstacion3.Enabled = false;
            gpEstacion4.Enabled = false;
            gpAlimentador.Enabled = false;
            gpEmpujador.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modbusClient.Connected)
            {
                modbusClient.Disconnect();
            }
        }

        //-- Conectar
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (modbusClient.Connected)
            {
                modbusClient.Disconnect();
                Conectar.Text = "Connect";
                gpEstacion1.Enabled = false;
                gpEstacion2.Enabled = false;
                gpEstacion3.Enabled = false;
                gpEstacion4.Enabled = false;
                gpAlimentador.Enabled = false;
                gpEmpujador.Enabled = false;
            }
            else
            {
                modbusClient = new ModbusClient(textBox1.Text, 502);
                if (modbusClient.Available(2))
                {
                    modbusClient.Connect();
                    Conectar.Text = "Disconnect";
                    gpEstacion1.Enabled = true;
                    gpEstacion2.Enabled = true;
                    gpEstacion3.Enabled = true;
                    gpEstacion4.Enabled = true;
                    gpAlimentador.Enabled = true;
                    gpEmpujador.Enabled = true;
                }
            }
        }

        //-- Estacion 1
        private void E1S1HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 0, true);
            }
        }

        private void E1S1POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 1, true);
        }

        private void E1S1POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 2, true);
        }

        private void E1S1POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 3, true);
        }

        private void E1S2HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 4, true);
            }
        }

        private void E1S2POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 5, true);
        }

        private void E1S2POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 6, true);
        }

        private void E1S2POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 7, true);
        }

        private void E1S3HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 8, true);
            }
        }

        private void E1S3POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 9, true);
        }

        private void E1S3POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 10, true);
        }

        private void E1S3POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 11, true);
        }

        //-- Estacion 2
        private void E2S1HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 22, true);
            }
        }

        private void E2S1POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 23, true);
        }

        private void E2S1POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 24, true);
        }

        private void E2S1POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 25, true);
        }

        private void E2S2HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 26, true);
            }
        }

        private void E2S2POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 27, true);
        }

        private void E2S2POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 28, true);
        }

        private void E2S2POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 29, true);
        }

        private void E2S3HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 30, true);
            }
        }

        private void E2S3POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 31, true);
        }

        private void E2S3POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 32, true);
        }

        private void E2S3POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 33, true);
        }

        //-- Estacion 3
        private void E3S1HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 48, true);
            }
        }

        private void E3S1POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 49, true);
        }

        private void E3S1POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 50, true);
        }

        private void E3S1POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 51, true);
        }


        private void E3S2HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 52, true);
            }
        }

        private void E3S2POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 53, true);
        }

        private void E3S2POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 54, true);
        }

        private void E3S2POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 55, true);
        }

        private void E3S3HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 56, true);
            }
        }

        private void E3S3POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 57, true);
        }
        private void E3S3POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 58, true);
        }

        private void E3S3POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 59, true);
        }

        //-- Elevador alimentador
        private void M1S1HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 34, true);
            }
        }

        private void M1S1POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 35, true);
        }

        private void M1S1POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 36, true);
        }

        private void M1S1POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 37, true);
        }

        //-- Alimentador
        private void M1S2HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 12, true);
            }
        }

        private void M1S2POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 13, true);
        }

        private void M1S2POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 14, true);
        }

        private void M1S2POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 15, true);
        }

        private void M1S2POS4_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 16, true);
        }

        private void M1S2POS5_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 17, true);
        }

        private void M1S2POS6_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 18, true);
        }

        private void M1S2POS7_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 19, true);
        }

        private void M1S2POS8_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 20, true);
        }

        private void M1S2POS9_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 21, true);
        }

        //-- Empujador

        private void M1S3HOME_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quiere hacer el home?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                modbusClient.WriteSingleCoil(COILS + 38, true);
            }
        }

        private void M1S3POS1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 39, true);
        }

        private void M1S3POS2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 40, true);
        }

        private void M1S3POS3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 41, true);
        }

        private void M1S3POS4_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 42, true);
        }

        private void M1S3POS5_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 43, true);
        }

        private void M1S3POS6_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 44, true);
        }

        private void M1S3POS7_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 45, true);
        }

        private void M1S3POS8_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 46, true);
        }

        private void M1S3POS9_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 47, true);
        }
    }
}
