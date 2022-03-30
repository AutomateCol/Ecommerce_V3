using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;
using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class Luces : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        string mongoDBConnection = "mongodb://localhost:27017";
        private ModbusClient modbusLuces = new ModbusClient("169.254.1.236", 502);
        readonly int COILS_LUCES = 0x00;
        readonly int REGS_LUCES = 0x31;

        // ------------------------------
        //              Form            |
        // ------------------------------

        /// <summary>
        /// Inicializa las variables globales.
        /// </summary>
        public Luces(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los componentes en la ventana.
        /// </summary>
        private void Luces_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessages();
            tabControl1.Enabled = false;
        }
        /// <summary>
        /// Habilita la ventana que abrio esta.
        /// </summary>
        private void Luces_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modbusLuces.Connected)
            {
                DisconnectModbusClient();
            }
            _frm.Enabled = true;
        }

        // ------------------------------
        //          Components          |
        // ------------------------------

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusLuces.Connected)
                {
                    DisconnectModbusClient();
                    btnConnect.Text = "Connect";
                    tabControl1.Enabled = false;
                }
                else
                {
                    if (modbusLuces.Available(2))
                    {
                        modbusLuces.Connect();
                        btnConnect.Text = "Disconnect";
                        tabControl1.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Banda 1

        //-- Encendido/Apagado
        private void btnB1ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleCoil(COILS_LUCES + 12, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnB1OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleCoil(COILS_LUCES + 12, false);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Velocidad de la banda
        private void btnB1Velocity_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleRegister(REGS_LUCES + 0, (int)CodeValue((double)numB1Velocity.Value));
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E1
        private void btnE11OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 0, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE11P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 0, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE11P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 0, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE11P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 0, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE11ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 0, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E2
        private void btnE12OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 3, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE12P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 3, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE12P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 3, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE12P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 3, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE12ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 3, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E3
        private void btnE13OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 6, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE13P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 6, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE13P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 6, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE13P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 6, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE13ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 6, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E4
        private void btnE14OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 9, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE14P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 9, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE14P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 9, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE14P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 9, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE14ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 9, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Banda 2

        //-- Encendido/Apagado
        private void btnB2ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleCoil(COILS_LUCES + 13, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnB2OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleCoil(COILS_LUCES + 13, false);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Velocidad de la banda
        private void btnB2Velocity_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteSingleRegister(REGS_LUCES + 1, (int)CodeValue((double)numB2Velocity.Value));
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E1
        private void btnE21OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE21P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE21P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE21P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE21ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E2
        private void btnE22OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 17, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE22P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 17, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE22P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 17, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE22P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 17, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE22ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 17, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E3
        private void btnE23OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 20, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE23P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 20, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE23P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 20, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE23P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 20, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE23ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 20, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Encendido/Apagado Pilotos E4
        private void btnE24OFF_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 23, new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE24P1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 23, new bool[] { true, false, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE24P2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 23, new bool[] { false, true, false });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE24P3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 23, new bool[] { false, false, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        private void btnE24ON_Click(object sender, EventArgs e)
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + 23, new bool[] { true, true, true });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        // ---------------------------
        //          Methods          |
        // ---------------------------

        private void DisconnectModbusClient()
        {
            try
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES,
                    new bool[]
                    {
                        false, false, false,    //-- B1E1Pilotos
                        false, false, false,    //-- B1E2Pilotos
                        false, false, false,    //-- B1E3Pilotos
                        false, false, false,    //-- B1E4Pilotos
                        false, false,           //-- B1B2Active
                        false, false, false,    //-- B2E1Pilotos
                        false, false, false,    //-- B2E2Pilotos
                        false, false, false,    //-- B2E3Pilotos
                        false, false, false     //-- B2E4Pilotos
                    }
                );
                modbusLuces.WriteMultipleRegisters(REGS_LUCES, new int[] { 0, 0 });
                modbusLuces.Disconnect();
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
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
        /// Realiza una linealizacion entre 2 rangos diferentes
        /// </summary>
        /// <param name="value">Valor a cambiar en el rango 1</param>
        /// <param name="fromSource">Valor inicial rango 1</param>
        /// <param name="toSource">Valor final rango 1</param>
        /// <param name="fromTarget">Valor inicial rango 2</param>
        /// <param name="toTarget">Valor final rango 2</param>
        /// <returns>Retorna valor mapeado en el rango 2</returns>
        public static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        /// <summary>
        /// Inicializa los nombre de todos los textos segun el idioma.
        /// </summary>
        private void SetUIMessages()
        {
            if (_EnglishChecked)
            {
                this.Text = Properties.ResourceUI_en_US.T031;
                btnConnect.Text = Properties.ResourceUI_en_US.T025;
                tabPage1.Text = string.Format(Properties.ResourceUI_en_US.T014, 1);
                groupBox1.Text = string.Format(Properties.ResourceUI_en_US.T014, 1);
                groupBox2.Text = Properties.ResourceUI_en_US.T037;
                groupBox3.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                groupBox4.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                groupBox5.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                btnB1Velocity.Text = Properties.ResourceUI_en_US.T038;
                tabPage2.Text = string.Format(Properties.ResourceUI_en_US.T014, 2);
                groupBox7.Text = string.Format(Properties.ResourceUI_en_US.T014, 2);
                groupBox8.Text = Properties.ResourceUI_en_US.T037;
                groupBox12.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                groupBox11.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                groupBox10.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                btnB2Velocity.Text = Properties.ResourceUI_en_US.T038;
            }
            else
            {
                this.Text = Properties.ResourceUI_es_CO.T031;
                btnConnect.Text = Properties.ResourceUI_es_CO.T025;
                tabPage1.Text = string.Format(Properties.ResourceUI_es_CO.T014, 1);
                groupBox1.Text = string.Format(Properties.ResourceUI_es_CO.T014, 1);
                groupBox2.Text = Properties.ResourceUI_es_CO.T037;
                groupBox3.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                groupBox4.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                groupBox5.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                btnB1Velocity.Text = Properties.ResourceUI_es_CO.T038;
                tabPage2.Text = string.Format(Properties.ResourceUI_es_CO.T014, 2);
                groupBox7.Text = string.Format(Properties.ResourceUI_es_CO.T014, 2);
                groupBox8.Text = Properties.ResourceUI_es_CO.T037;
                groupBox12.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                groupBox11.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                groupBox10.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                btnB2Velocity.Text = Properties.ResourceUI_es_CO.T038;
            }
        }
    }
}
