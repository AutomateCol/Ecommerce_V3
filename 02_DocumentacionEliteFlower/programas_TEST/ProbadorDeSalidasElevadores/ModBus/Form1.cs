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
            gbMotores.Enabled = false;
            gbBanda1.Enabled = false;
            gbBanda2.Enabled = false;
            gbBandas.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modbusClient.Connected)
            {
                modbusClient.WriteMultipleCoils(COILS, 
                    new bool[] { 
                        false, false, false, 
                        false, false, false, 
                        false, false, false, 
                        false, false, false, 
                        false, false,
                        false, false, false,
                        false, false, false,
                        false, false, false,
                        false, false, false
                    });
                modbusClient.WriteMultipleRegisters(REGS, new int[] { 0, 0 });
                modbusClient.Disconnect();
            }
        }

        //-- Conectar
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (modbusClient.Connected)
            {
                modbusClient.WriteMultipleCoils(COILS,
                    new bool[] {
                        false, false, false,
                        false, false, false,
                        false, false, false,
                        false, false, false,
                        false, false,
                        false, false, false,
                        false, false, false,
                        false, false, false,
                        false, false, false
                    });
                modbusClient.WriteMultipleRegisters(REGS, new int[] { 0, 0 });
                modbusClient.Disconnect();
                Conectar.Text = "Connect";
                gbMotores.Enabled = false;
                gbBanda1.Enabled = false;
                gbBanda2.Enabled = false;
                gbBandas.Enabled = false;
            }
            else
            {
                modbusClient = new ModbusClient(textBox1.Text, 502);
                if (modbusClient.Available(2))
                {
                    modbusClient.Connect();
                    Conectar.Text = "Disconnect";
                    gbMotores.Enabled = true;
                    gbBanda1.Enabled = true;
                    gbBanda2.Enabled = true;
                    gbBandas.Enabled = true;
                }
            }
        }

        //-- V_REF Bandas
        private void button2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleRegister(REGS + 0, (int)numericUpDown1.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleRegister(REGS + 1, (int)numericUpDown2.Value);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int[] tt = modbusClient.ReadHoldingRegisters(REGS + 0, 1);
            textBox2.Text = $"{tt[0]}";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] tt = modbusClient.ReadHoldingRegisters(REGS + 1, 1);
            textBox3.Text = $"{tt[0]}";
        }

        private void GetElevador_Click(object sender, EventArgs e)
        {
            int[] tt = modbusClient.ReadHoldingRegisters(REGS + 4, 1);
            textBox4.Text = $"{tt[0]}";
        }

        //-- Banda 1
        private void B1Elevador1_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 0, tt1 );
        }

        private void B1Elevador2_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 3, tt1 );
        }

        private void B1Elevador3_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 6, tt1 );
        }

        private void B1Addon_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 9, tt1 );
        }

        //-- Bandas
        private void Bandas_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false },
                new bool[] { true, false },
                new bool[] { false, true },
                new bool[] { true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 12, tt1 );
        }

        //-- Banda 2
        private void B2Elevador1_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 14, tt1 );
        }

        private void B2Elevador2_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 17, tt1 );
        }

        private void B2Elevador3_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 20, tt1 );
        }

        private void B2Addon_Click(object sender, EventArgs e)
        {
            var random = new Random();
            List<bool[]> tt = new List<bool[]> {
                new bool[] { false, false, false },
                new bool[] { true, false, false },
                new bool[] { false, true, false },
                new bool[] { false, false, true },
                new bool[] { true, true, true }
            };
            bool[] tt1 = tt[random.Next(tt.Count)];
            modbusClient.WriteMultipleCoils(COILS + 23, tt1 );
        }
    }
}
