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
            modbusClient.WriteSingleRegister(REGS + 0, (int)CodeValue((double)numericUpDown1.Value));
            //modbusClient.WriteSingleRegister(REGS + 0, (int)numericUpDown1.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleRegister(REGS + 1, (int)CodeValue((double)numericUpDown2.Value));
            //modbusClient.WriteSingleRegister(REGS + 1, (int)numericUpDown2.Value);
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

        //-- Banda 1
        private void B1Elevador1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 0, new bool[] { false, false, false });
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 0, new bool[] { true, false, false });
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 0, new bool[] { false, true, false });
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 0, new bool[] { false, false, true });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 0, new bool[] { true, true, true });
        }

        private void B1Elevador2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 3, new bool[] { false, false, false });
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 3, new bool[] { true, false, false });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 3, new bool[] { false, true, false });
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 3, new bool[] { false, false, true });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 3, new bool[] { true, true, true });
        }

        private void B1Elevador3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 6, new bool[] { false, false, false });
        }

        private void button12_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 6, new bool[] { true, false, false });
        }

        private void button11_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 6, new bool[] { false, true, false });
        }

        private void button10_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 6, new bool[] { false, false, true });
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 6, new bool[] { true, true, true });
        }

        private void B1Addon_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 9, new bool[] { false, false, false });
        }

        private void button16_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 9, new bool[] { true, false, false });
        }

        private void button15_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 9, new bool[] { false, true, false });
        }

        private void button14_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 9, new bool[] { false, false, true });
        }

        private void button13_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 9, new bool[] { true, true, true });
        }

        //-- Bandas
        private void button33_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 12, false);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 12, true);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 13, false);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            modbusClient.WriteSingleCoil(COILS + 13, true);
        }

        //-- Banda 2
        private void B2Elevador1_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 14, new bool[] { false, false, false });
        }

        private void button28_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 14, new bool[] { true, false, false });
        }

        private void button27_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 14, new bool[] { false, true, false });
        }

        private void button26_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 14, new bool[] { false, false, true });
        }

        private void button25_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 14, new bool[] { true, true, true });
        }

        private void B2Elevador2_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 17, new bool[] { false, false, false });
        }

        private void button20_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 17, new bool[] { true, false, false });
        }

        private void button19_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 17, new bool[] { false, true, false });
        }

        private void button18_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 17, new bool[] { false, false, true });
        }

        private void button17_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 17, new bool[] { true, true, true });
        }

        private void B2Elevador3_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 20, new bool[] { false, false, false });
        }

        private void button24_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 20, new bool[] { true, false, false });
        }

        private void button23_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 20, new bool[] { false, true, false });
        }

        private void button22_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 20, new bool[] { false, false, true });
        }

        private void button21_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 20, new bool[] { true, true, true });
        }

        private void B2Addon_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 23, new bool[] { false, false, false });
        }

        private void button32_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 23, new bool[] { true, false, false });
        }

        private void button31_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 23, new bool[] { false, true, false });
        }

        private void button30_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 23, new bool[] { false, false, true });
        }

        private void button29_Click(object sender, EventArgs e)
        {
            modbusClient.WriteMultipleCoils(COILS + 23, new bool[] { true, true, true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxesHour"></param>
        /// <param name="reduction"></param>
        /// <param name="diameter"></param>
        /// <param name="numBoxes"></param>
        /// <param name="box2box"></param>
        /// <returns></returns>
        private double ExpectedRPM(double boxesHour, double reduction, double diameter, double numBoxes, double box2box)
        {
            double boxesPerMinute = boxesHour / 60;
            double linealDistance = diameter * Math.PI;
            double distanceBtwBoxes = numBoxes * box2box;
            return boxesPerMinute * (distanceBtwBoxes / linealDistance) * reduction;
        }
        /// <summary>
        /// Codifica un valor.
        /// </summary>
        /// <param name="value">Valor a codificar</param>
        /// <returns>Devuelve el valor codificado</returns>
        private double CodeValue(double value)
        {
            double newValue = Math.Ceiling(ExpectedRPM(value, 40, 87.27, 9, 30 * 2.54));
            return Math.Floor(Map(newValue, 0, 1390, 0, 4095));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromSource"></param>
        /// <param name="toSource"></param>
        /// <param name="fromTarget"></param>
        /// <param name="toTarget"></param>
        /// <returns></returns>
        public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }


    }
}
