using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace EliteFlower
{
    public partial class PLC : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        string mongoDBConnection = "mongodb://localhost:27017";

        // -----------------------------------
        //               Form                |
        // -----------------------------------

        public PLC(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los valores en la ventana.
        /// </summary>
        private void PLC_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessage();
            InitComponentValues();
        }
        /// <summary>
        /// Habilita la ventana desde la cual se llamo esta ventana.
        /// </summary>
        private void PLC_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }

        // -----------------------------------
        //             Components            |
        // -----------------------------------

        /// <summary>
        /// Actualiza los valores que se asignan.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIMessages.PLC(2, _EnglishChecked), "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string source = Microsoft.VisualBasic.Interaction.InputBox("¿Ingrese contraseña para validar usuario?", "Contraseña", "");
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        //From String to byte array
                        byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                        byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                        string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                        //Console.WriteLine(hash);
                        //if (hash == "9B2C1ACC635D03F27B2C26C8DCE1F332C06EEE3D95CF01006E3467B57DBAB9DB") //-- kz2PG3Rg
                        if (hash == "240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9") //-- admin123
                        {
                            MongoClient client = new MongoClient(mongoDBConnection);
                            IMongoDatabase database = client.GetDatabase("EliteFlower");
                            IMongoCollection<timePLC> PLCDB = database.GetCollection<timePLC>("MasterPLC");

                            timePLC timeplc = new timePLC() { 
                                ID = 1, 
                                timeLed = (double)(numTimeLed.Value * 1000), 
                                velocityMotor1 = CodeValue((double)numMotor1.Value), 
                                velocityMotor2 = CodeValue((double)numMotor2.Value), 
                                percentageMotor1 = (double)(numPercentage1.Value), 
                                percentageMotor2 = (double)(numPercentage2.Value)
                            };
                            PLCDB.ReplaceOne(d => d.ID == 1, timeplc);

                            MessageBox.Show(UIMessages.PLC(3, _EnglishChecked), UIMessages.PLC(7, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (source != "")
                            {
                                throw new PasswordException();
                            }
                        }
                    }
                    
                }
            }
            catch (PasswordException exPass)
            {
                Mongoose.LoadError(exPass, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show("The password is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        // -----------------------------------
        //              Methods              |
        // -----------------------------------

        /// <summary>
        /// 
        /// </summary>
        private void InitComponentValues()
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<timePLC> PLCDB = database.GetCollection<timePLC>("MasterPLC");
                List<timePLC> lst = PLCDB.Find(d => d.ID == 1).ToList();

                numTimeLed.Minimum = (decimal)0.5;
                numTimeLed.Maximum = (decimal)1.5;
                numMotor1.Minimum = 200;
                numMotor1.Maximum = 1000;
                numMotor2.Minimum = 200;
                numMotor2.Maximum = 1000;
                numPercentage1.Minimum = 40;
                numPercentage1.Maximum = 100;
                numPercentage2.Minimum = 40;
                numPercentage2.Maximum = 100;

                numTimeLed.Value = (decimal)(lst[0].timeLed / 1000);
                numMotor1.Value = (decimal)DecodeValue(lst[0].velocityMotor1);
                numMotor2.Value = (decimal)DecodeValue(lst[0].velocityMotor2);
                numPercentage1.Value = (decimal)(lst[0].percentageMotor1);
                numPercentage2.Value = (decimal)(lst[0].percentageMotor2);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        /// <summary>
        /// Calcula las cajas por hora esperadas segun parametros.
        /// </summary>
        /// <param name="rpm">Numero de RPMs del motor</param>
        /// <param name="reduction">Reduccion de la caja del motor</param>
        /// <param name="diameter">Pitch del sprocket</param>
        /// <param name="numBoxes">Numero de cajas en la banda</param>
        /// <param name="box2box">Distancia entre caja y caja</param>
        /// <returns></returns>
        private double BoxesPerHour(double rpm, double reduction, double diameter, double numBoxes, double box2box)
        {
            double rpmAfter = rpm / reduction;
            double linealDistance = diameter * Math.PI;
            double distancePerMinute = rpmAfter * linealDistance;
            double distanceBtwBoxes = numBoxes * box2box;
            return distancePerMinute / distanceBtwBoxes * 60;
        }
        /// <summary>
        /// Calcula los RPM esperados segun parametros.
        /// </summary>
        /// <param name="boxesHour">Numero de cajas por hora</param>
        /// <param name="reduction">Reduccion de la caja del motor</param>
        /// <param name="diameter">Pitch del sprocket</param>
        /// <param name="numBoxes">Numero de cajas en la banda</param>
        /// <param name="box2box">Distancia entre caja y caja</param>
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
            return Math.Floor(Utils.Map(newValue, 0, 1390, 0, 4095));
        }
        /// <summary>
        /// Decodifica un valor.
        /// </summary>
        /// <param name="value">Valor a decodificar</param>
        /// <returns>Devuelve el valor decodificado</returns>
        private double DecodeValue(double value)
        {
            double newValue = Math.Ceiling(Utils.Map(value, 0, 4095, 0, 1390));
            return Math.Floor(BoxesPerHour(newValue, 40, 87.27, 9, 30 * 2.54));
        }

        // -----------------------------------
        //               Help                |
        // -----------------------------------

        /// <summary>
        /// Inicializa los nombre de todos los textos segun el idioma.
        /// </summary>
        private void SetUIMessage()
        {
            if (_EnglishChecked)
            {
                lblLeds.Text = Properties.ResourceUI_en_US.T028;
                btnUpdate.Text = Properties.ResourceUI_en_US.T021;
                gbVelocity.Text = Properties.ResourceUI_en_US.T029;
                lblNumMotor1.Text = string.Format(Properties.ResourceUI_en_US.T030, 1.ToString());
                lblNumMotor2.Text = string.Format(Properties.ResourceUI_en_US.T030, 2.ToString());
            }
            else
            {
                lblLeds.Text = Properties.ResourceUI_es_CO.T028;
                btnUpdate.Text = Properties.ResourceUI_es_CO.T021;
                gbVelocity.Text = Properties.ResourceUI_es_CO.T029;
                lblNumMotor1.Text = string.Format(Properties.ResourceUI_es_CO.T030, 1.ToString());
                lblNumMotor2.Text = string.Format(Properties.ResourceUI_es_CO.T030, 2.ToString());
            }
        }
        /// <summary>
        /// Boton para pedir ayuda.
        /// </summary>
        private void PLC_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.PLC(1, _EnglishChecked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gbTimer_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.PLC(4, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.PLC(8, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gbVelocity_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.PLC(5, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.PLC(8, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnUpdate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.PLC(6, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.PLC(8, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
