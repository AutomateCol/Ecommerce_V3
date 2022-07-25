using EasyModbus;
using EliteFlower.Classes;
//-- OwnMethods
using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
//-- 3-parties
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#pragma warning disable CS0105 // La directiva using para 'System.Diagnostics' aparece previamente en este espacio de nombres
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
#pragma warning restore CS0105 // La directiva using para 'System.Diagnostics' aparece previamente en este espacio de nombres






namespace EliteFlower
{
    public partial class EliteFlower : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        readonly List<Image> Leds = new List<Image> {
            Properties.Resources.led100,
            Properties.Resources.led010,
            Properties.Resources.led001,
            Properties.Resources.led000,
            Properties.Resources.led111
        };
        readonly List<Image> LedsAddon = new List<Image> {
            Properties.Resources.led000,
            Properties.Resources.led100,
            Properties.Resources.led010,
            Properties.Resources.led110,
            Properties.Resources.led001,
            Properties.Resources.led011,
            Properties.Resources.led101,
            Properties.Resources.led111
        };
        readonly List<string> serialCOMS = Utils.GetSerials();

        // readonly string mongoDBConnection = "mongodb://localhost:27017";
        //private static readonly string mongoDBConnection = "mongodb://localhost:27017/maxPoolSize=50000&waitQueueSize=2000";
        private static readonly string mongoDBConnection = "mongodb://localhost:27017/waitQueueSize=5000";

        //coils
        readonly int coilEMG = 90;



        bool balanceCheck = false;
        bool _IsPaused = false;
        bool workerUpForm;
        bool workMesanin;
        bool btnconnectFlag = false;
        int timeBlinkLeds = 1000;
        int timeBlinkAddonsBand1 = 1000;
        int timeBlinkAddonsBand2 = 1000;
        private int[] Speed = new int[2];
        private List<Stage> idSelected = new List<Stage>();
        private ModbusClient modbusLuces = new ModbusClient();
        // private ModbusClient modbusElevadores = new ModbusClient();
        //private ModbusClient modbusElevadores = new ModbusClient("192.168.56.1", 502);
        private ModbusClient modbusElevadores = new ModbusClient("169.254.1.238", 502);
        readonly List<int> _ElevadoresMesanin = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        readonly int COILS_LUCES = 0x00;
        readonly int REGS_LUCES = 0x31;
        readonly int COILS_ELEVADORES = 0;
        readonly int REGS_ELEVADORES = 1000;
        readonly int HREG_Pulmon = 1002;
        public bool start_flag = false;

#pragma warning disable CS0414 // El campo 'EliteFlower._Loopback' está asignado pero su valor nunca se usa
        bool _Loopback = false;
#pragma warning restore CS0414 // El campo 'EliteFlower._Loopback' está asignado pero su valor nunca se usa
        string _LoopPort = "COM";
        msgMesanin lastPositionSorter = new msgMesanin() { Id = "VA", Pos = 4 };
        msgMesanin lastPositionEmpujador = new msgMesanin() { Id = "VA", Pos = 5 };
        private Queue<msgMesanin> llenarPulmon = new Queue<msgMesanin>();
        private Queue<msgMesanin> llenarElevador = new Queue<msgMesanin>();
        private Queue<msgMesanin> llenadoInicial = new Queue<msgMesanin>();
        readonly string patternTracking = @"1Z[A-Z0-9]{6}[0-9]{10}";
        readonly string patternOrder = @"[0-9]{11}-[0-9]{8}|E[0-9]{7}";
        readonly string patternAddon = @"[A-Z0-9]{2}\d{4}";
        Loader loader;
        bool manBalanceCheck = true;
        bool loadFlag = false;
        bool StateFlag = false;
        bool EMGFlag = false;



        private MongoClientSettings settingsMongoDB = new MongoClientSettings();

        public class msgMesanin
        {
            public string Id { get; set; }
            public int Pos { get; set; }
        }





        // -----------------------------------
        //               Form                |
        // -----------------------------------

        public EliteFlower()
        {
            Console.WriteLine("inicio");
            InitializeComponent();

        }


        private void EliteFlower_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// Llama al metodo que inicializa los componentes y detecta que pantallas secundarias tiene el PC.
            /// </summary>
            /// 


            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            workerUpForm = Mongoose.GetWorkUp();
            workMesanin = Mongoose.GetWorkMesanin();
            //Console.WriteLine(workerUpForm);
            //Console.WriteLine(workMesanin);
            btnStart.Enabled = true;
            btnResume.Visible = false;
            btnLoadStatus.Enabled = false;
            comboActual.Visible = false;
            comboNext.Visible = false;
            btnPause.Enabled = false;
            btnServoON.Enabled = true;
            gb_eworkers.Enabled = true;
            btnBalance.Enabled = true;
            gb_worker1.Enabled = true;
            gb_worker2.Enabled = true;
            gb_worker3.Enabled = true;
            gb_addon.Enabled = true;
            gb_transband.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            btnInicio.Enabled = false;
            lblMesanin.Visible = false;
            lblMesanin1.Visible = false;
            lblMesanin2.Visible = false;
            pcbFill.Visible = false;
            pcbFill.Image = pcbFill.InitialImage;
            tabControl.TabPages.Remove(tabMain);
            tabControl.TabPages.Remove(tabConfiguration);
            tabControl.TabPages.Remove(tabStatus);
            tabControl.TabPages.Remove(tabPrueba);
            ((Control)this.tabElite).Enabled = true;
            ((Control)this.tabElite).Visible = false;
            ((Control)this.tabMain).Visible = false;
            ((Control)this.tabConfiguration).Visible = false;
            ((Control)this.tabStatus).Visible = false;
            ((Control)this.tabPrueba).Visible = false;
            pictureBox1.Enabled = false;
            btnMain.Enabled = true;
            btnConfiguration.Enabled = true;
            btnStatus.Enabled = true;
            btnPrueba.Enabled = true;
            CheckOverview();
            //checkUPSTime();
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal11, SupSubtotal12, SupSubtotal13, SupTotal1 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal21, SupSubtotal22, SupSubtotal23, SupTotal2 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal31, SupSubtotal32, SupSubtotal33, SupTotal3 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal41, SupSubtotal42, SupSubtotal43, SupTotal4 });
            Mesanin();
            Mongoose.SetWorkUpFalse();
            Mongoose.SetWorkMesaninFalse();

            Mongoose.DeleteCountIDs("Data");
            Mongoose.DeleteIDAddOn();
            Mongoose.LoadCountIDs("Data", 1);
            Mongoose.GetDistinctAddOn("Data", 10);

            List<string> nameVS = FillVases(Mongoose.GetNameVases("Data"));
            List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
            Utils.SetComboBox(nameAD, new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });
            _ = CheckDB(Mongoose.GetNameVases("Data"), true);
            lblRegActual.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";
            lblPath.Text = $"{Mongoose.GetFilePath()}";
            CheckForIllegalCrossThreadCalls = false;
            CB_Template.Visible = false;
        }
        /// <summary>
        /// Saca una modal que pregunta si desea salir y si este es el caso entonces lo que hace es 
        /// desactivar la comunica de los scanners y el modbus.
        /// </summary>
        private void EliteFlower_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!Mongoose.GetRecovery())
                {
                    if (MessageBox.Show(UIMessages.EliteFlower(2, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        workerUpForm = Mongoose.GetWorkUp();
                        if (!workerUpForm)
                        {
                            DisconnectScanners(new List<SerialPort> { serialStage11, serialStage12 });
                            DisconnectScanners(new List<SerialPort> { serialStage21, serialStage22 });
                            DisconnectScanners(new List<SerialPort> { serialStage31, serialStage32 });
                            //DisconnectScanners(new List<SerialPort> { serialAD11, serialAD12 });
                            DisconnectScanners(new List<SerialPort> { serialMElev, serialPLC });
                            DisconnectModbusLuces(modbusLuces, COILS_LUCES);
                            DisconnectModbus(modbusElevadores, COILS_ELEVADORES);
                            Mongoose.DeleteBalanceNames("Data");
                            Mongoose.DeleteBalanceCount("Data");
                            Mongoose.DeleteStateStages();
                            Mongoose.DeleteIDStages();
                            Mongoose.DeleteIDAddOn();
                            Mongoose.SetInitialFeedFalse();
                            Mongoose.SetWorkMesaninFalse();
                            Mongoose.SetWorkUpFalse();
                            //-- Verifica si existen registros que se sobrepasan.
                            Mongoose.CleanLogs();
                        }
                        else
                        {
                            throw new FormCurrentExecutionException();
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    DisconnectScanners(new List<SerialPort>
                    {
                        serialStage11, serialStage12,
                        serialStage21, serialStage22,
                        serialStage31, serialStage32, 
                        //serialAD11, serialAD12, 
                        serialMElev,
                        serialPLC
                    });
                    DisconnectModbusLuces(modbusLuces, COILS_LUCES);
                    DisconnectModbus(modbusElevadores, COILS_ELEVADORES);
                }
            }
            catch (FormCurrentExecutionException exForm)
            {
                Mongoose.LoadError(exForm, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                //MessageBox.Show(UIMessages.EliteFlower(64, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string msg = $"{UIMessages.EliteFlower(64, mnuELEnglish.Checked)}\n\n{UIMessages.EliteFlower(9, mnuELEnglish.Checked)}";
                DialogResult dialogResult = MessageBox.Show(msg, UIMessages.EliteFlower(57, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    CloseStartMesanin();
                }
                else
                {
                    e.Cancel = true;
                }





            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }


        // -----------------------------------
        //            Components             |
        // -----------------------------------

        /// <summary>
        /// Le muestra al operario que se encuentra en el mesanin, como debe distribuir las cajas en las estaciones.
        /// </summary>
        public void btnStart_Click(object sender, EventArgs e)
        {
             try
            {
                //lista combo box de trabajo
                List<List<ComboBox>> comboStages = new List<List<ComboBox>>
                {
                    new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                    new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                    new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 }
                };
 
                List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };               //lista con los checkers de selección de estaciones
                int AvailableWorkers = WorkersChecked.Where(c => c).Count();                //se identifican los seleccionados
                List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
                List<int> countStages = new List<int>();
                foreach (var item in true_indexes)
                {
                    Console.WriteLine(comboStages[item][0].Items.Count);
                    countStages.Add(comboStages[item].Select(s => s.SelectedIndex).Where(s => s == (comboStages[item][0].Items.Count - 1)).Count());
                }
                Console.WriteLine("count = " + countStages.Where(s => s == 3).ToList().Count());
                Console.WriteLine("Available Workers" + AvailableWorkers);
                if (countStages.Where(s => s == 3).ToList().Count() == 0 && AvailableWorkers > 0)
                {
                    bool[] BandsChecked = new bool[] { chb_Band1.Checked, chb_Band2.Checked };

                    if (BandsChecked[0] == false && BandsChecked[1] == false)
                    {
                        throw new StartNoBandException();
                    }
                    else
                    {
                        workerUpForm = Mongoose.GetWorkUp();
                        Console.WriteLine("workupform = " + workerUpForm);

                        if (workerUpForm == false)
                        {
                            if (MessageBox.Show(UIMessages.EliteFlower(3, mnuELEnglish.Checked), UIMessages.EliteFlower(58, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                List<string> distinctIDS = new List<string>();
                                List<float> quantityIDS = new List<float>();
                                List<BalanceName> balance;
                                float balancedStages;
                                Console.WriteLine("balance check: " + balanceCheck);
                                if (balanceCheck)
                                {
                                    List<BalanceCount> balanceCount = Mongoose.GetDataBalanceCount();
                                    List<BalanceCount> balanceCountWithoutAddons = balanceCount.Where(w => w._Id <= 3).ToList();
                                    balance = Mongoose.GetDataBalanceNames("Data");
                                    quantityIDS = Utils.FillQuantity(balanceCountWithoutAddons, true_indexes);
                                    idSelected = Utils.FillSelected(comboStages, quantityIDS);
                                    distinctIDS = idSelected.Select(s => s.ID).ToList().Distinct().ToList();
                                    balancedStages = balanceCountWithoutAddons.Select(s => s.Count).Sum();
                                }
                                else
                                {
                                    List<float> valueStages = new List<float>
                                    {
                                        Int32.Parse(TxtSubtotal11.Text),
                                        Int32.Parse(TxtSubtotal12.Text),
                                        Int32.Parse(TxtSubtotal13.Text),
                                        Int32.Parse(TxtSubtotal21.Text),
                                        Int32.Parse(TxtSubtotal22.Text),
                                        Int32.Parse(TxtSubtotal23.Text),
                                        Int32.Parse(TxtSubtotal31.Text),
                                        Int32.Parse(TxtSubtotal32.Text),
                                        Int32.Parse(TxtSubtotal33.Text),
                                    };

                                    quantityIDS = Utils.GetQuantities(valueStages, true_indexes);
                                    idSelected = Utils.FillSelected(comboStages, quantityIDS);
                                    distinctIDS = idSelected.Select(s => s.ID).ToList().Distinct().ToList();
                                    balancedStages = valueStages.Sum();
                                    balance = Utils.ConvertBalance(idSelected);
                                }
                                if (CheckDB(distinctIDS, false))
                                {
                                    float disparitystages = quantityIDS.Sum();
                                    if (disparitystages == balancedStages)
                                    {
                                        List<Stage> newidSelected = CheckNames(balance, idSelected, balanceCheck);
                                        Mongoose.DeleteStateStages();
                                        Mongoose.DeleteIDStages();
                                        Mongoose.LoadStateStage(newidSelected, 1);
                                        Mongoose.LoadStateIDs();
                                        List<string> countCOMS = new List<string>();
                                        countCOMS.AddRange(SerialPort.GetPortNames());
                                        List<string> noCOMS = serialCOMS.Where(s => !countCOMS.Contains(s)).Select(s => s).ToList();
                                        PLC plc = new PLC(this, mnuELEnglish.Checked);
                                        (int, int, int, int, int) valuesPLC = plc.GetValuesPLC();

                                        //Console.WriteLine($"Tiempo pilotos - {timeBlinkLeds}");
                                        timeBlinkAddonsBand1 = (int)Math.Floor((decimal)(valuesPLC.Item4 * CalculateDelayAddon(valuesPLC.Item2) / 100));
                                        //Console.WriteLine($"Tiempo banda 1 - {timeBlinkAddonsBand1}");
                                        timeBlinkAddonsBand2 = (int)Math.Floor((decimal)(valuesPLC.Item5 * CalculateDelayAddon(valuesPLC.Item3) / 100));
                                        //Console.WriteLine($"Tiempo banda 2 - {timeBlinkAddonsBand2}");
                                        if (noCOMS.Count == 0)
                                        {
                                            modbusElevadores = new ModbusClient("169.254.1.238", 502);
                                            modbusLuces = new ModbusClient("169.254.1.236", 502);
                                            //if (modbusElevadores.Available(2) && modbusLuces.Available(2))
                                            if (!modbusLuces.Connected)
                                            //if (true)
                                            {
                                                //-- PLC Luces
                                                modbusLuces.Connect();
                                                modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, BandsChecked);
                                                modbusLuces.WriteMultipleRegisters(REGS_LUCES, new int[] { valuesPLC.Item2, valuesPLC.Item3 });
                                                //-- PLC Elevadores
                                                //modbusElevadores.Connect();

                                                btnStart.Text = "Stop";
                                                ConnectBarcodeStations();
                                                Mongoose.SetWorkUpTrue();
                                                workerUpForm = Mongoose.GetWorkUp();
                                                start_flag = true;



                                                chb_Band1.Enabled = false;
                                                chb_Band2.Enabled = false;
                                                gb_eworkers.Enabled = false;
                                                gb_worker1.Enabled = false;
                                                gb_worker2.Enabled = false;
                                                gb_worker3.Enabled = false;
                                                gb_addon.Enabled = false;
                                                gb_transband.Enabled = false;
                                                btnBalance.Enabled = false;
                                                btnPause.Enabled = true;
                                                btnInicio.Enabled = true;


                                                //-- Edits
                                                //-- Language
                                                mnuELanguage.Enabled = false;
                                                //-- Masters
                                                mnuEProducts.Enabled = false;
                                                mnuEAddOn.Enabled = false;
                                                mnuEPackage.Enabled = false;
                                                mnuEPLC.Enabled = true;
                                                //-- Serial Checkers
                                                mnuEChecker.Enabled = false;
                                                //-- Manuals
                                                mnuEMElevators.Enabled = false;
                                                mnuEMLights.Enabled = false;
                                                //start = new Start(newidSelected, BandsChecked, mnuELEnglish.Checked);
                                                //start.Show();
                                                RefreshElevator(0, -1);
                                                RefreshElevator(1, -1);
                                                RefreshElevator(2, -1);
                                                RefreshElevator2(0, -1);
                                                RefreshElevator2(1, -1);
                                                RefreshElevator2(2, -1);
                                                RefreshAddon(0, -1);
                                                RefreshAddon(1, -1);
                                                LoadValuesMatriz();
                                                //gpMatriz.Visible = true;
                                            }
                                            else
                                            {
                                                CloseStartMesanin();
                                                throw new StartNoPLCException();
                                            }
                                        }
                                        else
                                        {
                                            string msg = $"{UIMessages.EliteFlower(5, mnuELEnglish.Checked)}\n\n";
                                            foreach (string item in noCOMS)
                                            {
                                                msg += $"{item}\n";
                                            }
                                            msg += $"\n{UIMessages.EliteFlower(6, mnuELEnglish.Checked)}";
                                            
                                            //throw new StartNeedScannersException(msg);
                                        }
                                    }
                                    else
                                    {
                                        throw new StartMatchBalanceException();
                                    }
                                }
                                else
                                {
                                    _ = CheckDB(distinctIDS, true);
                                }
                            }
                        }
                        else
                        {
                            //Mensaje 
                            string msg = $"{UIMessages.EliteFlower(8, mnuELEnglish.Checked)}\n\n{UIMessages.EliteFlower(9, mnuELEnglish.Checked)}";
                            DialogResult dialogResult = MessageBox.Show(msg, UIMessages.EliteFlower(57, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                CloseStartMesanin();
                            }
                        }
                    }
                }
                else
                {
                    throw new StartNeedParametersException();
                }
            }
            catch (StartNoBandException exBand)
            {
                Mongoose.LoadError(exBand, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(4, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (StartNoPLCException exPLC)
            {
                Mongoose.LoadError(exPLC, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(65, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (StartNeedScannersException exScanners)
            {
                Mongoose.LoadError(exScanners, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(exScanners.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (StartMatchBalanceException exMatch)
            {
                Mongoose.LoadError(exMatch, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(7, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitComboBox(new List<string>(), new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13, cbWorker21, cbWorker22, cbWorker23, cbWorker31, cbWorker32, cbWorker33 });
            }
            catch (StartNeedParametersException exParameters)
            {
                Mongoose.LoadError(exParameters, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(10, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        async public void changeSpeed(int SpeedC1, int SpeedC2)
        {

            modbusLuces.WriteMultipleRegisters(REGS_LUCES, new int[] { SpeedC1, SpeedC2 });
            await Task.Delay(100);
        }
        private int CodeValue(int value)
        {
            double pwm_frequency = 3.74 * value + 180;
            int ipwm_frequency = (int)Math.Round(pwm_frequency);
            return ipwm_frequency;

        }

        /// <summary>
        /// Al dar click sobre el boton de abrir, se realiza la busqueda del archivo de Excel el cual se va analizar
        /// y sacar las columnas requeridas, luego de eso obtiene los valores unicos de los productos e inicializa
        /// los combobox con esos valores, ademas obtiene las cantidades unicas de los productos para mostrar un mensaje
        /// resumiento la informacion que el archivo contenia y por ultimo habilita mas funcionalidades que requieren 
        /// de tener este archivo cargado.
        /// </summary>
        private void btnOpen1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "Office Files|*.xls;*.xlsx" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Mongoose.DeleteEntryData("Actual");
                        Mongoose.DeleteCountIDs("Actual");
                        Mongoose.DeleteIDAddOn();
                        Mongoose.LoadExcel(ofd.FileName, "Actual");
                        Mongoose.SetFileNameML(ofd.FileName, false, lblPath.Text);
                        Mongoose.LoadCountIDs("Actual", 1);
                        Mongoose.GetDistinctAddOn("Actual", 10);
                        Mongoose.GetStatistics("Actual", mnuELEnglish.Checked);

                        List<string> nameVS = FillVases(Mongoose.GetNameVases("Actual"));
                        List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Actual"));

                        Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 });
                        Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
                        Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 });
                        Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
                        Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 });
                        Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
                        Utils.SetComboBox(nameAD, new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
                        Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
                        Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });
                        _ = CheckDB(Mongoose.GetNameVases("Actual"), true);
                        lblRegActual.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";
                        lblPath.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFilePath())}";
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Obtiene la tabla de los datos y con este saca nuevamente los valores unicos de los productos y sus cantidades
        /// para ser mostradas en un mensaje.
        /// </summary>
        private void btnStatsActual_Click(object sender, EventArgs e)
        {
            try
            {
                Mongoose.GetStatistics("Data", mnuELEnglish.Checked);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Al hacer click sobre balancear, este obtiene las estaciones seleccionadas y no seleccionadas con su respectivo
        /// indice, luego verifica la cantidad de estaciones seleccionadas para escoger que hacer segun la cantidad
        /// seleccionada entrega la carga de trabajo balanceada.
        /// </summary>
        private void btnBalance_Click(object sender, EventArgs e)
        {
            try
            {
                balanceOK();
            }
            catch (BalanceMoreStationsException exBalance)
            {
                Mongoose.LoadError(exBalance, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(exBalance.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }


        /// <summary>
        /// Pausa el sistema por si se requiere.
        /// </summary>
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";
                if (!_IsPaused)
                {
                    msg = $"{UIMessages.EliteFlower(62, mnuELEnglish.Checked)}\n\n{UIMessages.EliteFlower(63, mnuELEnglish.Checked)}";
                }
                else
                {
                    msg = UIMessages.EliteFlower(61, mnuELEnglish.Checked);
                }

                if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!_IsPaused)
                    {
                        DisconnectScanners(new List<SerialPort> { serialStage11, serialStage12, serialStage21, serialStage22, serialStage31, serialStage32, serialMElev, serialPLC });
                        btnPause.Text = UIMessages.EliteFlower(67, mnuELEnglish.Checked);
                        btnStart.Enabled = false;
                        //-- Edits
                        //-- Language
                        mnuELanguage.Enabled = false;
                        //-- Masters
                        mnuEProducts.Enabled = false;
                        mnuEAddOn.Enabled = false;
                        mnuEPackage.Enabled = false;
                        mnuEPLC.Enabled = true;
                        //-- Serial Checkers
                        mnuEChecker.Enabled = true;
                        //-- Manuals
                        mnuEMElevators.Enabled = false;
                        mnuEMLights.Enabled = true;
                        //-- **********
                        modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, new bool[] { false, false });
                        if (modbusLuces.Connected)
                        {
                            modbusLuces.Disconnect();
                        }
                        //-- **********
                        _IsPaused = true;
                    }
                    else
                    {
                        btnPause.Text = UIMessages.EliteFlower(66, mnuELEnglish.Checked);
                        btnStart.Enabled = true;
                        //-- Edits
                        //-- Language
                        mnuELanguage.Enabled = false;
                        //-- Masters
                        mnuEProducts.Enabled = false;
                        mnuEAddOn.Enabled = false;
                        mnuEPackage.Enabled = false;
                        mnuEPLC.Enabled = false;
                        //-- Serial Checkers
                        mnuEChecker.Enabled = false;
                        //-- Manuals
                        mnuEMElevators.Enabled = false;
                        mnuEMLights.Enabled = false;
                        //-- **********
                        PLC plc = new PLC(this, mnuELEnglish.Checked);
                        (int, int, int, int, int) valuesPLC = plc.GetValuesPLC();
                        timeBlinkAddonsBand1 = (int)Math.Floor((decimal)(valuesPLC.Item4 * CalculateDelayAddon(valuesPLC.Item2) / 100));
                        //Console.WriteLine(timeBlinkAddonsBand1);
                        timeBlinkAddonsBand2 = (int)Math.Floor((decimal)(valuesPLC.Item5 * CalculateDelayAddon(valuesPLC.Item3) / 100));
                        //Console.WriteLine(timeBlinkAddonsBand2);
                        modbusLuces.Connect();
                        modbusLuces.WriteMultipleRegisters(REGS_LUCES, new int[] { valuesPLC.Item2, valuesPLC.Item3 });
                        modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, new bool[] { chb_Band1.Checked, chb_Band2.Checked });
                        ConnectBarcodeStations();
                        //-- **********
                        _IsPaused = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Actualiza la imagen de la banda para observar cual es la que se tiene seleccionada.
        /// </summary>
        private void chb_Band1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImgBands();
        }
        /// <summary>
        /// Actualiza la imagen de la banda para observar cual es la que se tiene seleccionada.
        /// </summary>
        private void chb_Band2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImgBands();
        }
        /// <summary>
        /// Al deshabilitar el checker de la estacion pone todos los combobox con el valor de "NV"
        /// que representa NoValue.
        /// </summary>
        private void chb_worker1_CheckedChanged(object sender, EventArgs e)
        {
            gb_worker1.Enabled = chb_worker1.Checked;
            if (chb_worker1.Checked == false)
            {
                cbWorker11.SelectedIndex = cbWorker11.Items.Count - 1;
                cbWorker12.SelectedIndex = cbWorker12.Items.Count - 1;
                cbWorker13.SelectedIndex = cbWorker13.Items.Count - 1;
                Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
            }
        }
        /// <summary>
        /// Al deshabilitar el checker de la estacion pone todos los combobox con el valor de "NV"
        /// que representa NoValue.
        /// </summary>
        private void chb_worker2_CheckedChanged(object sender, EventArgs e)
        {
            gb_worker2.Enabled = chb_worker2.Checked;
            if (chb_worker2.Checked == false)
            {
                cbWorker21.SelectedIndex = cbWorker21.Items.Count - 1;
                cbWorker22.SelectedIndex = cbWorker22.Items.Count - 1;
                cbWorker23.SelectedIndex = cbWorker23.Items.Count - 1;
                Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
            }
        }
        /// <summary>
        /// Al deshabilitar el checker de la estacion pone todos los combobox con el valor de "NV"
        /// que representa NoValue.
        /// </summary>
        private void chb_worker3_CheckedChanged(object sender, EventArgs e)
        {
            gb_worker3.Enabled = chb_worker3.Checked;
            if (chb_worker3.Checked == false)
            {
                cbWorker31.SelectedIndex = cbWorker31.Items.Count - 1;
                cbWorker32.SelectedIndex = cbWorker32.Items.Count - 1;
                cbWorker33.SelectedIndex = cbWorker33.Items.Count - 1;
                Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker11_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker11.SelectedItem.ToString(), 0, 1))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker12_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker12.SelectedItem.ToString(), 0, 2))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker13_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker13.SelectedItem.ToString(), 0, 3))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker21_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker21.SelectedItem.ToString(), 1, 1))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker22_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker22.SelectedItem.ToString(), 1, 2))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker23_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker23.SelectedItem.ToString(), 1, 3))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker31_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker31.SelectedItem.ToString(), 2, 1))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker32_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker32.SelectedItem.ToString(), 2, 2))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio y hace un calculo manual del balanceo.
        /// </summary>
        private void cbWorker33_DropDownClosed(object sender, EventArgs e)
        {
            balanceCheck = false;
            if (Utils.manualCheck(cbWorker33.SelectedItem.ToString(), 2, 3))
            {
                //manual();
                manBalanceCheck = false;
            }
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio.
        /// </summary>
        private void cbAddon11_DropDownClosed(object sender, EventArgs e)
        {
            if (cbAddon11.SelectedItem.ToString() == cbAddon12.SelectedItem.ToString() || cbAddon11.SelectedItem.ToString() == cbAddon13.SelectedItem.ToString())
            {
                cbAddon11.SelectedItem = "NV";
            }
            List<IDAddOn> tt = Mongoose.GetListAddOn("Data");
            if (cbAddon11.SelectedItem.ToString() == "NV")
            {
                TxtSubtotal41.Text = "0";
            }
            else
            {
                List<int> tt1 = tt.Where(s => s.AddOn == cbAddon11.SelectedItem.ToString()).Select(s => s.Count).ToList();
                TxtSubtotal41.Text = $"{tt1[0]}";
            }
            RefreshAddOns();
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio.
        /// </summary>
        private void cbAddon12_DropDownClosed(object sender, EventArgs e)
        {
            if (cbAddon12.SelectedItem.ToString() == cbAddon11.SelectedItem.ToString() || cbAddon12.SelectedItem.ToString() == cbAddon13.SelectedItem.ToString())
            {
                cbAddon12.SelectedItem = "NV";
            }
            List<IDAddOn> tt = Mongoose.GetListAddOn("Data");
            if (cbAddon12.SelectedItem.ToString() == "NV")
            {
                TxtSubtotal42.Text = "0";
            }
            else
            {
                List<int> tt1 = tt.Where(s => s.AddOn == cbAddon12.SelectedItem.ToString()).Select(s => s.Count).ToList();
                TxtSubtotal42.Text = $"{tt1[0]}";
            }
            RefreshAddOns();
        }
        /// <summary>
        /// Si se realiza algun cambio de ID en algun producto el realiza el cambio.
        /// </summary>
        private void cbAddon13_DropDownClosed(object sender, EventArgs e)
        {
            if (cbAddon13.SelectedItem.ToString() == cbAddon11.SelectedItem.ToString() || cbAddon13.SelectedItem.ToString() == cbAddon12.SelectedItem.ToString())
            {
                cbAddon13.SelectedItem = "NV";
            }
            List<IDAddOn> tt = Mongoose.GetListAddOn("Data");
            if (cbAddon13.SelectedItem.ToString() == "NV")
            {
                TxtSubtotal43.Text = "0";
            }
            else
            {
                List<int> tt1 = tt.Where(s => s.AddOn == cbAddon13.SelectedItem.ToString()).Select(s => s.Count).ToList();
                TxtSubtotal43.Text = $"{tt1[0]}";
            }
            RefreshAddOns();
        }

        private async void btnCleanDB_Click(object sender, EventArgs e)
        {
            ShowLoader("Unbalance DataBase");
            Task oTask1 = new Task(UnbalanceDB_Data);
            oTask1.Start();
            await oTask1;
            HideLoader();
            manBalanceCheck = true;
            MessageBox.Show("Database Unbalanced");
        }

        private void btnCleanWorkers_Click(object sender, EventArgs e)
        {
            CleanWorkers();
        }

        private void btnClearWorkers_Click(object sender, EventArgs e)
        {
            CleanWorkers();
        }



        private void btnLoadStatus_Click(object sender, EventArgs e)
        {
            leerSensores();
        }

        // -------------------------------------
        //            Serial ports             |
        // -------------------------------------
        private void DoActionSerial(string portname, int nStage, int nBand, string entrymsg, string band, bool Tracking)
        {

            string response = Mongoose.GetSearchVase(entrymsg, nStage, band, Tracking);
            txtResponse.Text = response.ToString();
            if (response == "OFF")
            {
                txtControl.Text = "18";
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nStage * 3 + nBand, new bool[] { false, false, false });
                Mongoose.SetReadedStage(entrymsg, nStage, Tracking);
                RefreshElevator(nStage, -1);
            }
            else if (response != "ND" && response != "CK" && response != "OFF" && response != null)
            {
                txtControl.Text = "19";
                (int, int) p = GetIndexStage(portname, response, entrymsg, band, Tracking);
                if (nBand == 0)
                {
                    txtControl.Text = "20";
                    new Task(() => RefreshElevatorCount(p.Item1, p.Item2, response)).Start();
                }
                else if (nBand == 14)
                {
                    txtControl.Text = "21";
                    new Task(() => RefreshElevatorCount2(p.Item1, p.Item2, response)).Start();
                }


            }
        }

        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage11_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage11.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B1E1 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage11.PortName, 0, 0, matchTrack.Value, "A", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage11.PortName, 0, 0, matchOrder.Value, "A", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage12_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage12.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B2E1 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage12.PortName, 0, 14, matchTrack.Value, "B", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage12.PortName, 0, 14, matchOrder.Value, "B", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage21_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage21.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B1E2 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage21.PortName, 1, 0, matchTrack.Value, "A", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage21.PortName, 1, 0, matchOrder.Value, "A", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage22_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage22.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B2E2 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage22.PortName, 1, 14, matchTrack.Value, "B", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage22.PortName, 1, 14, matchOrder.Value, "B", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage31_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage31.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B1E3 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage31.PortName, 2, 0, matchTrack.Value, "A", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage31.PortName, 2, 0, matchOrder.Value, "A", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialStage32_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialStage32.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B2E3 {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerial(serialStage31.PortName, 2, 14, matchTrack.Value, "B", true);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerial(serialStage31.PortName, 2, 14, matchOrder.Value, "B", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        // Funcion que determina la acción que se debe tomar de acuerdo a la lectura del codigo de barras en la estación indicada

        private void DoActionSerialAddon(string entrymsg, bool Tracking, int nIndex, int nBand)
        {
            string responseAddon = Mongoose.GetSearchAddOn(entrymsg, Tracking);
            txtResponseAddon.Text = responseAddon.ToString();
            if (responseAddon == "OFF")
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nIndex + 14 * nBand, new bool[] { false, false, false });
                RefreshAddon(nBand, -1);
            }
            else if (responseAddon == "ND") // ND means No Data
            {
                new Task(() => SetCoilsAddonsND(nBand, 500, 500)).Start();

            }
            else if (responseAddon != "ND" && responseAddon != "CK" && responseAddon != "OFF" && responseAddon != null)
            {
                //List<string> requestAddon = responseAddon.Replace(" ", "").Split(',').ToList();
                MatchCollection matchList = Regex.Matches(responseAddon, patternAddon);
                List<string> requestAddon = matchList.Cast<Match>().Select(match => match.Value).ToList();
                if (requestAddon.Count > 0)
                {
                    Mongoose.SetAddonQuantity(requestAddon);
                    bool[] lights = LightsAddons(requestAddon);
                    new Task(() => SetCoilsAddons(nBand, 1000, lights)).Start();
                    Mongoose.SetReadedAddon(entrymsg, Tracking);
                    RefreshValuesInProcessAddon();
                    RefreshAddon(nBand, BoolArrayToInt(lights));
                }
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialAD11_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialAD11.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B1Addon {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerialAddon(matchTrack.Value, true, 9, 0);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerialAddon(matchOrder.Value, false, 9, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialAD12_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialAD12.ReadExisting().ToString();
                if (workerUpForm)
                {
                    Console.WriteLine($"B2Addon {entry}");
                    Match matchTrack = Regex.Match(entry, patternTracking);
                    if (matchTrack.Success)
                    {
                        DoActionSerialAddon(matchTrack.Value, true, 9, 1);
                    }
                    else
                    {
                        Match matchOrder = Regex.Match(entry, patternOrder);
                        if (matchOrder.Success)
                        {
                            DoActionSerialAddon(matchOrder.Value, false, 9, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        /// <summary>
        /// Recibe los datos de la lectura de los codigos de barra.
        /// </summary>
        private void serialMElev_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialMElev.ReadExisting().ToString();
                string response = entry.Replace("\r", "").Replace("\n", "");

                switch (response)
                {
                    case "A":
                        //Console.WriteLine("Prueba1");
                        break;
                    case "B":
                        //Console.WriteLine("Prueba2");
                        break;
                    case "P":
                        serialMElev.Close();
                        //Console.WriteLine("Cierra conexion serialMElev");
                        break;
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Recibe los datos del PLC.
        /// </summary>
        private void serialPLC_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string entry = serialPLC.ReadLine().ToString();
                string response = entry.Replace("\r", "").Replace("\n", "");
                _LoopPort = serialPLC.PortName;

                textBox5.Text = "hola";

                //actualizarPedidoCajaElevador();     //-- Pone en cola la tarea
                //actualizaInfoTextos();              //-- Actualiza textos
                //actualizarSCajaPulmon();
                //showNextBox2Fill();

                bool[] EMG = modbusElevadores.ReadCoils(coilEMG, 1);
                if (EMG[0])
                {
                    Console.WriteLine("Activo paro de emergencia\n");
                    //int[] tt = modbusElevadores.ReadHoldingRegisters(1002, 1);
                    //Console.WriteLine(tt[0]);
                    //modbusElevadores.Disconnect();
                    //modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, new bool[] { false, false });
                    //DisconnectScanners(new List<SerialPort> { serialStage11, serialStage12, serialStage21, serialStage22, serialStage31, serialStage32, serialPLC });
                    //llenarElevador.Clear();
                    //llenarPulmon.Clear();
                    Mongoose.SetESActiveTrue();
                    btnResume.Visible = true;
                    if (!EMGFlag)
                    {
                        EMGFlag = true;
                        MessageBox.Show(UIMessages.EliteFlower(104, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (PLCUndervoltageException exUndervoltage)
            {
                Mongoose.LoadError(exUndervoltage, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                Mongoose.SetRecovery();
                Console.WriteLine(UIMessages.EliteFlower(68, mnuELEnglish.Checked));
                Application.Exit();
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                Console.WriteLine($"{ex.Source} - {ex.Message} - {ex.StackTrace} - {ex.InnerException} - {ex.HResult}");
            }

        }
        /// <summary>
        /// Hace la conexion de los escaners segun cual banda se encuentre seleccionada.
        /// </summary>
        /// <param name="serialcodes">Lista de los puerto COM que tiene asociado el escaner</param>
        /// <param name="seriales">Lista de instancias de los puertos seriales</param>
        private void ConnectBarcodeScanners(List<string> serialcodes, List<SerialPort> seriales)
        {
            try
            {
                List<bool> bands = new List<bool>() { chb_Band1.Checked, chb_Band2.Checked };
                for (int i = 0; i < seriales.Count; i++)
                {
                    if (bands[i] && !seriales[i].IsOpen && serialcodes[i] != "NN")
                    {
                        seriales[i].PortName = serialcodes[i];
                        seriales[i].Open();
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Hace la conexion de los escaners segun cual estacion se encuentre seleccionada.
        /// </summary>
        private void ConnectBarcodeStations()
        {
            try
            {
                int ST1 = 0;
                int ST2 = 2;
                int ST3 = 4;
                int ADN = 6;
                ConnectBarcodeScanners(serialCOMS.GetRange(ADN, 2), new List<SerialPort>() { serialAD11, serialAD12 });
                if (chb_worker1.Checked)
                {
                    ConnectBarcodeScanners(serialCOMS.GetRange(ST1, 2), new List<SerialPort>() { serialStage11, serialStage12 });
                }
                if (chb_worker2.Checked)
                {
                    ConnectBarcodeScanners(serialCOMS.GetRange(ST2, 2), new List<SerialPort>() { serialStage21, serialStage22 });
                }
                if (chb_worker3.Checked)
                {
                    ConnectBarcodeScanners(serialCOMS.GetRange(ST3, 2), new List<SerialPort>() { serialStage31, serialStage32 });
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }

        }

        // -------------------------------------
        //            Balance work             |
        // -------------------------------------

        private async void balanceOK()
        {
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();
            List<int> int_indexes = new List<int> { 0, 1, 2 };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            List<int> false_indexes = int_indexes.Except(true_indexes).ToList();
            if (AvailableWorkers == 0)
            {
                string msg = UIMessages.EliteFlower(4, mnuELEnglish.Checked);
                MessageBox.Show(msg, UIMessages.EliteFlower(57, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (AvailableWorkers == 1)
                {
                    List<VaseCount> DataVase = Mongoose.GetDataVase("Data");
                    if (DataVase.Count > 3)
                    {
                        throw new BalanceMoreStationsException(string.Format(UIMessages.EliteFlower(13, mnuELEnglish.Checked), DataVase.Count));
                    }
                    else
                    {
                        List<List<ComboBox>> comboStages = new List<List<ComboBox>>
                            {
                                new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                                new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                                new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 }

                            };
                        List<string> namesIDS = DataVase.Select(s => s.Vase).ToList();
                        string msg = $"{UIMessages.EliteFlower(14, mnuELEnglish.Checked)}\n\n";
                        int ii = 1;
                        foreach (var item in namesIDS)
                        {
                            msg += string.Format(UIMessages.EliteFlower(15, mnuELEnglish.Checked), ii++, item);
                        }

                        DialogResult dialogResult = MessageBox.Show(msg, UIMessages.EliteFlower(56, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            CleanWorkers();
                            UnbalanceDB_Data();
                            comboStages[true_indexes[0]][0].SelectedIndex = comboStages[true_indexes[0]][0].Items.Count - 1;
                            comboStages[true_indexes[0]][1].SelectedIndex = comboStages[true_indexes[0]][1].Items.Count - 1;
                            comboStages[true_indexes[0]][2].SelectedIndex = comboStages[true_indexes[0]][2].Items.Count - 1;

                            for (int i = 0; i < DataVase.Count; i++)
                            {
                                comboStages[true_indexes[0]][i].SelectedIndex = i;
                            }


                            ShowLoader(UIMessages.EliteFlower(78, mnuELEnglish.Checked));
                            Task oTask2 = new Task(manual);
                            oTask2.Start();
                            await oTask2;
                            HideLoader();
                            BalanceRefreshAddon();
                            manBalanceCheck = true;
                            MessageBox.Show(UIMessages.EliteFlower(79, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);



                            //btnInicio.Enabled = true;
                        }
                    }
                }
                else

                {

                    ShowLoader(UIMessages.EliteFlower(77, mnuELEnglish.Checked));
                    Task oTask1 = new Task(LoadingBalanceData);
                    oTask1.Start();

                    await oTask1;
                    HideLoader();


                    string mytext = lblBalance.Text + $"\n\n{UIMessages.EliteFlower(18, mnuELEnglish.Checked)}";
                    DialogResult dialogResult = MessageBox.Show(mytext, UIMessages.EliteFlower(56, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {

                        CleanWorkers();
                        ShowLoader(UIMessages.EliteFlower(78, mnuELEnglish.Checked));
                        Task oTask2 = new Task(LoadingMakeChangesBalance);
                        oTask2.Start();
                        await oTask2;
                        HideLoader();
                        manBalanceCheck = true;
                        MessageBox.Show(UIMessages.EliteFlower(79, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ShowLoader("Cancelling Balance");
                        Task oTask2 = new Task(UnbalanceDB_Data);
                        oTask2.Start();
                        await oTask2;
                        HideLoader();
                        manBalanceCheck = true;
                        MessageBox.Show("Balance Cancelled");
                    }
                    //btnInicio.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Organiza la informacion para ser entregada al balanceo de las estaciones.
        /// </summary>
        /// <param name="lstOut">Lista de objetos con el ID unico y su total</param>
        /// <param name="dgv">DGV donde se va almacenar los datos de los valores</param>
        /// <param name="dgv_in">DGV donde se va almacenar los datos de los valores</param>
        /// <param name="dgv_in2">DGV donde se va almacenar los datos de los valores</param>
        /// <param name="make_change">Si va aparece el MsgBox de si desea realizar el cambio</param>
        /// <param name="combo">ComboBox donde se almacena la lista de los ID de los productos</param>
        private void LoadBalance(List<VaseCount> lstOut, bool make_change, ComboBox combo, int AvailableWorkers, string document, int init)
        {
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            List<int> ProductCount = lstOut.Select(item => item.Count).ToList();

            // MessageBox.Show("true_indexes" + true_indexes.Count.ToString() +"\nProductCount"+ProductCount.Count.ToString());
            Thread.Sleep(30);

            float[][] array2D = new float[2][];
            int rowCount = ProductCount.Count();
            for (int i = 0; i < 2; i++)
            {
                array2D[i] = new float[rowCount];
            }
            for (int i = 0; i < rowCount; i++)
            {
                array2D[0][i] = float.Parse(ProductCount[i].ToString());
                array2D[1][i] = float.Parse(i.ToString());
            }

            totalchange2(array2D, rowCount, AvailableWorkers, make_change, combo, document, init);
        }

        /// <summary>
        /// Organiza la informacion para ser entregada al balanceo de las estaciones.
        /// </summary>
        /// <param name="array2D">La matriz de flotantes donde se tienela cantidad unica y el indice del ID del producto</param>
        /// <param name="quantity">La cantidad de ID de productos que se tiene</param>
        /// <param name="places">La cantidad de estaciones activas</param>
        /// <param name="dgv_in">DGV donde se va almacenar los datos de los valores</param>
        /// <param name="dgv_in2">DGV donde se va almacenar los datos de las posiciones</param>
        /// <param name="make_change">Si va aparece el MsgBox de si desea realizar el cambio</param>
        /// <param name="combo">ComboBox donde se almacena la lista de los ID de los productos</param>
        private void totalchange2(float[][] array2D, int quantity, int places, bool make_change, ComboBox combo, string document, int init)
        {
            float[][] valuesa = new float[2][];
            for (int i = 0; i < 2; i++)
            {
                valuesa[i] = new float[places * 3];
            }
            for (int i = 0; i < quantity; i++)
            {
                valuesa[0][i] = array2D[0][i];
                valuesa[1][i] = array2D[1][i];
            }
            float maxvalue;
            float maxidprod;
            int row;
            for (int i = 0; i < (places * 3) - quantity; i++)
            {
                //tomo el primer valor
                maxvalue = valuesa[0][0];
                //y tomo su id
                maxidprod = valuesa[1][0];
                row = 0;
                for (int j = 0; j < quantity + i; j++)
                {
                    if (valuesa[0][j] > maxvalue)
                    {
                        //busco el valor mas grande junto con su id
                        maxvalue = valuesa[0][j];
                        maxidprod = valuesa[1][j];
                        row = j;
                    }
                }
                //dividimos su cantidad en 2
                valuesa[0][i + quantity] = (float)Math.Floor(maxvalue / 2);
                valuesa[1][i + quantity] = maxidprod;
                valuesa[0][row] = (float)Math.Ceiling(maxvalue / 2);
            }
            float[][] values = new float[places][];
            int[][] positions = new int[places][];
            //me encargo de acomodar la informacion de modo que
            for (int i = 0; i < places; i++)
            {
                //total change pueda hacer algo con ella
                values[i] = new float[3];
                positions[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    values[i][j] = valuesa[0][i * 3 + j];
                    positions[i][j] = (int)valuesa[1][i * 3 + j];
                }

            }
            txtControl.Text = "0.46";
            totalchange(values, positions, places, make_change, combo, document, init);
        }


        private List<int> BalanceIndexPriv;
        private List<int> StageIndexPriv;
        /// <summary>
        /// Entrega el balanceo de las estaciones de trabajo segun la cantidad de estaciones activas y el numero de productos que se tenga.
        /// </summary>
        /// <param name="values">La matriz flotante con la cantidad de productos segun ID</param>
        /// <param name="positions">La matriz de enteros del indice del nombre del ID del producto</param>
        /// <param name="places">La cantidad de estaciones activas</param>
        /// <param name="dgv_in">DGV donde se va almacenar los datos de los valores</param>
        /// <param name="dgv_in2">DGV donde se va almacenar los datos de las posiciones</param>
        /// <param name="make_change">Si va aparece el MsgBox de si desea realizar el cambio</param>
        /// <param name="combo">ComboBox donde se almacena la lista de los ID de los productos</param>
        private void totalchange(float[][] values, int[][] positions, int places, bool make_change, ComboBox combo, string document, int init)
        {
            int count = 0, sumpos;
            int[][] pos1 = new int[2][];
            int[] workers = new int[places], workers2 = new int[places];
            float[][] matrix2 = new float[places][];
            workers = BalanceWork.sortmatrix(values, places);

            for (int i = 0; i < 2; i++)
            {
                pos1[i] = new int[2];
            }
            for (int i = 0; i < places; i++)
            {
                matrix2[i] = new float[3];
            }
            pos1[0][0] = 0;
            pos1[0][1] = 0;
            pos1[1][0] = 5;
            pos1[1][1] = 0;
            sumpos = pos1[0].Sum() + pos1[1].Sum();
            while ((count < 10) & (sumpos != 0))
            {
                count += 1;
                pos1 = BalanceWork.changeb(values, places);
                values = BalanceWork.changef(values, pos1);
                positions = BalanceWork.changei(positions, pos1);
                sumpos = pos1[0].Sum() + pos1[1].Sum();
            }
            workers2 = BalanceWork.sortmatrix(values, places);

            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> int_indexes = new List<int> { 0, 1, 2 };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            List<int> false_indexes = int_indexes.Except(true_indexes).ToList();
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();

            List<BalanceQuantity> balancesQ = new List<BalanceQuantity>();
            for (int i = 0; i < places; i++)
            {
                balancesQ.Add(new BalanceQuantity()
                {
                    ID1 = values[workers2[i]][0],
                    ID2 = values[workers2[i]][1],
                    ID3 = values[workers2[i]][2],
                    Stage = true_indexes[i] + 1,
                    Amount = values[workers2[i]].Sum()
                });
            }

            Mongoose.DeleteBalanceCount(document);
            Mongoose.LoadBalanceCount(values, workers2, true_indexes, false_indexes, places, document, init);

            List<int> BalanceIndex = new List<int>();
            List<int> StageIndex = new List<int>();
            string mytext = $"{UIMessages.EliteFlower(16, mnuELEnglish.Checked)} \n";
            for (int i = 0; i < places; i++)
            {
                StageIndex.Add(true_indexes[i]);
                mytext += string.Format(UIMessages.EliteFlower(17, mnuELEnglish.Checked), (true_indexes[i] + 1));
                for (int j = 0; j <= 2; j++)
                {
                    BalanceIndex.Add(positions[workers2[i]][j]);
                    mytext += $"{combo.Items[positions[workers2[i]][j]]}, ";
                }
                mytext += "\n";
            }

            Mongoose.DeleteBalanceNames(document);
            Mongoose.LoadBalanceNames(combo, places, BalanceIndex, StageIndex, false_indexes, document, init);
            Mongoose.AddChangeBalance();

            BalanceIndexPriv = BalanceIndex;
            StageIndexPriv = StageIndex;
            lblBalance.Text = mytext;
        }

        public void LoadingMakeChangesBalance()
        {
            // HideLoader();
            // ShowLoader("BalacingAddon");
            BalanceRefreshAddon();
            txtControl.Text = "1";
            List<List<ComboBox>> comboStages = new List<List<ComboBox>>
            {
                new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 }
            };
            txtControl.Text = "2";
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();
            List<int> int_indexes = new List<int> { 0, 1, 2 };
            List<int> false_indexes = int_indexes.Except(true_indexes).ToList();
            txtControl.Text = "3";

            int valueBalance = 0;
            for (int ii = 0; ii < WorkersChecked.Count; ii++)
            {
                if (WorkersChecked[ii])
                {
                    valueBalance += (int)Math.Pow(2, ii);
                }
            }
            txtControl.Text = "4";
            List<int> BalanceIndex = BalanceIndexPriv;
            List<int> StageIndex = StageIndexPriv;
            balanceCheck = true;

            if (AvailableWorkers == 2)
            {
                comboStages[StageIndex[0]][0].SelectedIndex = BalanceIndex[0];
                comboStages[StageIndex[0]][1].SelectedIndex = BalanceIndex[1];
                comboStages[StageIndex[0]][2].SelectedIndex = BalanceIndex[2];
                comboStages[StageIndex[1]][0].SelectedIndex = BalanceIndex[3];
                comboStages[StageIndex[1]][1].SelectedIndex = BalanceIndex[4];
                comboStages[StageIndex[1]][2].SelectedIndex = BalanceIndex[5];
                comboStages[false_indexes[0]][0].SelectedIndex = comboStages[false_indexes[0]][0].Items.Count - 1;
                comboStages[false_indexes[0]][1].SelectedIndex = comboStages[false_indexes[0]][1].Items.Count - 1;
                comboStages[false_indexes[0]][2].SelectedIndex = comboStages[false_indexes[0]][2].Items.Count - 1;
            }
            else
            {
                comboStages[StageIndex[0]][0].SelectedIndex = BalanceIndex[0];
                comboStages[StageIndex[0]][1].SelectedIndex = BalanceIndex[1];
                comboStages[StageIndex[0]][2].SelectedIndex = BalanceIndex[2];
                comboStages[StageIndex[1]][0].SelectedIndex = BalanceIndex[3];
                comboStages[StageIndex[1]][1].SelectedIndex = BalanceIndex[4];
                comboStages[StageIndex[1]][2].SelectedIndex = BalanceIndex[5];
                comboStages[StageIndex[2]][0].SelectedIndex = BalanceIndex[6];
                comboStages[StageIndex[2]][1].SelectedIndex = BalanceIndex[7];
                comboStages[StageIndex[2]][2].SelectedIndex = BalanceIndex[8];
            }
            txtControl.Text = "5";
            List<BalanceCount> BalanceQuantityDB = Mongoose.GetDataBalanceCount();
            txtControl.Text = "6";
            List<float> valuesStages = Utils.GetValuesBalanced(BalanceQuantityDB, true_indexes);
            txtControl.Text = "7";
            Utils.SetValuesTxt(valuesStages, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtSubtotal31, TxtSubtotal32, TxtSubtotal33 });
            txtControl.Text = "8";
            Utils.SetValuesTxt(new List<float> { valuesStages.GetRange(0, 3).Sum(), valuesStages.GetRange(3, 3).Sum(), valuesStages.GetRange(6, 3).Sum() }, new List<TextBox> { TxtTotal1, TxtTotal2, TxtTotal3 });
            txtControl.Text = "9";
            Mongoose.SetVase2Stage(valueBalance);
            txtControl.Text = "10";
        }

        // --------------------------------
        //            Methods             |
        // --------------------------------

        /// <summary>
        /// Convierte un array booleano en un entero.
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        /// 


        public int BoolArrayToInt(bool[] bits)
        {
            int r = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    r += (int)Math.Pow(2, i);
                }
            }
            return r;
        }

        /// <summary>
        /// Actualiza las referencias del addon en sus posiciones.
        /// </summary>
        /// 
        private void CleanWorkers()
        {
            List<List<ComboBox>> comboStages = new List<List<ComboBox>>
                {
                   new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                   new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                   new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 },
                   new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 }

                };
            //List<VaseCount> DataVase = Mongoose.GetDataVase("Data");
            for (int i = 0; i < 3; i++)
            {
                comboStages[0][i].SelectedItem = "NV";
                comboStages[1][i].SelectedItem = "NV";
                comboStages[2][i].SelectedItem = "NV";
                comboStages[3][i].SelectedItem = "NV";
            }

            TxtSubtotal11.Text = "0"; TxtSubtotal12.Text = "0"; TxtSubtotal13.Text = "0";
            TxtSubtotal21.Text = "0"; TxtSubtotal22.Text = "0"; TxtSubtotal23.Text = "0";
            TxtSubtotal31.Text = "0"; TxtSubtotal32.Text = "0"; TxtSubtotal33.Text = "0";
            TxtSubtotal41.Text = "0"; TxtSubtotal42.Text = "0"; TxtSubtotal43.Text = "0";
            TxtTotal1.Text = "0"; TxtTotal2.Text = "0"; TxtTotal3.Text = "0"; TxtTotal4.Text = "0";

        }

        private void unlockManual()
        {
            List<List<ComboBox>> comboStages = new List<List<ComboBox>>
                {
                   new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                   new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                   new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 },
                   new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 }

                };


            for (int i = 0; i < 3; i++)
            {
                if ((comboStages[0][i].Text == "NV") ||
                     (comboStages[1][i].Text == "NV") ||
                     (comboStages[2][i].Text == "NV") ||
                     (comboStages[3][i].Text == "NV"))
                {
                    btnManBalance.Enabled = false;
                }
                else
                {
                    btnManBalance.Enabled = true;
                }


            }

        }
        public void LoadingBalanceData()
        {
            UnbalanceDB_Data();
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();
            List<VaseCount> DataVase = Mongoose.GetDataVase("Data");

            LoadBalance(DataVase, true, comboActual, AvailableWorkers, "Data", 1);
        }
        public void UnbalanceDB_Data()
        {
            Mongoose.UnbalanceData();
        }
        private void BalanceRefreshAddon()
        {
            List<string> tt = BalanceAddOns();
            cbAddon11.SelectedItem = tt[0];
            cbAddon12.SelectedItem = tt[1];
            cbAddon13.SelectedItem = tt[2];
            List<IDAddOn> tt1 = Mongoose.GetListAddOn("Data");
            List<int> qID1 = tt1.Where(w => w.AddOn == tt[0]).Select(s => s.Count).ToList();
            List<int> qID2 = tt1.Where(w => w.AddOn == tt[1]).Select(s => s.Count).ToList();
            List<int> qID3 = tt1.Where(w => w.AddOn == tt[2]).Select(s => s.Count).ToList();
            int qID11 = qID1.Count > 0 ? qID1[0] : 0;
            int qID12 = qID2.Count > 0 ? qID2[0] : 0;
            int qID13 = qID3.Count > 0 ? qID3[0] : 0;
            int totalIDs = qID11 + qID12 + qID13;
            TxtSubtotal41.Text = $"{qID11}";
            TxtSubtotal42.Text = $"{qID12}";
            TxtSubtotal43.Text = $"{qID13}";
            TxtTotal4.Text = $"{totalIDs}";
        }
        /// <summary>
        /// Obtiene la referencia del Vase aplicando un filtro en la DB.
        /// </summary>
        /// <param name="portname">Cadena de texto que indica el nombre del puerto</param>
        /// <param name="response">Cadena de texto que indica el codigo de la caja</param>
        /// <returns></returns>
        private (int nStage, int nElevador) GetIndexStage(string portname, string response, string serialMessage, string typeband, bool Tracking)
        {
            Random random = new Random();
            int indexSt;
            if (Utils.GetStageSerials(1).Contains(portname))
            {
                List<Stage> stage1 = idSelected.Where(s => s.StageN == 1).ToList();
                IEnumerable<int> filterStage = stage1.Select((s, index) => (s.ID == response && s.Quantity > 0) ? index : -1);
                List<int> checkStage = filterStage.Where(o => o >= 0).ToList();
                indexSt = 0;
                if (checkStage.ToList().Sum() != -3)
                {
                    if (checkStage.Count > 0)
                    {
                        int index = checkStage[random.Next(checkStage.Count)];
                        Stage selected = idSelected[indexSt * 3 + index];
                        if (selected.Quantity >= 0)
                        {
                            idSelected[indexSt * 3 + index].Quantity -= 1;
                        }
                        Mongoose.SetReadedVase(serialMessage, 0, typeband, Tracking);
                        return (indexSt, index);
                    }
                    return (indexSt, -1);
                }
                return (indexSt, -1);
            }
            if (Utils.GetStageSerials(2).Contains(portname))
            {
                List<Stage> stage2 = idSelected.Where(s => s.StageN == 2).ToList();
                IEnumerable<int> filterStage = stage2.Select((s, index) => (s.ID == response && s.Quantity > 0) ? index : -1);
                List<int> checkStage = filterStage.Where(o => o >= 0).ToList();
                indexSt = 1;
                if (checkStage.ToList().Sum() != -3)
                {
                    if (checkStage.Count > 0)
                    {
                        int index = checkStage[random.Next(checkStage.Count)];
                        Stage selected = idSelected[indexSt * 3 + index];
                        if (selected.Quantity >= 0)
                        {
                            idSelected[indexSt * 3 + index].Quantity -= 1;
                        }
                        Mongoose.SetReadedVase(serialMessage, 1, typeband, Tracking);
                        return (indexSt, index);
                    }
                    return (indexSt, -1);
                }
                return (indexSt, -1);
            }
            if (Utils.GetStageSerials(3).Contains(portname))
            {
                List<Stage> stage3 = idSelected.Where(s => s.StageN == 3).ToList();
                IEnumerable<int> filterStage = stage3.Select((s, index) => (s.ID == response && s.Quantity > 0) ? index : -1);
                List<int> checkStage = filterStage.Where(o => o >= 0).ToList();
                indexSt = 2;
                if (checkStage.ToList().Sum() != -3)
                {
                    if (checkStage.Count > 0)
                    {
                        int index = checkStage[random.Next(checkStage.Count)];
                        Stage selected = idSelected[indexSt * 3 + index];
                        if (selected.Quantity >= 0)
                        {
                            idSelected[indexSt * 3 + index].Quantity -= 1;
                        }
                        Mongoose.SetReadedVase(serialMessage, 2, typeband, Tracking);
                        return (indexSt, index);
                    }
                    return (indexSt, -1);
                }
                return (indexSt, -1);
            }
            return (-1, -1);
        }
        /// <summary>
        /// Deshabilita la comunicacion con el PLC.
        /// </summary>
        /// <param name="client">El cliente del modbus para enviar los datos</param>
        /// <param name="COILS">El registro desde donde empiezan las bobinas</param>
        private void DisconnectModbus(ModbusClient client, int COILS)
        {
            if (client.Connected)
            {
                client.WriteMultipleCoils(COILS, new bool[] { false, false, false, false, false, false, false, false, false, false, false });
                client.Disconnect();
            }
        }
        /// <summary>
        /// Deshabilita la comunicacion con el PLC.
        /// </summary>
        /// <param name="client">El cliente del modbus para enviar los datos</param>
        /// <param name="COILS">El registro desde donde empiezan las bobinas</param>
        private void DisconnectModbusLuces(ModbusClient client, int COILS_LUCES)
        {
            if (client.Connected)
            {
                client.WriteMultipleCoils(COILS_LUCES, new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });   //-- Banda 1
                client.WriteMultipleCoils(COILS_LUCES + 12, new bool[] { false, false });                                                                         //-- Bandas active
                client.WriteMultipleCoils(COILS_LUCES + 14, new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });   //-- Banda 2
                client.Disconnect();
            }
        }
        /// <summary>
        /// Genera una lista de 3 booleanos falsos y pone en verdadero el que se debe encender.
        /// </summary>
        /// <param name="nElevator">entero que indica cual elevador de la estacion se prende</param>
        /// <returns>retorna una lista de booleanos de la estacion</returns>
        public bool[] SetLights(int nElevator)
        {
            bool[] lights = new bool[] { false, false, false };
            if (nElevator != -1)
            {
                lights[nElevator] = true;
            }
            return lights;
        }
        /// <summary>
        /// Apaga y vuelve a prender las luces en el piloto indicado.
        /// </summary>
        /// <param name="nStage">Indice de la estacion</param>
        /// <param name="nElevator">Indice del elevador</param>
        /// <param name="delay">Retardo en ms</param>
        /// <returns>Una tarea asincrona</returns>
        private async Task WaitLedAsync(int nStage, int nElevator, int span, int delay, int nBand)
        {
            int writecoil = COILS_LUCES + nStage * 3 + span;
            txtControl.Text = writecoil.ToString();
            if (nBand == 1)
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nStage * 3 + span, SetLights(-1));
                RefreshElevator(nStage, -1);
                await Task.Delay(delay);
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nStage * 3 + span, SetLights(nElevator));
                RefreshElevator(nStage, nElevator);
            }
            else if (nBand == 2)
            {
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nStage * 3 + span, SetLights(-1));
                RefreshElevator2(nStage, -1);
                await Task.Delay(delay);
                modbusLuces.WriteMultipleCoils(COILS_LUCES + nStage * 3 + span, SetLights(nElevator));
                RefreshElevator2(nStage, nElevator);
            }
        }
        /// <summary>
        /// Apaga y vuelve a prender las luces en el piloto indicado.
        /// </summary>
        /// <param name="nBand">Por cual banda es</param>
        /// <param name="delay">Retardo en ms</param>
        /// <param name="lights">Luces que va encender</param>
        /// <returns>Una tarea asincrona</returns>
        private async Task WaitLedAddon(int nBand, int delay, bool[] lights)
        {
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, SetLights(-1));
            await Task.Delay(delay);
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, lights);
        }
        /// <summary>
        /// Envia como se debe poner en ON/OFF los leds que corresponder a la estacion.
        /// </summary>
        /// <param name="client">El cliente del modbus para enviar los datos</param>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevado9r</param>
        public void SetCoils(ModbusClient client, int nStage, int nElevator)
        {
            try
            {
                if (nElevator == -2)
                {
                    client.WriteMultipleCoils(COILS_LUCES + nStage * 3, new bool[] { true, true, true });
                    RefreshElevator(nStage, -2);
                }
                else if (nStage != -1 && nElevator != -1)
                {
                    txtControl.Text = "22";
                    _ = WaitLedAsync(nStage, nElevator, 0, timeBlinkLeds, 1);
                }
                else
                {
                    client.WriteMultipleCoils(COILS_LUCES + nStage * 3, new bool[] { false, false, false });
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException exConnection)
            {
                Mongoose.LoadError(exConnection, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(76, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Envia como se debe poner en ON/OFF los leds que corresponder a la estacion.
        /// </summary>
        /// <param name="client">El cliente del modbus para enviar los datos</param>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void SetCoils2(ModbusClient client, int nStage, int nElevator)
        {
            try
            {
                if (nElevator == -2)
                {
                    client.WriteMultipleCoils(COILS_LUCES + nStage * 3 + 14, new bool[] { true, true, true });
                    RefreshElevator2(nStage, -2);
                }
                else if (nStage != -1 && nElevator != -1)
                {
                    txtControl.Text = "22.1";
                    _ = WaitLedAsync(nStage, nElevator, 14, timeBlinkLeds, 2);
                }
                else
                {
                    client.WriteMultipleCoils(COILS_LUCES + nStage * 3 + 14, new bool[] { false, false, false });
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException exConnection)
            {
                Mongoose.LoadError(exConnection, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.EliteFlower(76, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lights"></param>
        public void SetCoilsAddons(int nBand, int delay, bool[] lights)
        {
            _ = WaitLedAddon(nBand, delay, lights);
        }

        public async Task WaitLedAddonND(int nBand, int delay1, int delay2)
        {
            await Task.Delay(delay1);
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, new bool[] { true, true, true });
            await Task.Delay(delay2);
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, new bool[] { false, false, false });
            await Task.Delay(delay2);
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, new bool[] { true, true, true });
            await Task.Delay(delay2);
            modbusLuces.WriteMultipleCoils(COILS_LUCES + 9 + 14 * nBand, new bool[] { false, false, false });
        }

        public void SetCoilsAddonsND(int nBand, int delay1, int delay2)
        {
            int delayND1 = (int)(delay1 * 0.9);
            int delayND2 = (int)(delay2 * 0.7);
            _ = WaitLedAddonND(nBand, delayND1, delayND2);
        }
        /// <summary>
        /// Muestra visualmente como se prenderia el led de la cabina.
        /// </summary>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void RefreshElevator(int nStage, int nElevator)
        {
            List<PictureBox> pcbLeds = new List<PictureBox> { pcbLeds11, pcbLeds21, pcbLeds31 };
            switch (nElevator)
            {
                case 0:
                    pcbLeds[nStage].Image = Leds[0];
                    break;
                case 1:
                    pcbLeds[nStage].Image = Leds[1];
                    break;
                case 2:
                    pcbLeds[nStage].Image = Leds[2];
                    break;
                case -2:
                    pcbLeds[nStage].Image = Leds[4];
                    break;
                default:
                    pcbLeds[nStage].Image = Leds[3];
                    break;
            }
        }
        /// <summary>
        /// Muestra visualmente como se prenderia el led de la cabina.
        /// </summary>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void RefreshElevator2(int nStage, int nElevator)
        {
            List<PictureBox> pcbLeds = new List<PictureBox> { pcbLeds12, pcbLeds22, pcbLeds32 };
            switch (nElevator)
            {
                case 0:
                    pcbLeds[nStage].Image = Leds[0];
                    break;
                case 1:
                    pcbLeds[nStage].Image = Leds[1];
                    break;
                case 2:
                    pcbLeds[nStage].Image = Leds[2];
                    break;
                case -2:
                    pcbLeds[nStage].Image = Leds[4];
                    break;
                default:
                    pcbLeds[nStage].Image = Leds[3];
                    break;
            }
        }
        /// <summary>
        /// Muestra visualmente como se prenderia el led de la cabina.
        /// </summary>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void RefreshAddon(int nBand, int nState)
        {
            List<PictureBox> pcbLeds = new List<PictureBox> { pcbLeds41, pcbLeds42 };
            switch (nState)
            {
                case -1:
                    pcbLeds[nBand].Image = LedsAddon[0];
                    break;
                case 1:
                    pcbLeds[nBand].Image = LedsAddon[1];
                    break;
                case 2:
                    pcbLeds[nBand].Image = LedsAddon[2];
                    break;
                case 3:
                    pcbLeds[nBand].Image = LedsAddon[3];
                    break;
                case 4:
                    pcbLeds[nBand].Image = LedsAddon[4];
                    break;
                case 5:
                    pcbLeds[nBand].Image = LedsAddon[5];
                    break;
                case 6:
                    pcbLeds[nBand].Image = LedsAddon[6];
                    break;
                case 7:
                    pcbLeds[nBand].Image = LedsAddon[7];
                    break;
                default:
                    pcbLeds[nBand].Image = LedsAddon[0];
                    break;
            }
        }
        /// <summary>
        /// Realiza el descuento de la cantidad de unidades que se tiene por elevador.
        /// </summary>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void RefreshElevatorCount(int nStage, int nElevator, string request)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");

            List<StateStage> tt = StateStageDB.Find(f => f.Status == "state").ToList();

            if (nElevator != -1)
            {
                List<StateStage> tt1 = tt.Where(w => w.StageN == (nStage + 1)).ToList();
                StateStage tt2 = tt1[nElevator];
                tt2.Quantity -= 1;
                StateStageDB.ReplaceOne(f => f._Id == tt2._Id, tt2);
                new Task(() => SetCoils(modbusLuces, nStage, nElevator)).Start();
            }
            if (nElevator == -1)
            {
                new Task(() => SetCoils(modbusLuces, nStage, -1)).Start();
                RefreshElevator(nStage, -1);
            }
            //LoadValuesMatriz();
            RefreshValuesInProcess();
        }
        /// <summary>
        /// Realiza el descuento de la cantidad de unidades que se tiene por elevador.
        /// </summary>
        /// <param name="nStage">Entero que indica el numero de la estacion</param>
        /// <param name="nElevator">Entero que indica el numero del elevador</param>
        public void RefreshElevatorCount2(int nStage, int nElevator, string request)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");

            List<StateStage> tt = StateStageDB.Find(f => f.Status == "state").ToList();

            if (nElevator != -1)
            {
                List<StateStage> tt1 = tt.Where(w => w.StageN == (nStage + 1)).ToList();
                StateStage tt2 = tt1[nElevator];
                tt2.Quantity -= 1;
                StateStageDB.ReplaceOne(f => f._Id == tt2._Id, tt2);
                new Task(() => SetCoils2(modbusLuces, nStage, nElevator)).Start();
            }
            if (nElevator == -1)
            {
                new Task(() => SetCoils2(modbusLuces, nStage, -1)).Start();
                RefreshElevator2(nStage, -1);
            }
            //LoadValuesMatriz();
            RefreshValuesInProcess();
        }
        /// <summary>
        /// Verifica si el estado del overview se encuentre falso.
        /// </summary>
        private void CheckOverview()
        {
            if (!Mongoose.GetOverview())
            {
                Overview overview = new Overview(this, mnuELEnglish.Checked);
                this.Enabled = false;
                overview.TopMost = true;
                overview.Show();
            }
        }
        /// <summary>
        /// Verifica que el estado del UPS se encuentre activo para activar el modo
        /// de recuperacion de la informacion.
        /// </summary>
        private void checkUPSTime()
        {
            if (Mongoose.GetRecovery())
            {
                string msg = UIMessages.EliteFlower(69, mnuELEnglish.Checked);
                DialogResult dialogResult = MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    SetRecoveryState();
                    Mongoose.SetRecovery();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Mongoose.SetRecovery();
                }
            }
        }
        /// <summary>
        /// Restaura la informacion para poder continuar el proceso como se encontraba anteriormente.
        /// </summary>
        private void SetRecoveryState()
        {
            lblRegActual.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";
            lblPath.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFilePath())}";
            //-- Llena los ListBox con los nombres de las referencias que se tenia en el archivo previa a la caida de energia 
            List<String> nameVS = FillVases(Mongoose.GetNameVases("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13, cbWorker21, cbWorker22, cbWorker23, cbWorker31, cbWorker32, cbWorker33 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });
            //-- poner los nombres de las referencias que se tenia antes de la caida de energia
            Mongoose.SetRecoveryNames(new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13, cbWorker21, cbWorker22, cbWorker23, cbWorker31, cbWorker32, cbWorker33 });
            //-- Llena los valores del balanceo que se tenia previamente antes de caer la energia
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            List<float> valuesStages = Utils.GetValuesBalanced(Mongoose.GetDataBalanceCount(), true_indexes);
            Utils.SetValuesTxt(valuesStages, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtSubtotal31, TxtSubtotal32, TxtSubtotal33 });
            Utils.SetValuesTxt(new List<float> { valuesStages.GetRange(0, 3).Sum(), valuesStages.GetRange(3, 3).Sum(), valuesStages.GetRange(6, 3).Sum() }, new List<TextBox> { TxtTotal1, TxtTotal2, TxtTotal3 });

            chb_worker1.Enabled = true;
            chb_worker2.Enabled = true;
            chb_worker3.Enabled = true;
            gb_worker1.Enabled = true;
            gb_worker2.Enabled = true;
            gb_worker3.Enabled = true;
            gb_addon.Enabled = true;
            gb_transband.Enabled = true;
            btnStart.Enabled = true;
            btnBalance.Enabled = true;
            //btnOpen2.Enabled = true;
            btnInicio.Enabled = true;
        }
        /// <summary>
        /// Refresca la informacion que tiene del estado para ver como se esta restando las unidades en tiempo real.
        /// </summary>
        public void LoadValuesMatriz()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> balanceCounts = database.GetCollection<BalanceCount>("BWorkQuantity");

            List<BalanceCount> _stages = balanceCounts.Find(f => f._Id != 0 && f.File == "Data").ToList();
            if (_stages.Count == 4)
            {
                List<BalanceCount> stage1 = _stages.Where(w => w.Stage == 1).ToList();
                List<BalanceCount> stage2 = _stages.Where(w => w.Stage == 2).ToList();
                List<BalanceCount> stage3 = _stages.Where(w => w.Stage == 3).ToList();
                List<BalanceCount> stage4 = _stages.Where(w => w.Stage == 4).ToList();
                Utils.SetValuesTxt(new List<float> { stage1[0].ID1, stage1[0].ID2, stage1[0].ID3, stage1[0].Count }, new List<TextBox> { SupSubtotal11, SupSubtotal12, SupSubtotal13, SupTotal1 });
                Utils.SetValuesTxt(new List<float> { stage2[0].ID1, stage2[0].ID2, stage2[0].ID3, stage2[0].Count }, new List<TextBox> { SupSubtotal21, SupSubtotal22, SupSubtotal23, SupTotal2 });
                Utils.SetValuesTxt(new List<float> { stage3[0].ID1, stage3[0].ID2, stage3[0].ID3, stage3[0].Count }, new List<TextBox> { SupSubtotal31, SupSubtotal32, SupSubtotal33, SupTotal3 });
                Utils.SetValuesTxt(new List<float> { stage4[0].ID1, stage4[0].ID2, stage4[0].ID3, stage4[0].Count }, new List<TextBox> { SupSubtotal41, SupSubtotal42, SupSubtotal43, SupTotal4 });
            }
        }

        public void RefreshValuesInProcessAddon()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<BalanceCount> balanceCounts = database.GetCollection<BalanceCount>("BWorkQuantity");

            List<BalanceCount> _stages = balanceCounts.Find(f => f._Id != 0 && f.File == "Data").ToList();
            if (_stages.Count == 4)
            {
                List<BalanceCount> stage4 = _stages.Where(w => w.Stage == 4).ToList();
                Utils.SetValuesTxt(new List<float> { stage4[0].ID1, stage4[0].ID2, stage4[0].ID3, stage4[0].Count }, new List<TextBox> { SupSubtotal41, SupSubtotal42, SupSubtotal43, SupTotal4 });
            }
        }

        public void RefreshValuesInProcess()
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<StateStage> StateStageDB = database.GetCollection<StateStage>("BWorkState");

            List<StateStage> tt = StateStageDB.Find(f => f.Status == "state").ToList();
            var stage1 = tt.Where(w => w.StageN == 1).ToList();
            var stage2 = tt.Where(w => w.StageN == 2).ToList();
            var stage3 = tt.Where(w => w.StageN == 3).ToList();
            Utils.SetValuesTxt(new List<float> { stage1[0].Quantity, stage1[1].Quantity, stage1[2].Quantity, stage1[0].Quantity + stage1[1].Quantity + stage1[2].Quantity }, new List<TextBox> { SupSubtotal11, SupSubtotal12, SupSubtotal13, SupTotal1 });
            Utils.SetValuesTxt(new List<float> { stage2[0].Quantity, stage2[1].Quantity, stage2[2].Quantity, stage2[0].Quantity + stage2[1].Quantity + stage2[2].Quantity }, new List<TextBox> { SupSubtotal21, SupSubtotal22, SupSubtotal23, SupTotal2 });
            Utils.SetValuesTxt(new List<float> { stage3[0].Quantity, stage3[1].Quantity, stage3[2].Quantity, stage3[0].Quantity + stage3[1].Quantity + stage3[2].Quantity }, new List<TextBox> { SupSubtotal31, SupSubtotal32, SupSubtotal33, SupTotal3 });
        }
        /// <summary>
        /// Esta funcion permite cerrar la ventana del mesanin y luego inicializar los valores necesarios
        /// cuando se cierre esa ventana.
        /// </summary>
        public void CloseStartMesanin()
        {
            Mongoose.SetWorkUpFalse();

            btnStart.Text = "Start";
            chb_Band1.Enabled = true;
            chb_Band2.Enabled = true;
            gb_eworkers.Enabled = true;
            gb_worker1.Enabled = true;
            gb_worker2.Enabled = true;
            gb_worker3.Enabled = true;
            gb_addon.Enabled = true;
            gb_transband.Enabled = true;
            btnBalance.Enabled = true;
            btnPause.Enabled = false;

            //-- Edits
            //-- Language
            mnuELanguage.Enabled = true;
            //-- Masters
            mnuEProducts.Enabled = true;
            mnuEAddOn.Enabled = true;
            mnuEPackage.Enabled = true;
            mnuEPLC.Enabled = true;
            //-- Serial Checkers
            mnuEChecker.Enabled = true;
            //-- Manuals
            mnuEMElevators.Enabled = true;
            mnuEMLights.Enabled = true;

            DisconnectScanners(new List<SerialPort>
            {
                serialStage11, serialStage12,
                serialStage21, serialStage22,
                serialStage31, serialStage32, 
                //serialAD11, serialAD12, 
                serialMElev,
                serialPLC
            });
            DisconnectModbus(modbusElevadores, COILS_ELEVADORES);
            DisconnectModbusLuces(modbusLuces, COILS_LUCES);
            InitComboBox(new List<string>(), new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13, cbWorker21, cbWorker22, cbWorker23, cbWorker31, cbWorker32, cbWorker33 });
            InitComboBox(new List<string>(), new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal1, TxtTotal2, TxtTotal3 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
            chb_worker1.Checked = true;
            chb_worker2.Checked = true;
            chb_worker3.Checked = true;
            //gpMatriz.Visible = false;
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal11, SupSubtotal12, SupSubtotal13, SupTotal1 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal21, SupSubtotal22, SupSubtotal23, SupTotal2 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal31, SupSubtotal32, SupSubtotal33, SupTotal3 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { SupSubtotal41, SupSubtotal42, SupSubtotal43, SupTotal4 });
            RefreshElevator(0, -1);
            RefreshElevator(1, -1);
            RefreshElevator(2, -1);
            RefreshElevator2(0, -1);
            RefreshElevator2(1, -1);
            RefreshElevator2(2, -1);
            RefreshAddon(0, -1);
            RefreshAddon(1, -1);
            Mongoose.DeleteStateStages();
            Mongoose.DeleteIDStages();

            Mongoose.UpdateReadedData();

            Mongoose.DeleteCountIDs("Data");
            Mongoose.DeleteIDAddOn();
            Mongoose.LoadCountIDs("Data", 1);
            Mongoose.GetDistinctAddOn("Data", 10);

            List<string> nameVS = FillVases(Mongoose.GetNameVases("Data"));
            List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
            Utils.SetComboBox(nameAD, new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });
            UnbalanceDB_Data();
            //Mongoose.UnbalanceData();
            MessageBox.Show(UIMessages.EliteFlower(103, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Deshabilita la conexion de los scanners.
        /// </summary>
        /// <param name="stage11">Instancia del scanner</param>
        /// <param name="stage21">Instancia del scanner</param>
        /// <param name="stage31">Instancia del scanner</param>
        private void DisconnectScanners(List<SerialPort> serialPorts)
        {
            foreach (SerialPort serial in serialPorts)
            {
                if (serial.IsOpen)
                {
                    serial.Close();
                }
            }
        }
        /// <summary>
        /// Al momento de hacer seleccionar la cantidad de bandas con las que se va a trabajar, realiza el cambio
        /// de las imagenes que tiene en los recursos.
        /// </summary>
        private void UpdateImgBands()
        {
            if (chb_Band1.Checked && chb_Band2.Checked)
            {
                pcbBandas.Image = Properties.Resources.bandas11;
            }
            else if (chb_Band1.Checked && !chb_Band2.Checked)
            {
                pcbBandas.Image = Properties.Resources.bandas10;
            }
            else if (!chb_Band1.Checked && chb_Band2.Checked)
            {
                pcbBandas.Image = Properties.Resources.bandas01;
            }
            else
            {
                pcbBandas.Image = Properties.Resources.bandas00;
            }
        }
        /// <summary>
        /// Agrega "NV" a una lista de cadenas.
        /// </summary>
        /// <param name="vases">Lista de cadenas</param>
        /// <returns>Retorna una lista de cadenas con un nuevo valor</returns>
        private List<string> FillVases(List<string> vases)
        {
            List<String> nameVS = vases.Distinct().ToList();
            nameVS.Add("NV");
            return nameVS;
        }
        /// <summary>
        /// Inicializa los ComboBox segun la lista de entrada que se genere.
        /// </summary>
        /// <param name="nameInit">Lista con los nombres a poner en el ComboBox</param>
        /// <param name="workers">Lista de ComboBox</param>
        private void InitComboBox(List<string> nameInit, List<ComboBox> workers)
        {
            int ii = 0;
            if (nameInit.Count == 0)
            {
                foreach (ComboBox worker in workers)
                {
                    worker.SelectedIndex = worker.Items.Count - 1;
                }
            }
            else
            {
                foreach (ComboBox worker in workers)
                {
                    worker.SelectedItem = nameInit[ii++];
                }
            }
        }
        /// <summary>
        /// Varifica que los ID de los productos se encuentren en la DB maestra.
        /// </summary>
        /// <param name="nameVasesFile">Lista de los ID de los productos del archivo</param>
        public bool CheckDB(List<string> nameVasesFile, bool showMsg)
        {
            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

            List<string> dbIDS = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
            List<string> noDB = new List<string>();
            foreach (string item in nameVasesFile)
            {
                if (!dbIDS.Contains(item))
                {
                    if (item != "NV")
                    {
                        noDB.Add(item);
                    }
                }
            }
            if (showMsg && noDB.Count > 0)
            {
                string msg = $"{UIMessages.EliteFlower(25, mnuELEnglish.Checked)}\n\n";
                foreach (string item in noDB)
                {
                    msg += $"ID: {item}\n";
                }
                msg += $"\n\n{UIMessages.EliteFlower(26, mnuELEnglish.Checked)}";
                MessageBox.Show(msg, UIMessages.EliteFlower(57, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                if (noDB.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Revisa que el balanceo realizado automaticamente no este diferente a lo que tienen los combobox de
        /// lo contrario despliega un MsgBox preguntando si va usar los cambios realizados en manual o si va usar
        /// el del balanceo automatico.
        /// </summary>
        /// <param name="balance">Lista con los nombres del balanceo automatico </param>
        /// <param name="idSelected">Lista con lo que tiene en los combobox</param>
        /// <returns>Retorna una lista segun si encuentra diferencia o no</returns>
        private List<Stage> CheckNames(List<BalanceName> balance, List<Stage> idSelected, bool balancedManual)
        {
            List<string> nameComboBox = new List<string>
            {
                cbWorker11.SelectedItem.ToString(),
                cbWorker12.SelectedItem.ToString(),
                cbWorker13.SelectedItem.ToString(),
                cbWorker21.SelectedItem.ToString(),
                cbWorker22.SelectedItem.ToString(),
                cbWorker23.SelectedItem.ToString(),
                cbWorker31.SelectedItem.ToString(),
                cbWorker32.SelectedItem.ToString(),
                cbWorker33.SelectedItem.ToString(),
            };

            var checkedStages = balance.OrderByDescending(ord => ord.Stage).Select(s => s.Stage - 1).Distinct().ToList();
            List<string> nameBalance = new List<string>();
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                if (checkedStages.Contains(i))
                {
                    nameBalance.Add(balance[j].ID1);
                    nameBalance.Add(balance[j].ID2);
                    nameBalance.Add(balance[j++].ID3);
                }
                else
                {
                    nameBalance.Add("NV");
                    nameBalance.Add("NV");
                    nameBalance.Add("NV");
                }
            }

            if (!nameBalance.SequenceEqual(nameComboBox) && balancedManual)
            {
                idSelected = manualBalance(idSelected, nameComboBox);
                return (idSelected);
            }
            return (idSelected);
        }
        /// <summary>
        /// Realiza el balanceo manual segun los ID que tiene estructurados en los combobox.
        /// </summary>
        /// <param name="idSelected">Lista con la tabla de los nombres que se tienen actual</param>
        /// <param name="nameComboBox">Lista con nombres que va a cambiar</param>
        /// <returns>Retorna una lista balanceada manualmente</returns>
        private List<Stage> manualBalance(List<Stage> idSelected, List<string> nameComboBox)
        {
            List<VaseCount> vaseCounts = Mongoose.GetDataVase("Data");
            List<List<string>> stages = new List<List<string>>
            {
                nameComboBox.GetRange(0, 3),
                nameComboBox.GetRange(3, 3),
                nameComboBox.GetRange(6, 3)
            };
            List<Stage> newIdSelected = new List<Stage>();
            int jj = 1;
            foreach (List<string> stage in stages)
            {
                int noNV = stage.Count(s => s == "NV");
                if (noNV == 3)
                {
                    foreach (string item in stage)
                    {
                        newIdSelected.Add(new Stage { ID = item, Check = false, Quantity = 0, StageN = jj });
                    }
                }
                else
                {
                    foreach (string item in stage)
                    {
                        if (item == "NV")
                        {
                            newIdSelected.Add(new Stage { ID = item, Check = false, Quantity = 0, StageN = jj });
                        }
                        else
                        {
                            newIdSelected.Add(new Stage { ID = item, Check = true, Quantity = 0, StageN = jj });
                        }
                    }
                }
                jj++;
            }
            List<List<Stage>> centinel = newIdSelected.GroupBy(u => u.ID).Select(grp => grp.ToList()).ToList();
            var newData = newIdSelected.GroupBy(u => u.ID).Select(grp => new { ID = grp.Key, Count = grp.Count() }).ToList();
            List<int> separateQuantity = new List<int>();
            foreach (var item in newData)
            {
                if (item.ID != "NV")
                {
                    double value = vaseCounts.Where(s => s.Vase == item.ID).Select(s => s.Count).ToList()[0];
                    if (item.Count == 1)
                    {
                        separateQuantity.Add((int)value);
                    }
                    else
                    {
                        int acum = 0;
                        for (int i = 0; i < (item.Count - 1); i++)
                        {
                            int valueFloor = (int)Math.Floor(value / item.Count);
                            separateQuantity.Add(valueFloor);
                            acum += valueFloor;
                        }
                        separateQuantity.Add((int)Math.Abs(value - acum));
                    }
                }
                else
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        separateQuantity.Add(0);
                    }
                }
            }
            List<Stage> flattenCentinel = centinel.SelectMany(i => i).ToList();
            int kk = 0;
            foreach (var item in flattenCentinel)
            {
                item.Quantity = separateQuantity[kk++];
            }

            List<string> tt = vaseCounts.Select(s => s.Vase).ToList();
            List<string> tt1 = nameComboBox.Where(s => s != "NV").Distinct().ToList();
            List<string> tt2 = tt.Except(tt1).ToList();

            string msg = $"{UIMessages.EliteFlower(29, mnuELEnglish.Checked)}\n\n";
            foreach (var item in tt2)
            {
                msg += $"{string.Format(UIMessages.EliteFlower(29, mnuELEnglish.Checked), item, vaseCounts.Where(s => s.Vase == item).Select(s => s.Count).ToList()[0])}";
            }
            MessageBox.Show(msg, UIMessages.EliteFlower(57, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return flattenCentinel.OrderBy(s => s.StageN).ToList();
        }
        /// <summary>
        /// Realiza el calculo del balanceo manual tomando en cuenta las posiciones de cada ID en el elevador.
        /// </summary>
        private void manual()
        {
            UnbalanceDB_Data();
            // Mongoose.UnbalanceData();
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();

            List<VaseCount> difVases = Mongoose.GetDataVase("Data");
            List<List<string>> stages = new List<List<string>>
            {
                new List<string> { cbWorker11.SelectedItem.ToString(), cbWorker12.SelectedItem.ToString(), cbWorker13.SelectedItem.ToString() },
                new List<string> { cbWorker21.SelectedItem.ToString(), cbWorker22.SelectedItem.ToString(), cbWorker23.SelectedItem.ToString() },
                new List<string> { cbWorker31.SelectedItem.ToString(), cbWorker32.SelectedItem.ToString(), cbWorker33.SelectedItem.ToString() },
                new List<string> { cbAddon11.SelectedItem.ToString(),  cbAddon12.SelectedItem.ToString(),  cbAddon13.SelectedItem.ToString() },
            };

            List<StageElevator> newIdSelected = new List<StageElevator>();
            int jj = 1;
            foreach (List<string> stage in stages)
            {
                int noNV = stage.Count(s => s == "NV");
                if (noNV == 3)
                {
                    for (int i = 0; i < stage.Count; i++)
                    {
                        newIdSelected.Add(new StageElevator() { ID = stage[i], Check = false, Quantity = 0, StageN = jj, ElevatorN = i + 1 });
                    }
                }
                else
                {
                    for (int i = 0; i < stage.Count; i++)
                    {
                        if (stage[i] == "NV")
                        {
                            newIdSelected.Add(new StageElevator() { ID = stage[i], Check = false, Quantity = 0, StageN = jj, ElevatorN = i + 1 });
                        }
                        else
                        {
                            newIdSelected.Add(new StageElevator() { ID = stage[i], Check = true, Quantity = 0, StageN = jj, ElevatorN = i + 1 });
                        }
                    }
                }
                jj++;
            }
            var tt = newIdSelected;
            List<List<StageElevator>> centinel = newIdSelected.GroupBy(u => u.ID).Select(grp => grp.ToList()).ToList();
            var newData = newIdSelected.GroupBy(u => u.ID).Select(grp => new { ID = grp.Key, Count = grp.Count() }).ToList();
            List<int> separateQuantity = new List<int>();
            foreach (var item in newData)
            {
                if (item.ID != "NV")
                {
                    double value = difVases.Where(s => s.Vase == item.ID).Select(s => s.Count).ToList()[0];
                    if (item.Count == 1)
                    {
                        separateQuantity.Add((int)value);
                    }
                    else
                    {
                        int acum = 0;
                        for (int i = 0; i < (item.Count - 1); i++)
                        {
                            int valueFloor = (int)Math.Floor(value / item.Count);
                            separateQuantity.Add(valueFloor);
                            acum += valueFloor;
                        }
                        separateQuantity.Add((int)Math.Abs(value - acum));
                    }
                }
                else
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        separateQuantity.Add(0);
                    }
                }
            }
            List<StageElevator> flattenCentinel = centinel.SelectMany(i => i).ToList();
            int mm = 0;
            foreach (var item in flattenCentinel)
            {
                item.Quantity = separateQuantity[mm++];
            }
            List<float> valueStage1 = flattenCentinel.Where(s => s.StageN == 1).OrderBy(s => s.ElevatorN).Select(s => s.Quantity).ToList();
            List<float> valueStage2 = flattenCentinel.Where(s => s.StageN == 2).OrderBy(s => s.ElevatorN).Select(s => s.Quantity).ToList();
            List<float> valueStage3 = flattenCentinel.Where(s => s.StageN == 3).OrderBy(s => s.ElevatorN).Select(s => s.Quantity).ToList();
            List<float> valueStages = valueStage1.Concat(valueStage2).Concat(valueStage3).ToList();

            Mongoose.DeleteBalanceCount("Data");
            Mongoose.DeleteBalanceNames("Data");
            Mongoose.ConvertStages(flattenCentinel, "Data");
            Utils.SetValuesTxt(valueStages, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtSubtotal31, TxtSubtotal32, TxtSubtotal33 });
            Utils.SetValuesTxt(new List<float> { valueStage1.Sum(), valueStage2.Sum(), valueStage3.Sum() }, new List<TextBox> { TxtTotal1, TxtTotal2, TxtTotal3 });
            RefreshAddOns();

            int valueBalance = 0;
            for (int ii = 0; ii < WorkersChecked.Count; ii++)
            {
                if (WorkersChecked[ii])
                {
                    valueBalance += (int)Math.Pow(2, ii);
                }
            }
            Mongoose.SetVase2Stage(valueBalance);
            balanceCheck = true;
        }
        /// <summary>
        /// Actualiza los campos de los Addons
        /// </summary>
        private void RefreshAddOns()
        {
            List<List<string>> addons = new List<List<string>>
            {
                new List<string> { cbAddon11.SelectedItem.ToString(), cbAddon12.SelectedItem.ToString(), cbAddon13.SelectedItem.ToString() }
            };
            Mongoose.DeleteAddons();
            Mongoose.LoadAddOns(addons, "Data");
            TxtTotal4.Text = $"{Mongoose.GetTotalAddons()}";
        }
        /// <summary>
        /// Balance los Addons.
        /// </summary>
        /// <returns>Retorna una lista balanceada manualmente</returns>
        private List<string> BalanceAddOns()
        {
            List<List<string>> addons = new List<List<string>>() { };
            List<string> addonsL = Mongoose.GetListAddOn("Data").Select(s => s.AddOn).ToList();
            switch (addonsL.Count)
            {
                case 0:
                    addons.Add(new List<string>() { "NV", "NV", "NV" });
                    break;
                case 1:
                    addonsL.Add("NV");
                    addonsL.Add("NV");
                    addons.Add(addonsL);
                    break;
                case 2:
                    addonsL.Add("NV");
                    addons.Add(addonsL);
                    break;
                case 3:
                    addons.Add(addonsL);
                    break;
                default:
                    addons.Add(addonsL.GetRange(0, 3));
                    break;
            }

            Mongoose.DeleteAddons();
            Mongoose.LoadAddOns(addons, "Data");
            return addonsL.GetRange(0, 3);
        }
        /// <summary>
        /// Indica que pilotos se deben encender segun la etiqueta que se le indica.
        /// </summary>
        public bool[] LightsAddons(List<string> response)
        {
            //string response = Mongoose.GetSearchAddOn("");
            //List<string> tt = response.Replace(" ", "").Split(',').ToList();
            bool[] lights = new bool[] { false, false, false };
            if (response.Count > 0)
            {
                List<int> idx = response.Select(s => Mongoose.GetAddons().IndexOf(s)).ToList();
                foreach (int item in idx)
                {
                    if (item != -1)
                    {
                        lights[item] = true;
                    }
                }
            }
            return lights;
        }

        // -----------------------------------
        //               Menu                |
        // -----------------------------------

        /// <summary>
        /// Muestra una ventana con la informacion acerca del software.
        /// </summary>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            About about = new About(this, mnuELEnglish.Checked);
            this.Enabled = false;
            about.Show();
        }
        /// <summary>
        /// Muestra una ventana para testear los scanners leyendo codigos de barras.
        /// </summary>
        private void mnuEChecker_Click(object sender, EventArgs e)
        {
            Checker checker = new Checker(this, mnuELEnglish.Checked);
            this.Enabled = false;
            checker.Show();
        }
        /// <summary>
        /// Muestra una ventana maestra que permite modificar los parametros al PLC.
        /// </summary>
        private void mnuEPLC_Click_1(object sender, EventArgs e)
        {
            PLC plc = new PLC(this, mnuELEnglish.Checked);
            this.Enabled = false;
            plc.Show();
        }
        /// <summary>
        /// Muestra una ventana maestra que permite hacer CRUD de los datos.
        /// </summary>
        private void mnuEProducts_Click_1(object sender, EventArgs e)
        {
            AddProduct addproduct = new AddProduct(this, mnuELEnglish.Checked);
            this.Enabled = false;
            addproduct.Show();
        }
        /// <summary>
        /// Muestra una ventana maestra que permite hacer CRUD de los datos.
        /// </summary>
        private void mnuEPackage_Click(object sender, EventArgs e)
        {
            AddPackage addpackage = new AddPackage(this, mnuELEnglish.Checked);
            this.Enabled = false;
            addpackage.MinimizeBox = false;
            addpackage.MaximizeBox = false;
            addpackage.Show();
        }
        /// <summary>
        /// Muestra una ventana que permite hacer movimientos manuales de luces y bandas
        /// </summary>
        private void mnuEMLights_Click(object sender, EventArgs e)
        {
            Luces luces = new Luces(this, mnuELEnglish.Checked);
            this.Enabled = false;
            luces.MinimizeBox = false;
            luces.MaximizeBox = false;
            luces.Show();
        }
        /// <summary>
        /// Muestra una ventana que permite hacer movimientos manuales de todos los servomotores y actuadores.
        /// </summary>
        private void mnuEMElevators_Click(object sender, EventArgs e)
        {
            Elevadores elevadores = new Elevadores(this, mnuELEnglish.Checked);
            this.Enabled = false;
            elevadores.MinimizeBox = false;
            elevadores.MaximizeBox = false;
            elevadores.Show();
        }
        /// <summary>
        /// Cambia entre Ingles/Español el idioma del software.
        /// </summary>
        private void mnuELEnglish_Click(object sender, EventArgs e)
        {
            if (mnuELEnglish.Checked)
            {
                mnuELSpanish.Checked = false;
                SetUIChanges();
            }
            else
            {
                mnuELSpanish.Checked = true;
                SetUIChanges();
            }
        }
        /// <summary>
        /// Cambia entre Ingles/Español el idioma del software.
        /// </summary>
        private void mnuELSpanish_Click(object sender, EventArgs e)
        {
            if (mnuELSpanish.Checked)
            {
                mnuELEnglish.Checked = false;
                SetUIChanges();
            }
            else
            {
                mnuELEnglish.Checked = true;
                SetUIChanges();
            }
        }
        /// <summary>
        /// Muestra una ventana con la informacion de ayuda del software.
        /// </summary>
        private void mnuHelp_Click(object sender, EventArgs e)
        {
            Overview overview = new Overview(this, mnuELEnglish.Checked);
            this.Enabled = false;
            overview.Show();
        }
        /// <summary>
        /// Muestra una ventana maestra que permite hacer CRUD de los datos.
        /// </summary>
        private void mnuEAddOn_Click_1(object sender, EventArgs e)
        {
            AddOn addOn = new AddOn(this, mnuELEnglish.Checked);
            this.Enabled = false;
            addOn.MinimizeBox = false;
            addOn.MaximizeBox = false;
            addOn.Show();
        }
        // -----------------------------------
        //             Language              |
        // -----------------------------------

        /// <summary>
        /// Realiza el cambio de los textos de Ingles/Español de todos los componentes.
        /// </summary>
        public void SetUIChanges()
        {
            try
            {
                if (mnuELEnglish.Checked)
                {
                    mnuFile.Text = Properties.ResourceUI_en_US.T001;
                    mnuFQuit.Text = Properties.ResourceUI_en_US.T002;
                    mnuEdit.Text = Properties.ResourceUI_en_US.T003;
                    mnuELanguage.Text = Properties.ResourceUI_en_US.T004;
                    mnuELEnglish.Text = Properties.ResourceUI_en_US.T005;
                    mnuELSpanish.Text = Properties.ResourceUI_en_US.T006;
                    mnuEChecker.Text = Properties.ResourceUI_en_US.T008;
                    mnuAbout.Text = Properties.ResourceUI_en_US.T009;
                    mnuHelp.Text = Properties.ResourceMsg_en_US.T121;
                    tabMain.Text = Properties.ResourceMsg_en_US.T119;
                    tabConfiguration.Text = Properties.ResourceMsg_en_US.T120;
                    mnuEMLights.Text = Properties.ResourceUI_en_US.T031;
                    mnuEMElevators.Text = Properties.ResourceUI_en_US.T032;
                    mnuEProducts.Text = Properties.ResourceUI_en_US.T007;
                    mnuEPackage.Text = Properties.ResourceUI_en_US.T023;
                    mnuFData.Text = Properties.ResourceUI_en_US.T033;
                    mnuFStatistics.Text = Properties.ResourceUI_en_US.T015;
                    mnuFDNewData.Text = Properties.ResourceUI_en_US.T034;
                    mnuFDUpdateData.Text = Properties.ResourceUI_en_US.T035;
                    mnuFDRestoreBackup.Text = Properties.ResourceUI_en_US.T036;
                    gb_eworkers.Text = Properties.ResourceUI_en_US.T010;
                    gbFeeder.Text = Properties.ResourceUI_es_CO.T045;
                    gpMatriz.Text = Properties.ResourceUI_es_CO.T046;
                    chb_worker1.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                    chb_worker2.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                    chb_worker3.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                    gb_worker1.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                    lblWorker11.Text = string.Format(Properties.ResourceUI_en_US.T012, 1);
                    lblWorker12.Text = string.Format(Properties.ResourceUI_en_US.T012, 2);
                    lblWorker13.Text = string.Format(Properties.ResourceUI_en_US.T012, 3);
                    gb_worker2.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                    lblWorker21.Text = string.Format(Properties.ResourceUI_en_US.T012, 1);
                    lblWorker22.Text = string.Format(Properties.ResourceUI_en_US.T012, 2);
                    lblWorker23.Text = string.Format(Properties.ResourceUI_en_US.T012, 3);
                    gb_worker3.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                    lblWorker31.Text = string.Format(Properties.ResourceUI_en_US.T012, 1);
                    lblWorker32.Text = string.Format(Properties.ResourceUI_en_US.T012, 2);
                    lblWorker33.Text = string.Format(Properties.ResourceUI_en_US.T012, 3);
                    gb_transband.Text = Properties.ResourceUI_en_US.T013;
                    chb_Band1.Text = string.Format(Properties.ResourceUI_en_US.T014, 1);
                    chb_Band2.Text = string.Format(Properties.ResourceUI_en_US.T014, 2);
                    btnBalance.Text = Properties.ResourceUI_en_US.T016;
                    lblStation1.Text = string.Format(Properties.ResourceUI_en_US.T011, 1);
                    lblStation2.Text = string.Format(Properties.ResourceUI_en_US.T011, 2);
                    lblStation3.Text = string.Format(Properties.ResourceUI_en_US.T011, 3);
                }
                else
                {
                    mnuFile.Text = Properties.ResourceUI_es_CO.T001;
                    mnuFQuit.Text = Properties.ResourceUI_es_CO.T002;
                    mnuEdit.Text = Properties.ResourceUI_es_CO.T003;
                    mnuELanguage.Text = Properties.ResourceUI_es_CO.T004;
                    mnuELEnglish.Text = Properties.ResourceUI_es_CO.T005;
                    mnuELSpanish.Text = Properties.ResourceUI_es_CO.T006;
                    mnuEChecker.Text = Properties.ResourceUI_es_CO.T008;
                    mnuAbout.Text = Properties.ResourceUI_es_CO.T009;
                    mnuHelp.Text = Properties.ResourceMsg_es_CO.T121;
                    tabMain.Text = Properties.ResourceMsg_es_CO.T119;
                    tabConfiguration.Text = Properties.ResourceMsg_es_CO.T120;
                    mnuEMLights.Text = Properties.ResourceUI_es_CO.T031;
                    mnuEMElevators.Text = Properties.ResourceUI_es_CO.T032;
                    mnuEProducts.Text = Properties.ResourceUI_es_CO.T007;
                    mnuEPackage.Text = Properties.ResourceUI_es_CO.T023;
                    mnuFData.Text = Properties.ResourceUI_es_CO.T033;
                    mnuFStatistics.Text = Properties.ResourceUI_es_CO.T015;
                    mnuFDNewData.Text = Properties.ResourceUI_es_CO.T034;
                    mnuFDUpdateData.Text = Properties.ResourceUI_es_CO.T035;
                    mnuFDRestoreBackup.Text = Properties.ResourceUI_es_CO.T036;
                    gb_eworkers.Text = Properties.ResourceUI_es_CO.T010;
                    gbFeeder.Text = Properties.ResourceUI_es_CO.T045;
                    gpMatriz.Text = Properties.ResourceUI_es_CO.T046;
                    chb_worker1.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                    chb_worker2.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                    chb_worker3.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                    gb_worker1.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                    lblWorker11.Text = string.Format(Properties.ResourceUI_es_CO.T012, 1);
                    lblWorker12.Text = string.Format(Properties.ResourceUI_es_CO.T012, 2);
                    lblWorker13.Text = string.Format(Properties.ResourceUI_es_CO.T012, 3);
                    gb_worker2.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                    lblWorker21.Text = string.Format(Properties.ResourceUI_es_CO.T012, 1);
                    lblWorker22.Text = string.Format(Properties.ResourceUI_es_CO.T012, 2);
                    lblWorker23.Text = string.Format(Properties.ResourceUI_es_CO.T012, 3);
                    gb_worker3.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                    lblWorker31.Text = string.Format(Properties.ResourceUI_es_CO.T012, 1);
                    lblWorker32.Text = string.Format(Properties.ResourceUI_es_CO.T012, 2);
                    lblWorker33.Text = string.Format(Properties.ResourceUI_es_CO.T012, 3);
                    gb_transband.Text = Properties.ResourceUI_es_CO.T013;
                    chb_Band1.Text = string.Format(Properties.ResourceUI_es_CO.T014, 1);
                    chb_Band2.Text = string.Format(Properties.ResourceUI_es_CO.T014, 2);
                    btnBalance.Text = Properties.ResourceUI_es_CO.T016;
                    lblStation1.Text = string.Format(Properties.ResourceUI_es_CO.T011, 1);
                    lblStation2.Text = string.Format(Properties.ResourceUI_es_CO.T011, 2);
                    lblStation3.Text = string.Format(Properties.ResourceUI_es_CO.T011, 3);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        // -----------------------------------
        //               Help                |
        // -----------------------------------

        /// <summary>
        /// Boton para pedir ayuda.
        /// </summary>
        private void EliteFlower_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.EliteFlower(1, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gb_eworkers_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(45, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gb_worker1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(46, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gb_worker2_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(46, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gb_worker3_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(46, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gb_transband_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(47, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnStart_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(54, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnPause_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.EliteFlower(60, mnuELEnglish.Checked);
            MessageBox.Show(msg, UIMessages.EliteFlower(59, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //--------------------------------------------------------------------------------


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

        private int CalculateDelayAddon(int _CodedBoxPerHour)
        {
            //calcula la velocidad de las bandas
            int _decodeBandVel = (int)Math.Floor(Utils.Map(_CodedBoxPerHour, 0, 4095, 0, 1390));
            int _boxPerHourBandVel = (int)Math.Floor(BoxesPerHour(_decodeBandVel, 40, 87.27, 9, 30 * 2.54));
            if (_boxPerHourBandVel > 837)
            {
                return (int)(4113);
            }
            else if (_boxPerHourBandVel < 0)
            {
                return (int)(0);
            }
            else
            {
                return (int)(5.0085 * _boxPerHourBandVel - 78.429);
            }
        }

        private void GetDataSummary()
        {
            (int, List<VaseCount>) tt = Mongoose.GetSummaryProduction();
            string msg = string.Format(UIMessages.EliteFlower(105, mnuELEnglish.Checked), tt.Item1);
            if (tt.Item1 > 0)
            {
                msg += "\n\n";
                foreach (VaseCount item in tt.Item2)
                {
                    msg += $"{string.Format(UIMessages.EliteFlower(19, mnuELEnglish.Checked), item.Vase, item.Count)}\n";
                }
            }
            MessageBox.Show(msg, UIMessages.EliteFlower(56, mnuELEnglish.Checked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Mesanin()
        {
            pcbMesanin.Controls.Add(pcbMesaninB10);
            pcbMesanin.Controls.Add(pcbMesaninB11);
            pcbMesanin.Controls.Add(pcbMesaninB12);
            pcbMesanin.Controls.Add(pcbMesaninB13);
            pcbMesanin.Controls.Add(pcbMesaninB14);
            pcbMesanin.Controls.Add(pcbMesaninB15);
            pcbMesanin.Controls.Add(pcbMesaninB16);
            pcbMesanin.Controls.Add(pcbMesaninB17);
            pcbMesanin.Controls.Add(pcbMesaninB18);
            pcbMesanin.Controls.Add(pcbMesaninB19);
            pcbMesanin.Controls.Add(pcbMesaninL10);
            pcbMesanin.Controls.Add(pcbMesaninL11);
            pcbMesanin.Controls.Add(pcbMesaninL12);
            pcbMesanin.Controls.Add(pcbMesaninL13);
            pcbMesanin.Controls.Add(pcbMesaninL14);
            pcbMesanin.Controls.Add(pcbMesaninL15);
            pcbMesanin.Controls.Add(pcbMesaninL16);
            pcbMesanin.Controls.Add(pcbMesaninL17);
            pcbMesanin.Controls.Add(pcbMesaninL18);
            pcbMesanin.Controls.Add(pcbMesaninL19);
            pcbMesanin.Controls.Add(pcbMesaninLeft);
            pcbMesanin.Controls.Add(pcbMesaninRight);

            pcbMesaninB10.Location = new Point(262, 138);
            pcbMesaninB11.Location = new Point(44, 40);
            pcbMesaninB12.Location = new Point(95, 40);
            pcbMesaninB13.Location = new Point(146, 40);
            pcbMesaninB14.Location = new Point(210, 40);
            pcbMesaninB15.Location = new Point(262, 40);
            pcbMesaninB16.Location = new Point(314, 40);
            pcbMesaninB17.Location = new Point(377, 40);
            pcbMesaninB18.Location = new Point(429, 40);
            pcbMesaninB19.Location = new Point(481, 40);
            pcbMesaninL10.Location = new Point(270, 118);
            pcbMesaninL11.Location = new Point(52, 20);
            pcbMesaninL12.Location = new Point(103, 20);
            pcbMesaninL13.Location = new Point(154, 20);
            pcbMesaninL14.Location = new Point(218, 20);
            pcbMesaninL15.Location = new Point(270, 20);
            pcbMesaninL16.Location = new Point(322, 20);
            pcbMesaninL17.Location = new Point(385, 20);
            pcbMesaninL18.Location = new Point(437, 20);
            pcbMesaninL19.Location = new Point(489, 20);
            pcbMesaninLeft.Location = new Point(222, 150);
            pcbMesaninRight.Location = new Point(312, 150);

            pcbMesaninB10.BackColor = Color.Transparent;
            pcbMesaninB11.BackColor = Color.Transparent;
            pcbMesaninB12.BackColor = Color.Transparent;
            pcbMesaninB13.BackColor = Color.Transparent;
            pcbMesaninB14.BackColor = Color.Transparent;
            pcbMesaninB15.BackColor = Color.Transparent;
            pcbMesaninB16.BackColor = Color.Transparent;
            pcbMesaninB17.BackColor = Color.Transparent;
            pcbMesaninB18.BackColor = Color.Transparent;
            pcbMesaninB19.BackColor = Color.Transparent;
            pcbMesaninL10.BackColor = Color.Transparent;
            pcbMesaninL11.BackColor = Color.Transparent;
            pcbMesaninL12.BackColor = Color.Transparent;
            pcbMesaninL13.BackColor = Color.Transparent;
            pcbMesaninL14.BackColor = Color.Transparent;
            pcbMesaninL15.BackColor = Color.Transparent;
            pcbMesaninL16.BackColor = Color.Transparent;
            pcbMesaninL17.BackColor = Color.Transparent;
            pcbMesaninL18.BackColor = Color.Transparent;
            pcbMesaninL19.BackColor = Color.Transparent;
            pcbMesaninLeft.BackColor = Color.Transparent;
            pcbMesaninRight.BackColor = Color.Transparent;
        }

        private void showNextBox2Fill()
        {
            if (llenadoInicial.Count > 0)
            {
                msgMesanin primerPulmon = llenadoInicial.Peek();
                string url = Mongoose.ShowImageMesanin(primerPulmon.Id);
                if (url != "ND")
                {
                    FileStream fs = File.OpenRead(url);
                    pcbFill.Image = Image.FromStream(fs);
                    fs.Close();
                }
                else
                {
                    pcbFill.Image = pcbFill.InitialImage;
                }
                lblMesanin.Text = $"{string.Format(UIMessages.EliteFlower(74, mnuELEnglish.Checked), primerPulmon.Id, primerPulmon.Pos + 1)}\n{string.Format(UIMessages.EliteFlower(106, mnuELEnglish.Checked), llenadoInicial.Count)}";
            }
            else
            {
                pcbFill.Image = pcbFill.InitialImage;
                lblMesanin.Text = $"{string.Format(UIMessages.EliteFlower(74, mnuELEnglish.Checked), "NV", -1)}\n{string.Format(UIMessages.EliteFlower(106, mnuELEnglish.Checked), 0)}";
            }
        }

        ///revisa contra la base de datos las estaciones que deben cambiar los vases en comparacion al archivo anterior.
        private List<int> initialFillQueue(bool _initialFeed)
        {
            llenadoInicial.Clear();
            string[] _lastReferences = (_initialFeed) ? Mongoose.GetLastReferences() : new string[] { "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV", "NV" };
            Mongoose.DeleteMReferences(1);
            List<string> references2Fill = Mongoose.InitMesanin(_lastReferences);
            if (_initialFeed == false)
            {
                List<int> references2FillIndexs = references2Fill.Select((value, index) => (value != "NV") ? index : -1).Where(o => o >= 0).ToList();
                List<string> references2FillNames = references2Fill.Where(o => o != "NV").ToList();
                setColaMesanin(llenadoInicial, references2FillNames, references2FillIndexs);
                return new List<int>();
            }
            else
            {
                llenadoInicial.Clear();
                List<string> valuesdifferents = new List<string>();
                for (int kk = 0; kk < 9; kk++)
                {
                    if (_lastReferences[kk] != references2Fill[kk])
                    {
                        valuesdifferents.Add(references2Fill[kk]);
                    }
                    else
                    {
                        valuesdifferents.Add("NV");
                    }
                }
                return valuesdifferents.Select((value, index) => (value != "NV") ? index : -1).Where(o => o >= 0).ToList();
            }
        }

        /// <summary>
        /// Da el inicio del proceso en la parte del llenado.
        /// </summary>
        private void btnInicio_Click(object sender, EventArgs e)
        {
            try
            {
                Mongoose.SetInitialFeedTrue();
                bool initialFeed = Mongoose.GetInitialFeed();
                workMesanin = Mongoose.GetWorkMesanin();
                if (workMesanin == false && balanceCheck)
                {
                    string msg = UIMessages.EliteFlower(70, mnuELEnglish.Checked);
                    if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (modbusElevadores.Available(2))
                        {
                            List<int> posiciones2unfill = initialFillQueue(initialFeed);
                            if (initialFeed == false)
                            {
                                btnInicio.Text = UIMessages.EliteFlower(72, mnuELEnglish.Checked);
                                if (llenadoInicial.Count > 0)
                                {
                                    showNextBox2Fill();
                                    //lblMesanin.Visible = true;
                                    pcbInitialFill.Visible = true;
                                    serialPLC.PortName = "COM42";
                                    serialPLC.Open();
                                    Mongoose.SetWorkMesaninTrue();
                                    workMesanin = Mongoose.GetWorkMesanin();
                                    modbusElevadores.Connect();

                                    leerSensores();
                                    //actualizaInfoTextos();              //-- Actualiza textos
                                    ShowState();
                                    btnConnect.Enabled = false;
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    //btnLoadStatus.Enabled = true;
                                    //coils para habilitar las estaciones 76,77,78
                                    modbusElevadores.WriteMultipleCoils(COILS_ELEVADORES + 76, new bool[] { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked });
                                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 51, true);
                                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, false);
                                }
                                else
                                {
                                    //TODO
                                    Mongoose.SetWorkMesaninFalse();
                                    workMesanin = Mongoose.GetWorkMesanin();
                                    MessageBox.Show(UIMessages.EliteFlower(100, mnuELEnglish.Checked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // Throw error
                                }
                            }
                            else
                            {
                                //TODO
                                string msg2show = $"{UIMessages.EliteFlower(101, mnuELEnglish.Checked)}\n";
                                foreach (int item in posiciones2unfill)
                                {
                                    msg2show += $"{String.Format(UIMessages.EliteFlower(102, mnuELEnglish.Checked), item + 1)}\n";
                                }
                                if (MessageBox.Show(msg2show, "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                                {
                                    llenadoInicial.Clear();
                                    Mongoose.SetInitialFeedTrue();
                                    Mongoose.SetWorkMesaninTrue();
                                    workMesanin = Mongoose.GetWorkMesanin();

                                    //lblMesanin1.Visible = true;
                                    lblMesanin2.Visible = true;

                                    pcbFill.Visible = true;
                                    btnInicio.Text = "Stop";

                                    serialPLC.PortName = "COM42";
                                    serialPLC.Open();

                                    modbusElevadores.Connect();
                                    leerSensores();
                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                    btnConnect.Enabled = false;
                                    //btnLoadStatus.Enabled = true;
                                    ShowState();

                                    modbusElevadores.WriteMultipleCoils(COILS_ELEVADORES + 76, new bool[] { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked });
                                    modbusElevadores.WriteMultipleRegisters(REGS_ELEVADORES, new int[] { 15, 15 });
                                    modbusElevadores.WriteMultipleCoils(50, new bool[] { false, true });    //- InitialFill - Automatico
                                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 51, true);
                                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, false);
                                    //int[] tt = modbusElevadores.ReadHoldingRegisters(1003, 1);
                                    //int[] tt = modbusElevadores.ReadHoldingRegisters(REGS_SENSORES_ELEVADORES, 1);
                                    //string s = Convert.ToString(tt[0], 1); //Convert to binary in a string
                                    //textBox1.Text = s;
                                }
                            }
                        }
                    }
                }
                else if (workMesanin == true)
                {
                    string msg = UIMessages.EliteFlower(71, mnuELEnglish.Checked);
                    if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        btnInicio.Text = UIMessages.EliteFlower(73, mnuELEnglish.Checked);
                        lblMesanin.Visible = false;
                        pcbInitialFill.Visible = false;
                        pcbInitialFill.Image = pcbFill.InitialImage;

                        lblMesanin1.Visible = false;
                        lblMesanin2.Visible = false;

                        pcbFill.Visible = false;
                        btnInicio.Text = "Init";
                        btnLoadStatus.Enabled = false;
                        if (modbusElevadores.Connected)
                        {
                            modbusElevadores.WriteMultipleCoils(50, new bool[] { false, false });   //- InitialFill - Automatico
                            modbusElevadores.Disconnect();
                        }
                        if (serialPLC.IsOpen)
                        {
                            serialPLC.Close();
                        }
                        llenarElevador.Clear();
                        llenarPulmon.Clear();
                        llenadoInicial.Clear();
                        Mongoose.SetWorkMesaninFalse();
                        workMesanin = Mongoose.GetWorkMesanin();
                    }
                }
                else if (balanceCheck == false)
                {
                    string msg = UIMessages.EliteFlower(99, mnuELEnglish.Checked);
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"{ex.Source} - {ex.Message} - {ex.StackTrace} - {ex.InnerException} - {ex.HResult}");
            }
        }

        public MLlenado getInfoMesanin()
        {

            MongoClient client = new MongoClient(mongoDBConnection);
            IMongoDatabase database = client.GetDatabase("EliteFlower");
            IMongoCollection<MLlenado> MReferenceDB = database.GetCollection<MLlenado>("BWorkMesanin");
            var tt = MReferenceDB.Find(s => s.ID == 1).ToList();
            return tt[0];
        }

        private void actualizaInfoTextos()
        {
            setInfoPulmon(llenarPulmon);
            // setInfoElevadores(llenarElevador);
        }

        private void setInfoPulmon(Queue<msgMesanin> llenar)
        {

            int[] tt = modbusElevadores.ReadHoldingRegisters(HREG_Pulmon, 1);
            string s = tt[0].ToString();




            if (llenar.Count == 0)
            {
                lblMesanin1.Text = String.Format(UIMessages.EliteFlower(97, mnuELEnglish.Checked), llenar.Count);
                lblMesanin2.Visible = false;
                pcbFill.Image = pcbFill.InitialImage;
            }
            else
            {

                lblMesanin1.Text = String.Format(UIMessages.EliteFlower(97, mnuELEnglish.Checked), llenar.Count);
                // lblMesanin2.Text = String.Format(UIMessages.EliteFlower(98, mnuELEnglish.Checked), llenar.Peek().Id, llenar.Peek().Pos + 1);
                lblMesanin2.Text = String.Format(UIMessages.EliteFlower(98, mnuELEnglish.Checked), s);
                string url = Mongoose.ShowImageMesanin(llenar.Peek().Id);
                if (url != "ND")
                {
                    FileStream fs = File.OpenRead(url);
                    pcbFill.Image = Image.FromStream(fs);
                    fs.Close();
                }
                else
                {
                    pcbFill.Image = pcbFill.InitialImage;
                }
                lblMesanin2.Visible = true;
            }
        }

        //private void setInfoElevadores(Queue<msgMesanin> llenar)
        //{
        //    if (llenar.Count == 0)
        //    {
        //        lblMesanin3.Text = String.Format(UIMessages.EliteFlower(95, mnuELEnglish.Checked), llenar.Count);
        //        lblMesanin4.Visible = false;
        //    }
        //    else
        //    {
        //        lblMesanin3.Text = String.Format(UIMessages.EliteFlower(95, mnuELEnglish.Checked), llenar.Count);
        //        lblMesanin4.Text = String.Format(UIMessages.EliteFlower(96, mnuELEnglish.Checked), llenar.Peek().Pos + 1, llenar.Peek().Id);
        //        lblMesanin4.Visible = true;
        //    }
        //}

        private void setColaMesanin(Queue<msgMesanin> llenar, List<string> vases, List<int> indexs)
        {
            List<int> checker = llenar.Select(s => s.Pos).ToList();
            if (checker.Count == 0)
            {
                for (int i = 0; i < vases.Count; i++)
                {
                    llenar.Enqueue(new msgMesanin() { Id = vases[i], Pos = indexs[i] });
                }
            }
            else
            {
                for (int i = 0; i < vases.Count; i++)
                {
                    if (!checker.Contains(indexs[i]))
                    {
                        llenar.Enqueue(new msgMesanin() { Id = vases[i], Pos = indexs[i] });
                    }
                }
            }
        }





        private void mnuFQuit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void mnuFDNewData_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = UIMessages.EliteFlower(82, mnuELEnglish.Checked);
                if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "Office Files|*.xls;*.xlsx" })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {

                            lblData.Text = ofd.FileName;
                            string FilePath = System.IO.Path.GetFullPath(ofd.FileName);
                            lblPath.Text = FilePath;
                            Console.Write("File path is : ");
                            Console.WriteLine(FilePath);
                            ShowLoader(UIMessages.EliteFlower(83, mnuELEnglish.Checked));
                            Task oTask = new Task(LoadingNewData);
                            oTask.Start();
                            await oTask;
                            HideLoader();
                            MessageBox.Show(UIMessages.EliteFlower(84, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        public void LoadingNewData()
        {
            string ofdFilename = lblData.Text;
            Mongoose.DeleteEntryData("DataBackup");
            Mongoose.DeleteEntryData("DataSummaryBackup");
            Mongoose.BackupEntryData();
            Mongoose.BackupEntryDataSummary();
            Mongoose.DeleteEntryData("Data");
            Mongoose.DeleteEntryData("DataSummary");

            //check data flags
            List<int> lista = Mongoose.LoadExcel(ofdFilename, "Data");
            Console.WriteLine("mi lista es: ");
            Console.WriteLine(lista[0]);
            //----------------------------------------------------------------------//


            //Mongoose.LoadExcel(ofdFilename, "Data");

            Mongoose.SetFileNameML(ofdFilename, false, lblPath.Text);

            Mongoose.DeleteCountIDs("Data");
            Mongoose.DeleteIDAddOn();
            Mongoose.LoadCountIDs("Data", 1);
            Mongoose.GetDistinctAddOn("Data", 10);

            List<string> nameVS = FillVases(Mongoose.GetNameVases("Data"));
            List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
            Utils.SetComboBox(nameAD, new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });

            _ = CheckDB(Mongoose.GetNameVases("Data"), true); // REVISAR FUNCIÓN 

            lblRegActual.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";

            if (lista[0] != 1 && lista[1] != 1 && lista[2] != 1)
            {
                MessageBox.Show("Falta información de VASE ID, ADD_ON_ID o BARCODE_NUMBER");
            }

            else if (lista[0] == 1 && lista[1] == 1 && lista[2] == 1)
            {
                MessageBox.Show("La información esta completa");
            }
        }

        public void LoadingUpdateData()
        {
            string ofdFilename = lblData.Text;
            Mongoose.DeleteEntryData("DataTunnel");
            Mongoose.DeleteEntryData("DataBackup");
            Mongoose.BackupEntryData();
            Mongoose.LoadExcel(ofdFilename, "DataTunnel");
            Mongoose.UpdateEntryData();
            Mongoose.SetFileNameML(ofdFilename, true, lblPath.Text);

            Mongoose.DeleteCountIDs("Data");
            Mongoose.DeleteIDAddOn();
            Mongoose.LoadCountIDs("Data", 1);
            Mongoose.GetDistinctAddOn("Data", 10);

            List<string> nameVS = FillVases(Mongoose.GetNameVases("Data"));
            List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtTotal1 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtTotal2 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal31, TxtSubtotal32, TxtSubtotal33, TxtTotal3 });
            Utils.SetComboBox(nameAD, new List<ComboBox> { cbAddon11, cbAddon12, cbAddon13 });
            Utils.SetValuesTxt(new List<float> { 0, 0, 0, 0 }, new List<TextBox> { TxtSubtotal41, TxtSubtotal42, TxtSubtotal43, TxtTotal4 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { comboActual });

            _ = CheckDB(Mongoose.GetNameVases("Data"), true);
            lblRegActual.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";
            lblPath.Text = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFilePath())}";
        }

        private async void mnuFDUpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = UIMessages.EliteFlower(85, mnuELEnglish.Checked);
                if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "Office Files|*.xls;*.xlsx" })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            lblData.Text = ofd.FileName;
                            ShowLoader(UIMessages.EliteFlower(86, mnuELEnglish.Checked));
                            Task oTask = new Task(LoadingUpdateData);
                            oTask.Start();
                            await oTask;
                            HideLoader();
                            MessageBox.Show(UIMessages.EliteFlower(87, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void mnuFDRestoreBackup_Click(object sender, EventArgs e)
        {
            string msg = UIMessages.EliteFlower(88, mnuELEnglish.Checked);
            if (MessageBox.Show(msg, "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Console.WriteLine("Backup");
            }
        }

        private void mnuFStatistics_Click(object sender, EventArgs e)
        {
            Mongoose.GetStatistics("Data", mnuELEnglish.Checked);
            Mongoose.UpdateReadedData();
            GetDataSummary();
        }


        /// <summary>
        /// Funciones Para Actualizar los graficos de los sensores
        /// </summary>


        private void updateGraphicsMesaninPulmones(bool[] sensorValues)
        {
            List<PictureBox> BoxesMesanin = new List<PictureBox>()
            {
                pcbMesaninB19,
                pcbMesaninB18,
                pcbMesaninB17,
                pcbMesaninB16,
                pcbMesaninB15,
                pcbMesaninB14,
                pcbMesaninB13,
                pcbMesaninB12,
                pcbMesaninB11
            };
            List<PictureBox> LightsMesanin = new List<PictureBox>()
            {
                pcbMesaninL19,
                pcbMesaninL18,
                pcbMesaninL17,
                pcbMesaninL16,
                pcbMesaninL15,
                pcbMesaninL14,
                pcbMesaninL13,
                pcbMesaninL12,
                pcbMesaninL11
            };
            for (int ii = 0; ii < sensorValues.Length; ii++)
            {
                if (!sensorValues[ii])
                {
                    LightsMesanin[ii].Image = LightsMesanin[ii].InitialImage;
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].InitialImage;
                }
                else
                {
                    LightsMesanin[ii].Image = LightsMesanin[ii].ErrorImage;
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].ErrorImage;
                }
            }
        }

        private void updateGraphicsNeumaticActuators(bool[] sensorValues)
        {
            List<PictureBox> BoxesMesanin = new List<PictureBox>()
            {
                E4N1P1,
                E4N1P2,
                E4N2P1,
                E4N2P2
            };
            for (int ii = 0; ii < sensorValues.Length; ii++)
            {
                if (sensorValues[ii])
                {
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].InitialImage;
                }
                else
                {
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].ErrorImage;
                }
            }
        }

        private void updateGraphicsMesaninElevadores(bool[] sensorValues)
        {
            List<PictureBox> BoxesMesanin = new List<PictureBox>()
            {
                pcbElevadoresL19,
                pcbElevadoresL18,
                pcbElevadoresL17,
                pcbElevadoresL16,
                pcbElevadoresL15,
                pcbElevadoresL14,
                pcbElevadoresL13,
                pcbElevadoresL12,
                pcbElevadoresL11
            };
            for (int ii = 0; ii < sensorValues.Length; ii++)
            {
                if (!sensorValues[ii])
                {
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].InitialImage;
                }
                else
                {
                    BoxesMesanin[ii].Image = BoxesMesanin[ii].ErrorImage;
                }
            }
        }

        private void updateGraphicsMesaninSorter(int pos)
        {
            if (pos > 5 && pos <= 9)
            {
                pcbMesaninLeft.Image = pcbMesaninLeft.InitialImage;
                pcbMesaninRight.Image = pcbMesaninRight.ErrorImage;
            }
            else if (pos >= 1 && pos < 5)
            {
                pcbMesaninLeft.Image = pcbMesaninLeft.ErrorImage;
                pcbMesaninRight.Image = pcbMesaninRight.InitialImage;
            }
            else
            {
                pcbMesaninLeft.Image = pcbMesaninLeft.ErrorImage;
                pcbMesaninRight.Image = pcbMesaninRight.ErrorImage;
            }
        }

        private void updateGraphicsBaseSorter()
        {
            bool[] sensorValue = modbusElevadores.ReadCoils(45, 1);
            if (sensorValue[0] == true)
            {
                pcbMesaninB10.Image = pcbMesaninB10.InitialImage;
                pcbMesaninL10.Image = pcbMesaninL10.InitialImage;
            }
            else
            {
                pcbMesaninB10.Image = pcbMesaninB10.ErrorImage;
                pcbMesaninL10.Image = pcbMesaninL10.ErrorImage;
            }
        }

        private void updateGraphicsFeederElevator()
        {
            bool[] sensorValue = modbusElevadores.ReadCoils(43, 1);
            bool[] DButtonValue = modbusElevadores.ReadCoils(79, 1);
            bool[] Reflex2Value = modbusElevadores.ReadCoils(81, 1);
            if (sensorValue[0] == true)
            {
                E4S1B0.Image = E4S1B0.InitialImage;

            }
            else
            {
                E4S1B0.Image = E4S1B0.ErrorImage;

            }
            if (DButtonValue[0] == true)
            {
                E4DBON.Image = E4DBON.InitialImage;

            }
            else
            {
                E4DBON.Image = E4DBON.ErrorImage;

            }
            if (Reflex2Value[0] == true)
            {
                E4R2BO.Image = E4R2BO.InitialImage;

            }
            else
            {
                E4R2BO.Image = E4R2BO.ErrorImage;

            }



        }

        async private void leerSensores()
        {
            // Llamar esta función para leer los sensores del sistema en tiempo real
            if (modbusElevadores.Connected)
            {
                if (loadFlag)
                {
                    loadFlag = false;
                    btnLoadStatus.Text = "Load";
                    txtModbus.Text = "   ";
                    if (btnconnectFlag)
                    {
                        btnConnect.Enabled = true;
                    }
                }
                else
                {
                    loadFlag = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    btnLoadStatus.Text = "Cancel";
                    txtModbus.Text = "Connected   ";
                    btnConnect.Enabled = false;

                }
                while (loadFlag)
                {
                    Emergency_Stop();
                    bool[] _valuesSElevadores = modbusElevadores.ReadCoils(67, 9);
                    bool[] _valuesSPulmones = modbusElevadores.ReadCoils(58, 9);
                    bool[] _valuesSActuadores = modbusElevadores.ReadCoils(102, 4);

                    updateGraphicsMesaninPulmones(_valuesSPulmones);
                    updateGraphicsMesaninElevadores(_valuesSElevadores);
                    updateGraphicsBaseSorter();
                    updateGraphicsNeumaticActuators(_valuesSActuadores);
                    updateGraphicsFeederElevator();
                    await Task.Delay(100);


                    int speed1 = CodeValue((int)numSpeed1.Value);
                    int speed2 = CodeValue((int)numSpeed2.Value);
                    txtSpeed1.Text = speed1.ToString();
                    if (btnconnectFlag == false)
                    {
                        changeSpeed(speed1, speed2);
                    }


                    //gpMatriz.Visible = true;
                }

            }
            else
            {

                CloseStartMesanin();
                throw new StartNoPLCException();
            }
        }

        async private void ShowState()
        {
            if (modbusElevadores.Connected)
            {
                if (StateFlag)
                {
                    StateFlag = false;
                    txtNext.Text = "0";
                }
                else
                {
                    StateFlag = true;
                }
                while (StateFlag)
                {
                    int[] tt = modbusElevadores.ReadHoldingRegisters(HREG_Pulmon, 1);
                    string s = tt[0].ToString();
                    txtNext.Text = s;

                    string vase;

                    switch (s)
                    {
                        case "1":
                            vase = cbWorker11.Text;
                            break;
                        case "2":
                            vase = cbWorker12.Text;
                            break;
                        case "3":
                            vase = cbWorker13.Text;
                            break;
                        case "4":
                            vase = cbWorker21.Text;
                            break;
                        case "5":
                            vase = cbWorker22.Text;
                            break;
                        case "6":
                            vase = cbWorker23.Text;
                            break;
                        case "7":
                            vase = cbWorker31.Text;
                            break;
                        case "8":
                            vase = cbWorker32.Text;
                            break;
                        case "9":
                            vase = cbWorker33.Text;
                            break;
                        default:
                            vase = "NA";
                            break;
                    }
                    txtVase.Text = vase;
                    string url = Mongoose.ShowImageMesanin(vase);

                    if (url != "ND")
                    {
                        FileStream fs = File.OpenRead(url);
                        pcbInitialFill.Image = Image.FromStream(fs);
                        pcbFill.Image = Image.FromStream(fs);
                        fs.Close();
                    }
                    else
                    {
                        pcbInitialFill.Image = pcbFill.InitialImage;
                        pcbFill.Image = pcbFill.InitialImage;
                    }
                    await Task.Delay(100);
                }
            }

        }





        /// <summary>
        /// Funcion para procesar un paro de emergencia
        /// </summary>

        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mongoose.GetESActive() == true)
                {

                    if (workMesanin == true)
                    {
                        if (modbusElevadores.Connected)
                        {
                            //Mongoose.SetInitialFeedTrue();
                            //Mongoose.SetWorkMesaninTrue();
                            //workMesanin = Mongoose.GetWorkMesanin();
                            //lblMesanin1.Visible = true;
                            //lblMesanin2.Visible = true;
                            //lblMesanin3.Visible = true;
                            //lblMesanin4.Visible = true;
                            //pcbFill.Visible = true;
                            //btnInicio.Text = "Stop";
                            Mongoose.SetESActiveFalse();
                            // actualizaInfoTextos();              //-- Actualiza textos
                            btnResume.Visible = false;
                            modbusElevadores.WriteSingleCoil(coilEMG, false);
                            Thread.Sleep(3000);



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"{ex.Source} - {ex.Message} - {ex.StackTrace} - {ex.InnerException} - {ex.HResult}");
            }
        }

        async public void Emergency_Stop()
        {
            workMesanin = Mongoose.GetWorkMesanin();
            if (workMesanin == true)
            {

                if (modbusElevadores.Connected)
                {
                    bool[] EMG = modbusElevadores.ReadCoils(coilEMG, 1);
                    if (EMG[0])
                    {

                        //Console.WriteLine("Activo paro de emergencia\n");
                        // int[] tt = modbusElevadores.ReadHoldingRegisters(1002, 1);

                        // Console.WriteLine(tt[0]);               

                        modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, new bool[] { false, false });
                        Mongoose.SetESActiveTrue();
                        btnResume.Visible = true;
                        if (!EMGFlag)
                        {
                            EMGFlag = true;
                            MessageBox.Show(UIMessages.EliteFlower(104, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (!EMG[0])
                    {
                        // Console.WriteLine("Desactivo paro de emergencia\n");
                        bool[] BandsChecked = new bool[] { chb_Band1.Checked, chb_Band2.Checked };
                        modbusLuces.WriteMultipleCoils(COILS_LUCES + 12, BandsChecked);
                        EMGFlag = false;
                    }

                }
            }
            await Task.Delay(50);
        }

        public void ShowLoader(string text)
        {
            loader = new Loader(text);
            loader.Show();
        }

        public void HideLoader()
        {
            if (loader != null)
            {
                loader.Close();
            }
        }

        private async void btnManBalance_Click(object sender, EventArgs e)
        {
            string msg = UIMessages.EliteFlower(89, mnuELEnglish.Checked);
            DialogResult dialogResult = MessageBox.Show(msg, UIMessages.EliteFlower(56, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ShowLoader(UIMessages.EliteFlower(90, mnuELEnglish.Checked));
                Task oTask2 = new Task(manual);
                oTask2.Start();
                await oTask2;
                HideLoader();
                manBalanceCheck = true;
                MessageBox.Show(UIMessages.EliteFlower(91, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manBalanceCheck == false)
            {
                string msg = UIMessages.EliteFlower(92, mnuELEnglish.Checked);
                DialogResult dialogResult = MessageBox.Show(msg, UIMessages.EliteFlower(56, mnuELEnglish.Checked), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    ShowLoader(UIMessages.EliteFlower(90, mnuELEnglish.Checked));
                    Task oTask1 = new Task(manual);
                    oTask1.Start();
                    await oTask1;
                    HideLoader();
                    manBalanceCheck = true;
                    MessageBox.Show(UIMessages.EliteFlower(91, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    List<BalanceCount> BalanceCountData = Mongoose.GetDataBalanceCount();
                    if (BalanceCountData.Count > 0 && BalanceCountData[0].Stage != 4)
                    {
                        ShowLoader(UIMessages.EliteFlower(93, mnuELEnglish.Checked));
                        Task oTask2 = new Task(LoadingBackLastBalance);
                        oTask2.Start();
                        await oTask2;
                        HideLoader();
                        manBalanceCheck = true;
                        MessageBox.Show(UIMessages.EliteFlower(94, mnuELEnglish.Checked), "EliteFlower", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        List<List<ComboBox>> comboStages = new List<List<ComboBox>>
                        {
                            new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                            new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                            new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 },
                            new List<ComboBox> { cbAddon11,  cbAddon12,  cbAddon13  }
                        };
                        foreach (List<ComboBox> item in comboStages)
                        {
                            item[0].SelectedIndex = item[0].Items.Count - 1;
                            item[1].SelectedIndex = item[1].Items.Count - 1;
                            item[2].SelectedIndex = item[2].Items.Count - 1;
                        }
                        Utils.SetValuesTxt(
                            new List<float> {
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0
                            },
                            new List<TextBox> {
                                TxtSubtotal11, TxtSubtotal12, TxtSubtotal13,
                                TxtSubtotal21, TxtSubtotal22, TxtSubtotal23,
                                TxtSubtotal31, TxtSubtotal32, TxtSubtotal33,
                                TxtSubtotal41, TxtSubtotal42, TxtSubtotal43
                            });
                        Utils.SetValuesTxt(
                            new List<float> {
                                0, 0, 0, 0
                            },
                            new List<TextBox> {
                                TxtTotal1, TxtTotal2, TxtTotal3, TxtTotal4
                            });
                        manBalanceCheck = true;
                    }
                }
            }
        }

        private void LoadingBackLastBalance()
        {
            Thread.Sleep(1000);
            List<List<ComboBox>> comboStages = new List<List<ComboBox>>
            {
                new List<ComboBox> { cbWorker11, cbWorker12, cbWorker13 },
                new List<ComboBox> { cbWorker21, cbWorker22, cbWorker23 },
                new List<ComboBox> { cbWorker31, cbWorker32, cbWorker33 }
            };
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();
            List<int> int_indexes = new List<int> { 0, 1, 2 };
            List<int> false_indexes = int_indexes.Except(true_indexes).ToList();

            List<int> BalanceIndex = BalanceIndexPriv;
            List<int> StageIndex = StageIndexPriv;

            if (AvailableWorkers == 2)
            {
                comboStages[StageIndex[0]][0].SelectedIndex = BalanceIndex[0];
                comboStages[StageIndex[0]][1].SelectedIndex = BalanceIndex[1];
                comboStages[StageIndex[0]][2].SelectedIndex = BalanceIndex[2];
                comboStages[StageIndex[1]][0].SelectedIndex = BalanceIndex[3];
                comboStages[StageIndex[1]][1].SelectedIndex = BalanceIndex[4];
                comboStages[StageIndex[1]][2].SelectedIndex = BalanceIndex[5];
                comboStages[false_indexes[0]][0].SelectedIndex = comboStages[false_indexes[0]][0].Items.Count - 1;
                comboStages[false_indexes[0]][1].SelectedIndex = comboStages[false_indexes[0]][1].Items.Count - 1;
                comboStages[false_indexes[0]][2].SelectedIndex = comboStages[false_indexes[0]][2].Items.Count - 1;
            }
            else
            {
                comboStages[StageIndex[0]][0].SelectedIndex = BalanceIndex[0];
                comboStages[StageIndex[0]][1].SelectedIndex = BalanceIndex[1];
                comboStages[StageIndex[0]][2].SelectedIndex = BalanceIndex[2];
                comboStages[StageIndex[1]][0].SelectedIndex = BalanceIndex[3];
                comboStages[StageIndex[1]][1].SelectedIndex = BalanceIndex[4];
                comboStages[StageIndex[1]][2].SelectedIndex = BalanceIndex[5];
                comboStages[StageIndex[2]][0].SelectedIndex = BalanceIndex[6];
                comboStages[StageIndex[2]][1].SelectedIndex = BalanceIndex[7];
                comboStages[StageIndex[2]][2].SelectedIndex = BalanceIndex[8];
            }

            List<BalanceCount> BalanceCountData = Mongoose.GetDataBalanceCount();
            List<float> valuesStages = Utils.GetValuesBalanced(BalanceCountData, true_indexes);
            Utils.SetValuesTxt(valuesStages, new List<TextBox> { TxtSubtotal11, TxtSubtotal12, TxtSubtotal13, TxtSubtotal21, TxtSubtotal22, TxtSubtotal23, TxtSubtotal31, TxtSubtotal32, TxtSubtotal33 });
            Utils.SetValuesTxt(new List<float> { valuesStages.GetRange(0, 3).Sum(), valuesStages.GetRange(3, 3).Sum(), valuesStages.GetRange(6, 3).Sum() }, new List<TextBox> { TxtTotal1, TxtTotal2, TxtTotal3 });
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusElevadores.Connected)
                {



                    btnConnect.Text = "Connect";
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                    btnLoadStatus.Enabled = false;
                    DisconnectModbusClient();
                    btnconnectFlag = false;


                }
                else
                {
                    if (modbusElevadores.Available(2))
                    {
                        txtModbus.Text = "Available ";
                        modbusElevadores.Connect();
                        btnLoadStatus.Enabled = true;
                        btnconnectFlag = true;
                        btnConnect.Text = "Disconnect";
                        if (modbusElevadores.Connected)
                        {
                            txtModbus.Text = "Available and connected ";
                            modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                            modbusElevadores.WriteMultipleCoils(COILS_ELEVADORES + 76, new bool[] { true, true, true });
                        }



                    }
                    else
                    {
                        txtModbus.Text = "Not Available  ";
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void DisconnectModbusClient()
        {
            try
            {

                modbusElevadores.Disconnect();
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusElevadores.Available(2))
                {
                    modbusElevadores.Connect();
                }

                if (modbusElevadores.Connected)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 91, true);
                    Thread.Sleep(500);
                    // modbusElevadores.Disconnect();
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                Application.Exit();
            }

        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Add(tabMain);
            tabControl.TabPages.Remove(tabConfiguration);
            tabControl.TabPages.Remove(tabStatus);
            tabControl.TabPages.Remove(tabPrueba);
            tabControl.TabPages.Remove(tabElite);
            ((Control)this.tabMain).Enabled = true;
            ((Control)this.tabMain).Visible = true;
            ((Control)this.tabConfiguration).Visible = false;
            ((Control)this.tabStatus).Visible = false;
            ((Control)this.tabPrueba).Visible = false;
            ((Control)this.tabElite).Visible = false;

            btnMain.Enabled = false;
            btnConfiguration.Enabled = true;
            btnStatus.Enabled = true;
            btnPrueba.Enabled = true;
            pictureBox1.Enabled = true;


            btnConfiguration.Location = new Point(3, 300);
            btnStatus.Location = new Point(3, 350);
            btnPrueba.Location = new Point(3, 400);

            btnStart.Parent = MenuVertical;
            btnPause.Parent = MenuVertical;
            btnInicio.Parent = MenuVertical;
            btnInicio.Font = new Font("Zygo", 12);
            btnBalance.Parent = tabConfiguration;
            btnManBalance.Parent = tabConfiguration;



            btnStart.Location = new Point(40, 150);
            btnPause.Location = new Point(40, 200);
            btnInicio.Location = new Point(40, 250);
            btnBalance.Location = new Point(380, 244);
            btnManBalance.Location = new Point(382, 310);

            panel3.Location = new Point(40, 150);
            panel4.Location = new Point(40, 200);
            panel5.Location = new Point(40, 250);










        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Add(tabConfiguration);
            tabControl.TabPages.Remove(tabMain);
            tabControl.TabPages.Remove(tabStatus);
            tabControl.TabPages.Remove(tabPrueba);
            tabControl.TabPages.Remove(tabElite);
            ((Control)this.tabConfiguration).Enabled = true;
            ((Control)this.tabConfiguration).Visible = true;
            ((Control)this.tabMain).Visible = false;
            ((Control)this.tabStatus).Visible = false;
            ((Control)this.tabPrueba).Visible = false;
            ((Control)this.tabElite).Visible = false;
            btnMain.Enabled = true;
            btnConfiguration.Enabled = false;
            btnStatus.Enabled = true;
            btnPrueba.Enabled = true;
            pictureBox1.Enabled = true;

            btnStart.Parent = panel9;
            btnPause.Parent = panel9;
            btnInicio.Parent = panel9;
            btnBalance.Parent = MenuVertical;
            btnManBalance.Parent = MenuVertical;

            btnStart.Location = new Point(3, 150);
            btnPause.Location = new Point(3, 200);
            btnInicio.Location = new Point(3, 250);
            btnStatus.Location = new Point(3, 300);
            btnPrueba.Location = new Point(3, 350);
            btnConfiguration.Location = new Point(3, 150);
            btnBalance.Location = new Point(40, 200);
            btnManBalance.Location = new Point(40, 250);






            panel3.Location = new Point(3, 150);
            panel4.Location = new Point(40, 200);
            panel5.Location = new Point(40, 250);
            panel10.Location = new Point(3, 300);
            panel11.Location = new Point(3, 350);
            panel12.Location = new Point(3, 400);
            panel13.Location = new Point(3, 450);
            panel14.Location = new Point(3, 500);







        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Add(tabStatus);
            tabControl.TabPages.Remove(tabMain);
            tabControl.TabPages.Remove(tabConfiguration);
            tabControl.TabPages.Remove(tabPrueba);
            tabControl.TabPages.Remove(tabElite);
            ((Control)this.tabStatus).Enabled = true;
            ((Control)this.tabStatus).Visible = true;
            ((Control)this.tabMain).Visible = false;
            ((Control)this.tabConfiguration).Visible = false;
            ((Control)this.tabPrueba).Visible = false;
            btnMain.Enabled = true;
            btnConfiguration.Enabled = true;
            btnStatus.Enabled = false;
            btnPrueba.Enabled = true;
            pictureBox1.Enabled = true;


            btnStart.Parent = panel9;
            btnPause.Parent = panel9;
            btnInicio.Parent = panel9;
            btnBalance.Parent = tabConfiguration;
            btnManBalance.Parent = tabConfiguration;

            btnStart.Location = new Point(3, 150);
            btnPause.Location = new Point(3, 200);
            btnInicio.Location = new Point(3, 150);
            btnConfiguration.Location = new Point(3, 150);
            btnStatus.Location = new Point(3, 200);
            btnPrueba.Location = new Point(3, 250);
            btnBalance.Location = new Point(40, 200);
            btnManBalance.Location = new Point(40, 250);


            panel3.Location = new Point(3, 150);
            panel4.Location = new Point(3, 200);
            panel5.Location = new Point(3, 250);
            panel10.Location = new Point(3, 300);
            panel11.Location = new Point(3, 350);
            panel12.Location = new Point(3, 400);
            panel13.Location = new Point(3, 450);
            panel14.Location = new Point(3, 500);




        }


        private void btnPrueba_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Add(tabPrueba);
            tabControl.TabPages.Remove(tabMain);
            tabControl.TabPages.Remove(tabConfiguration);
            tabControl.TabPages.Remove(tabStatus);
            tabControl.TabPages.Remove(tabElite);

            ((Control)this.tabPrueba).Enabled = true;
            ((Control)this.tabPrueba).Visible = true;
            ((Control)this.tabMain).Visible = false;
            ((Control)this.tabConfiguration).Visible = false;
            ((Control)this.tabStatus).Visible = false;
            ((Control)this.tabElite).Visible = false;

            btnMain.Enabled = true;
            btnConfiguration.Enabled = true;
            btnStatus.Enabled = true;
            btnPrueba.Enabled = false;
            pictureBox1.Enabled = true;

            btnStart.Parent = panel9;
            btnPause.Parent = panel9;
            btnInicio.Parent = panel9;
            btnBalance.Parent = tabConfiguration;
            btnManBalance.Parent = tabConfiguration;

            btnStart.Location = new Point(3, 150);
            btnPause.Location = new Point(3, 200);
            btnInicio.Location = new Point(3, 150);
            btnConfiguration.Location = new Point(3, 150);
            btnStatus.Location = new Point(3, 200);
            btnPrueba.Location = new Point(3, 250);
            btnBalance.Location = new Point(40, 200);
            btnManBalance.Location = new Point(60, 250);


            panel3.Location = new Point(3, 150);
            panel4.Location = new Point(3, 200);
            panel5.Location = new Point(3, 250);
            panel10.Location = new Point(3, 300);
            panel11.Location = new Point(3, 350);
            panel12.Location = new Point(3, 400);
            panel13.Location = new Point(3, 450);
            panel14.Location = new Point(3, 500);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Remove(tabPrueba);
            tabControl.TabPages.Remove(tabMain);
            tabControl.TabPages.Remove(tabConfiguration);
            tabControl.TabPages.Remove(tabStatus);
            tabControl.TabPages.Add(tabElite);

            ((Control)this.tabElite).Enabled = true;
            ((Control)this.tabElite).Visible = true;
            ((Control)this.tabMain).Visible = false;
            ((Control)this.tabConfiguration).Visible = false;
            ((Control)this.tabStatus).Visible = false;
            ((Control)this.tabPrueba).Visible = false;

            btnMain.Enabled = true;
            btnConfiguration.Enabled = true;
            btnStatus.Enabled = true;
            btnPrueba.Enabled = true;
            pictureBox1.Enabled = false;

            btnStart.Parent = panel9;
            btnPause.Parent = panel9;
            btnInicio.Parent = panel9;
            btnBalance.Parent = tabConfiguration;
            btnManBalance.Parent = tabConfiguration;

            btnStart.Location = new Point(3, 150);
            btnPause.Location = new Point(3, 200);
            btnInicio.Location = new Point(3, 150);
            btnConfiguration.Location = new Point(3, 150);
            btnStatus.Location = new Point(3, 200);
            btnPrueba.Location = new Point(3, 250);
            btnBalance.Location = new Point(40, 200);
            btnManBalance.Location = new Point(60, 250);

            panel3.Location = new Point(3, 150);
            panel4.Location = new Point(3, 200);
            panel5.Location = new Point(3, 250);
            panel10.Location = new Point(3, 300);
            panel11.Location = new Point(3, 350);
            panel12.Location = new Point(3, 400);
            panel13.Location = new Point(3, 450);
            panel14.Location = new Point(3, 500);

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void tabStatus_Click(object sender, EventArgs e)
        {

        }

        private void btnServoON_Click(object sender, EventArgs e)
        {

            try
            {
                if (modbusElevadores.Available(2))
                {
                    modbusElevadores.Connect();
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 50, false);
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, true);

                }
                if (modbusElevadores.Connected)
                {
                    bool[] servostatus = modbusElevadores.ReadCoils(91, 1);
                    if (!servostatus[0])
                    {
                        modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 55, true);
                    }

                }
                mnuEdit.Enabled = false;
                btnServoON.Enabled = false;
                btnHome.Enabled = true;

            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        async private void btnHome_Click(object sender, EventArgs e)
        {
            var CoilsHome = new List<int> { 0, 4, 8, 13, 17, 21, 26, 30, 34, 38, 12, 25 };

            try
            {
                string msgBody = "¿Do you want to Home All?";
                string msgTitle = "Question";
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



                btnHome.Enabled = false;
                btnInitAll.Enabled = true;

            }

            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        async private void btnInitAll_Click(object sender, EventArgs e)
        {
            bool flagok = false;
            var CoilPos1 = new List<int> { 1, 5, 9, 14, 18, 22, 27, 31, 35, 39 };
            try
            {
                string msgBody = "¿All Servos at Home?";
                string msgTitle = "Question";
                if (MessageBox.Show(msgBody, msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 5);
                    modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 6);
                    foreach (int item in CoilPos1)
                    {
                        int coil = COILS_ELEVADORES + item;
                        modbusElevadores.WriteSingleCoil(coil, true);
                        await Task.Delay(10);

                    }
                    modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 0, 5);
                    modbusElevadores.WriteSingleRegister(REGS_ELEVADORES + 1, 6);
                    await Task.Delay(10);
                    flagok = true;
                }

                Mongoose.SetLastPosZone2(new List<int>() { 5, 6 });
                if (flagok)
                {
                    btnInitAll.Enabled = false;
                    button3.Enabled = true;
                }





            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (modbusElevadores.Connected)
            {
                modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 80, false);
                DisconnectModbusClient();
                mnuEdit.Enabled = true;
                button3.Enabled = false;

            }
        }

        private async void Bal_Python_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoader("Balancing Data");
                Task oTask2 = new Task(balanceo);
                oTask2.Start();
                await oTask2;
                HideLoader();
                ShowBalance();
                balanceCheck = true;
            }
            catch (BalanceMoreStationsException exBalance)
            {
                Mongoose.LoadError(exBalance, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(exBalance.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }


        }



        private void balanceo()
        {
            List<bool> WorkersChecked = new List<bool>() { chb_worker1.Checked, chb_worker2.Checked, chb_worker3.Checked };
            int AvailableWorkers = WorkersChecked.Where(c => c).Count();
            List<int> int_indexes = new List<int> { 0, 1, 2 };
            List<int> true_indexes = WorkersChecked.Select((value, index) => value ? index : -1).Where(o => o >= 0).ToList();
            List<int> false_indexes = int_indexes.Except(true_indexes).ToList();
            int ManualCheck = chb_manual.Checked ? 1 : 0;
            int BT = checkBox1.Checked ? 1 : 0;
            string[] SV = new string[] {
                this.cbWorker11.GetItemText(this.cbWorker11.SelectedItem),
                this.cbWorker12.GetItemText(this.cbWorker12.SelectedItem),
                this.cbWorker13.GetItemText(this.cbWorker13.SelectedItem),
                this.cbWorker21.GetItemText(this.cbWorker21.SelectedItem),
                this.cbWorker22.GetItemText(this.cbWorker22.SelectedItem),
                this.cbWorker23.GetItemText(this.cbWorker23.SelectedItem),
                this.cbWorker31.GetItemText(this.cbWorker31.SelectedItem),
                this.cbWorker32.GetItemText(this.cbWorker32.SelectedItem),
                this.cbWorker33.GetItemText(this.cbWorker33.SelectedItem),
            };

            Console.WriteLine(this.cbWorker11.GetItemText(this.cbWorker11.SelectedItem));


            Console.WriteLine("Workers checked = ");

            var FileName = $"{string.Format(UIMessages.EliteFlower(11, mnuELEnglish.Checked), Mongoose.GetFileNameML())}";
            var FilePath = lblPath.Text = $"{Mongoose.GetFilePath()}";
            var Tname = CB_Template.Text;
            Console.Write("File Name: ");
            Console.WriteLine(FileName);
            Console.Write("excel path: ");
            Console.WriteLine(FilePath);

            //1) Create Process Info
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\aalej\AppData\Local\Microsoft\WindowsApps\python.exe"; //foldier de instalacion de python


            //2) Provide script and arguments

            var script = @"D:\GitHub\EliteFlowerDeploy_V3\03_EliteFlower_V3\Balanceo\Balanceo.py";
            var var_ext_estaciones_slc = new List<int> { '2', '3', '1' };
            psi.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\" \"{11}\" \"{12}\" \"{13}\" \"{14}\" \"{15}\" \"{16}\"",
                                           script, WorkersChecked[0], WorkersChecked[1], WorkersChecked[2], FilePath, ManualCheck,
                                           SV[0], SV[1], SV[2], SV[3], SV[4], SV[5], SV[6], SV[7], SV[8], Tname , BT);
            Console.WriteLine("Argumentos ");
            Console.WriteLine(psi.Arguments);
            //psi.Arguments = $"\"{script}\"\"{WorkersChecked[0]}\"\"{WorkersChecked[1]}\"\"{WorkersChecked[2]}\"";
            //psi.Arguments = $"\"{script}\"";

            //3) Process Configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //4) Execute process and get output

            var errors = "";
            var results = "";
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

            }

            //5) Display Output
            Console.WriteLine("");
            Console.Write(errors);
            Console.WriteLine();
            Console.WriteLine("Results");
            Console.WriteLine(results);

        }

        private void prueba_Click(object sender, EventArgs e)
        {
            //"Este script usa la funcionalidad de IronPython"
            string pathPy = @"D:\GitHub\EliteFlowerDeploy_V3\03_EliteFlower_V3\Balanceo\module1.py";
            ScriptRuntime py = Python.CreateRuntime();
            dynamic pyBalanceo = py.UseFile(pathPy);
            pyBalanceo.Hi("Santiago");
        }

        private void prueba2_Click(object sender, EventArgs e)
        {
            //1) Create Process Info
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\aalej\AppData\Local\Microsoft\WindowsApps\python.exe"; //foldier de instalacion de python


            //2) Provide script and arguments

            var script = @"D:\GitHub\EliteFlowerDeploy_V3\03_EliteFlower_V3\Balanceo\module1.py";
            var name = "Santiago";
            //var otro = "prueba";
            psi.Arguments = string.Format("\"{0}\" \"{1}\"", script, name);
            //  psi.Arguments = $"\"{script}\"\"{name}\"\"{otro}\"";
            //psi.Arguments = $"\"{script}\"";

            //3) Process Configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //4) Execute process and get output

            var errors = "";
            var results = "";
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

            }

            //5) Display Output
            Console.WriteLine("");
            Console.Write(errors);
            Console.WriteLine();
            Console.WriteLine("Results");
            Console.WriteLine(results);


        }
        private void ShowBalance()
        {
            List<Statics> doc = Mongoose.get_balance();
            int Total_Orders = doc[0].Total_Orders;
            int Total_Vases = doc[0].Total_Vases;
            // object Orders = doc[0].Orders;
            string[] Balance = doc[0].Estaciones;
            int[] Cuenta = doc[0].Cuenta_Estaciones;
            string[] AddON = doc[0].AddON;
            int[] Cuenta_AddON = doc[0].Cuenta_AddON;


            //Console.WriteLine(Total_Orders);
            //Console.WriteLine(Total_Vases);
            //Console.WriteLine(Balance);
            //for (int i = 0; i < Balance.Length; i++)
            //{
            //    Console.WriteLine(Balance[i]);
            //    Console.WriteLine(Cuenta[i]);
            //}

            cbWorker11.Text = Balance[0];
            cbWorker12.Text = Balance[1];
            cbWorker13.Text = Balance[2];
            cbWorker21.Text = Balance[3];
            cbWorker22.Text = Balance[4];
            cbWorker23.Text = Balance[5];
            cbWorker31.Text = Balance[6];
            cbWorker32.Text = Balance[7];
            cbWorker33.Text = Balance[8];

            TxtSubtotal11.Text = Cuenta[0].ToString();
            TxtSubtotal12.Text = Cuenta[1].ToString();
            TxtSubtotal13.Text = Cuenta[2].ToString();
            TxtSubtotal21.Text = Cuenta[3].ToString();
            TxtSubtotal22.Text = Cuenta[4].ToString();
            TxtSubtotal23.Text = Cuenta[5].ToString();
            TxtSubtotal31.Text = Cuenta[6].ToString();
            TxtSubtotal32.Text = Cuenta[7].ToString();
            TxtSubtotal33.Text = Cuenta[8].ToString();

            TxtTotal1.Text = (Cuenta[0] + Cuenta[1] + Cuenta[2]).ToString();
            TxtTotal2.Text = (Cuenta[3] + Cuenta[4] + Cuenta[5]).ToString();
            TxtTotal3.Text = (Cuenta[6] + Cuenta[7] + Cuenta[8]).ToString();
            TxtTotal4.Text = (Cuenta_AddON[0] + Cuenta_AddON[1] + Cuenta_AddON[2]).ToString();

            cbAddon11.Text = AddON[0];
            cbAddon12.Text = AddON[1];
            cbAddon13.Text = AddON[2];

            TxtSubtotal41.Text = Cuenta_AddON[0].ToString();
            TxtSubtotal42.Text = Cuenta_AddON[1].ToString();
            TxtSubtotal43.Text = Cuenta_AddON[2].ToString();
        }
        private void btnShowBalance_Click(object sender, EventArgs e)
        {
            ShowBalance();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void mnuBtemplate_Click(object sender, EventArgs e)
        {
            BalanceTemplate Btemplate = new BalanceTemplate(this, mnuELEnglish.Checked);
            Btemplate.Show();
            this.Enabled = false;
        }

        private void chb_manual_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
             
            void checkBox1_Click()
            {
                switch (checkBox1.CheckState)
                {
                    case CheckState.Checked:
                        // Code for checked state.  
                        CB_Template.Visible = true;

                        string mongoDBConnection = "mongodb://localhost:27017";
                        this.FormBorderStyle = FormBorderStyle.FixedSingle;
                        mongoDBConnection = Mongoose.GetMongoDBConnection();

                        List<string> TemplateName = Mongoose.GetTemplate("BalanceTemplate");
                        Utils.SetComboBox(TemplateName, new List<ComboBox> { CB_Template });

                        if (Compare_database() == false)
                        {
                            MessageBox.Show("No all data base products are created in the master product");
                        }

                        break;

                    case CheckState.Unchecked:
                        // Code for unchecked state.
                        CB_Template.Visible = false;
                        break;

                    case CheckState.Indeterminate:
                        // Code for indeterminate state.
                        CB_Template.Visible = false;
                        break;
                }
            }

            checkBox1_Click();
            Compare_database();
        }

        private static List<Btemplate> GetTemplate(string texto)
        {

            //Console.WriteLine(texto);

            MongoClient client = Mongoose.GetDBConnection();
            IMongoDatabase database = Mongoose.GetDataBase(client);
            IMongoCollection<Btemplate> TemplateDB = database.GetCollection<Btemplate>("BalanceTemplate");
            //List<Btemplate> doc = TemplateDB.Find(f => f._id== 1).ToList();

            List<Btemplate> doc = TemplateDB.Find(d => d.ID == texto).ToList();
            string id = doc[0].ID;
            string[] temp = doc[0].Template;
            string[] add_on = doc[0].Add_on;

            return doc;
        }



        public void Get_Template()
        {
            string texto = CB_Template.SelectedItem.ToString();
            Console.WriteLine("texto = ");
            Console.WriteLine(texto);


            if (texto != "")
            {
                List<Btemplate> doc = GetTemplate(texto);

                string id = doc[0].ID;
                string[] temp = doc[0].Template;
                //string[] per = doc[0].Percentage;
                string[] add_on = doc[0].Add_on;

                cbWorker11.Text = temp[0];
                cbWorker12.Text = temp[1];
                cbWorker13.Text = temp[2];

                cbWorker21.Text = temp[3];
                cbWorker22.Text = temp[4];
                cbWorker23.Text = temp[5];

                cbWorker31.Text = temp[6];
                cbWorker32.Text = temp[7];
                cbWorker33.Text = temp[8];


                //S1_T1.Text = per[0];
                //S1_T2.Text = per[1];
                //S1_T3.Text = per[2];

                //S2_T1.Text = per[3];
                //S2_T2.Text = per[4];
                //S2_T3.Text = per[5];

                //S3_T1.Text = per[6];
                //S3_T2.Text = per[7];
                //S3_T3.Text = per[8];

                //cbAddon11.Text = add_on[0];
                //cbAddon12.Text = add_on[1];
                //cbAddon13.Text = add_on[2];
            }

        }

        private void tabConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void CB_Template_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Template();
        }

        public bool Compare_database()
        {

            List<string> ProductDB = Mongoose.GetMasterProducts("MasterProduct");
            List<string> nameVS = Mongoose.GetNameVases("Data");
            string[] vases = nameVS.ToArray();
            string[] products = ProductDB.ToArray();
            bool equal = true;

            equal = vases.SequenceEqual(products);

            return equal;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusElevadores.Available(2))
                {
                    modbusElevadores.Connect();
                }

                if (modbusElevadores.Connected)
                {
                    modbusElevadores.WriteSingleCoil(COILS_ELEVADORES + 91, true);
                    btnServoON.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                //Application.Exit();
            }
        }

        private void chEnableReset_CheckedChanged(object sender, EventArgs e)
        {
            if (chEnableReset.Checked)
            {
                btnReset.Enabled = true;
            }
            else
            {
                btnReset.Enabled = false;
            }
        }
    }
}

