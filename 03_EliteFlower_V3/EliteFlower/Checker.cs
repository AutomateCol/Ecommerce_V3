using EliteFlower.Exceptions;
using EliteFlower.Methods;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class Checker : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        bool _Loopback;
        string _LoopPort;
        string _State1;
        string _State2;
        string _Station;
        //private List<string> portsAvailable = Utils.GetSerials();
        private List<string> portsAvailable = Utils.GetSerials();

        // -----------------------------------
        //               Form                |
        // -----------------------------------

        public Checker(EliteFlower frm, bool english)
        {

            _frm = frm;
            _EnglishChecked = english;
            _State1 = "";
            _State2 = "";
            _Station = "";
            _Loopback = false;
            _LoopPort = "COM";
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa la ventana
        /// </summary>
        private void Checker_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button1.Enabled = false;
            SetUIMessage();
            LoadScanners();
        }
        /// <summary>
        /// Las acciones que debe realizar cuando se cierre la ventana.
        /// </summary>
        private void Checker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialChecker.IsOpen)
                serialChecker.Close();
        }
        /// <summary>
        /// Habilita la ventana desde la cual se llamo esta ventana.
        /// </summary>
        private void Checker_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }

        // -----------------------------------
        //            Components             |
        // -----------------------------------

        /// <summary>
        /// Permite activar/desactivar la conexion al serial.
        /// </summary>
        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialChecker.IsOpen)
                {
                    serialChecker.Close();
                    cbSerial.Enabled = true;
                    button1.Enabled = false;
                    btnSerial.Text = _State1;
                    rtxtSerial.Text = "";
                }
                else
                {
                    serialChecker.PortName = cbSerial.Text;
                    serialChecker.Open();
                    cbSerial.Enabled = false;
                    button1.Enabled = true;
                    btnSerial.Text = _State2;
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = sendData();
        }

        /// <summary>
        /// Recibe los datos del serial
        /// </summary>
        private void serialChecker_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SetRichTextCallback @delegate = new SetRichTextCallback(SetRichText);
                string station = GetStation(serialChecker.PortName);
                string response = serialChecker.ReadExisting().ToString().Replace("\r", "");
                _LoopPort = serialChecker.PortName;
                if (response == "L" && _Loopback)
                {
                    rtxtSerial.BeginInvoke(@delegate, $"El puerto {_LoopPort} esta conectado");
                    _Loopback = false;
                }
                else
                {
                    string msg = $"{station} --- {response}";
                    new Task(() => rtxtSerial.BeginInvoke(@delegate, msg)).Start();
                }
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
        /// Verifica que el lector de codigo de barras se encuentre conectado.
        /// </summary>
        /// <returns></returns>
        private async Task sendData()
        {
            try
            {
                _Loopback = true;
                serialChecker.Write("L");
                await Task.Delay(100);
                if (_Loopback)
                {
                    _Loopback = false;
                    string msg = $"El lector de codigo de barras en {cbSerial.Text} no esta conectado.";
                    throw new StartNeedScannersException(msg);
                }
            }
            catch (StartNeedScannersException exScanner)
            {
                _Loopback = false;
                Mongoose.LoadError(exScanner, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(exScanner.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }

        }

        /// <summary>
        /// Carga los puertos de los escaners en una lista
        /// </summary>
        private void LoadScanners()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    if (portsAvailable.Contains(port))
                        cbSerial.Items.Add(port);
                }
                if (cbSerial.Items.Count > 0)
                    cbSerial.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Devuelve la posicion del escaner segun el nombre del puerto.
        /// </summary>
        /// <param name="portname">El nombre del puerto asociado al escaner</param>
        /// <returns>Devuelve la cadena de texto</returns>
        private string GetStation(string portname)
        {
            if (Utils.GetStageSerials(1).Contains(portname))
            {
                if (portname == Utils.GetStageSerials(1)[0])
                    return string.Format(_Station, 1.ToString(), 1.ToString());
                else
                    return string.Format(_Station, 1.ToString(), 2.ToString());
            }
            else if (Utils.GetStageSerials(2).Contains(portname))
            {
                if (portname == Utils.GetStageSerials(2)[0])
                    return string.Format(_Station, 2.ToString(), 1.ToString());
                else
                    return string.Format(_Station, 2.ToString(), 2.ToString());
            }
            else if (Utils.GetStageSerials(3).Contains(portname))
            {
                if (portname == Utils.GetStageSerials(3)[0])
                    return string.Format(_Station, 3.ToString(), 1.ToString());
                else
                    return string.Format(_Station, 3.ToString(), 2.ToString());
            }
            else if (Utils.GetStageSerials(4).Contains(portname))
            {
                if (portname == Utils.GetStageSerials(4)[0])
                    return string.Format(_Station, 4.ToString(), 1.ToString());
                else
                    return string.Format(_Station, 4.ToString(), 2.ToString());
            }
            return "NV";
        }
        /// <summary>
        /// Delegado para cambiar el texto asincronamente.
        /// </summary>
        public delegate void SetRichTextCallback(string text);
        /// <summary>
        /// Asigna el valor del texto que se le indique.
        /// </summary>
        /// <param name="text">Texto que se va asignar</param>
        private void SetRichText(string text)
        {
            this.rtxtSerial.AppendText($"{text}\n");
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
                this.Text = Properties.ResourceUI_en_US.T008;
                _State1 = Properties.ResourceUI_en_US.T025;
                _State2 = Properties.ResourceUI_en_US.T026;
                _Station = Properties.ResourceUI_en_US.T027;
                btnSerial.Text = _State1;
            }
            else
            {
                this.Text = Properties.ResourceUI_es_CO.T008;
                _State1 = Properties.ResourceUI_es_CO.T025;
                _State2 = Properties.ResourceUI_es_CO.T026;
                _Station = Properties.ResourceUI_es_CO.T027;
                btnSerial.Text = _State1;
            }
        }
        /// <summary>
        /// Boton para pedir ayuda.
        /// </summary>
        private void Checker_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.Checker(1, _EnglishChecked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbSerial_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Checker(2, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Checker(5, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnSerial_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Checker(3, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Checker(5, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void rtxtSerial_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Checker(4, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Checker(5, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
