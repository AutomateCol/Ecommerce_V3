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
    public partial class Elevadores : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        bool _servostatus = false;
        string mongoDBConnection = "mongodb://localhost:27017";
        private static ModbusClient modbusElevadores = new ModbusClient("169.254.1.238", 502);
        readonly int COILS_ELEVADORES = 0;
        readonly int REGS_ELEVADORES = 1000;
        string msgBody = "¿Quiere hacer el home?";
        string msgTitle = "Pregunta";


        // ------------------------------
        //              Form            |
        // ------------------------------

        /// <summary>
        /// Inicializa las variables globales.
        /// </summary>
        public Elevadores(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los componentes en la ventana.
        /// </summary>
        private void Elevadores_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessages();
            tabControl1.Enabled = false;
            groupBox7.Enabled = false;
        }
        /// <summary>
        /// Habilita la ventana que abrio esta.
        /// </summary>
        private void Elevadores_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modbusElevadores.Connected)
            {
                DisconnectModbusClient();
            }
            _frm.Enabled = true;
        }

        // ---------------------------
        //          Components       |
        // ---------------------------

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusElevadores.Connected)
                {
                    if (_servostatus == true)
                    {
                        modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                        _servostatus = false;
                    }                  
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, false);
                    DisconnectModbusClient();
                    btnConnect.Text = "Connect";
                    tabControl1.Enabled = false;
                    groupBox7.Enabled = false;
                }
                else
                {
                    if (modbusElevadores.Available(2))
                    {
                        modbusElevadores.Connect();
                        btnConnect.Text = "Disconnect";
                        tabControl1.Enabled = true;
                        groupBox7.Enabled = true;
                        modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 50, false);
                        modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, true);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
            
        }

        //-- Actuadores
        private void btnPiston1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 52, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnPiston2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 53, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnMotSorter_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 54, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSON_Click(object sender, EventArgs e)
        {
            try
            {
                if (_servostatus == false)
                {
                    _servostatus = true;
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                  
                    btnSON.Text = "S_OFF";
                }
                else if (_servostatus == true)
                {
                    _servostatus = false;
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                  

                    btnSON.Text = "S_ON";
                }
                
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Zona 1

        //-- Estacion 1
        private void btnE1S1HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 0, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S1POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 1, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S1POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 57, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S2HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 4, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S2POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 5, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S2POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 6, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S3HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 8, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S3POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 9, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE1S3POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 10, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Estacion 2
        private void btnE2S1HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 13, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S1POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 14, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S1POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 15, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S2HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 17, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S2POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 18, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S2POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 19, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S3HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 21, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S3POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 22, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE2S3POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 23, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Estacion 3
        private void btnE3S1HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 26, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S1POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 27, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S1POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 28, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S2HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 30, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S2POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 31, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S2POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 32, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S3HOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 34, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S3POS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 35, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnE3S3POS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 36, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Zona 2

        //-- Elevador
        private void btnElevHOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 38, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnElevPOS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 39, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnElevPOS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 40, true);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Sorter
        private void btnSorterHOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 12, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 1);
                Mongoose.SetLastPosZone2(new List<int>() { 1, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 2);
                Mongoose.SetLastPosZone2(new List<int>() { 2, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 3);
                Mongoose.SetLastPosZone2(new List<int>() { 3, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS4_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 4);
                Mongoose.SetLastPosZone2(new List<int>() { 4, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS5_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 5);
                Mongoose.SetLastPosZone2(new List<int>() { 5, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS6_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 6);
                Mongoose.SetLastPosZone2(new List<int>() { 6, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS7_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 7);
                Mongoose.SetLastPosZone2(new List<int>() { 7, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS8_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 8);
                Mongoose.SetLastPosZone2(new List<int>() { 8, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnSorterPOS9_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 9);
                Mongoose.SetLastPosZone2(new List<int>() { 9, -1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        //-- Empujador
        private void btnEmpHOME_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 25, true);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS1_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 1);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 1 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS2_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 2);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 2 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS3_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 3);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 3 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS4_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 4);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 4 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS5_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 5);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 5 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS6_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 6);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 6 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS7_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 7);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 7 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS8_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 8);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 8 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnEmpPOS9_Click(object sender, EventArgs e)
        {
            try
            {
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 9);
                Mongoose.SetLastPosZone2(new List<int>() { -1, 9 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //-- Sorter
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 5);
                //-- Empujador
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 6);
                Mongoose.SetLastPosZone2(new List<int>() { 9, 9 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        // ------------------------
        //          Methods       |
        // ------------------------

        private void DisconnectModbusClient()
        {
            try
            {
                //modbusElevadores.WriteMultipleCoils(COILS_ELEVADORES, new bool[] { false, });
                //modbusElevadores.WriteMultipleRegisters(REGS_ELEVADORES, new int[] { 0, 0 });
                modbusElevadores.Disconnect();
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        /// <summary>
        /// Inicializa los nombre de todos los textos segun el idioma.
        /// </summary>
        private void SetUIMessages()
        {
            if (_EnglishChecked)
            {
                this.Text = Properties.ResourceUI_en_US.T032;
                btnConnect.Text = Properties.ResourceUI_en_US.T025;
                groupBox7.Text = Properties.ResourceUI_en_US.T038;
                tabPage1.Text = string.Format(Properties.ResourceUI_en_US.T040, 1);
                groupBox1.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                groupBox2.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                groupBox3.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                tabPage2.Text = string.Format(Properties.ResourceUI_en_US.T040, 2);
                groupBox4.Text = Properties.ResourceUI_en_US.T041;
                groupBox5.Text = Properties.ResourceUI_en_US.T042;
                groupBox6.Text = Properties.ResourceUI_en_US.T043;
                label1.Text = Properties.ResourceUI_en_US.T044;
            }
            else
            {
                this.Text = Properties.ResourceUI_es_CO.T032;
                btnConnect.Text = Properties.ResourceUI_es_CO.T025;
                groupBox7.Text = Properties.ResourceUI_es_CO.T038;
                tabPage1.Text = string.Format(Properties.ResourceUI_es_CO.T040, 1);
                groupBox1.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                groupBox2.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                groupBox3.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                tabPage2.Text = string.Format(Properties.ResourceUI_es_CO.T040, 2);
                groupBox4.Text = Properties.ResourceUI_es_CO.T041;
                groupBox5.Text = Properties.ResourceUI_es_CO.T042;
                groupBox6.Text = Properties.ResourceUI_es_CO.T043;
                label1.Text = Properties.ResourceUI_es_CO.T044;
            }
        }

        async private void btnHome_Click(object sender, EventArgs e)
        {
            var CoilsHome = new List<int> {0,4,8,13,17,21,26,30,34,38,12,25};

            try
            {
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    foreach (int item in CoilsHome)
                    {
                        int coil = COILS_ELEVADORES + item;
                        textBox1.Text = coil.ToString();
                        modbusElevadores.WriteSingleCoil(coil, true);
                        await Task.Delay(200);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        async private void btnInitAll_Click(object sender, EventArgs e)
        {

            var CoilPos1 = new List<int> { 1,5,9,14,18,22,27,31,35,39 };
            try
            {
                foreach (int item in CoilPos1)
                {
                    int coil = COILS_ELEVADORES + item;                
                    modbusElevadores.WriteSingleCoil(coil, true);
                    await Task.Delay(100);
                  
                }
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 5);
                //-- Empujador
                modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 6);
                Mongoose.SetLastPosZone2(new List<int>() { 9, 9 });
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        
    }
}
