using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class AddOn : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        Image FileImg1;
        readonly string PATHVASES = "C:\\Users\\Usuario\\assetsVases";
        string mongoDBConnection = "mongodb://localhost:27017";

        // ------------------------------
        //              Form            |
        // ------------------------------

        /// <summary>
        /// Inicializa las variables globales.
        /// </summary>
        public AddOn(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los componentes en la ventana.
        /// </summary>
        private void AddOn_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessages();

            chbCreate.Checked = true;
            pcbCreate.Image = pcbCreate.InitialImage;
        }
        /// <summary>
        /// Habilita la ventana que abrio esta.
        /// </summary>
        private void AddOn_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }

        // -----------------------------------
        //            Components             |
        // -----------------------------------

        /// <summary>
        /// Habilita/Deshabilita la seccion del CRUD.
        /// </summary>
        private void chbCreate_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCreate.Checked)
            {
                lblID.Visible = true;
                lblImg.Visible = true;
                //-- Create
                txtCreateID.Visible = true;
                btnOpenCreate.Visible = true;
                btnCreate.Visible = true;
                pcbCreate.Visible = true;
                //-- Delete
                cbDeleteID.Visible = false;
                btnDelete.Visible = false;
                pcbDelete.Visible = false;

                chbDelete.Checked = false;
            }
            RefreshCRUD();
        }
        /// <summary>
        /// Habilita/Deshabilita la seccion del CRUD.
        /// </summary>
        private void chbDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDelete.Checked)
            {
                lblID.Visible = true;
                lblImg.Visible = false;
                //-- Create
                txtCreateID.Visible = false;
                btnOpenCreate.Visible = false;
                btnCreate.Visible = false;
                pcbCreate.Visible = false;
                //-- Delete
                cbDeleteID.Visible = true;
                btnDelete.Visible = true;
                pcbDelete.Visible = true;

                chbCreate.Checked = false;
            }
            RefreshCRUD();
        }
        /// <summary>
        /// Cargar la imagen del producto en la seccion de crear.
        /// </summary>
        private void btnOpenCreate_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog f = new OpenFileDialog())
                {
                    f.Filter = "JPG(*.JPG)|*.jpg";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        FileImg1 = Image.FromFile(f.FileName);
                        pcbCreate.Image = FileImg1;
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Crea el registro que tiene en los campos de crear.
        /// </summary>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<AddOnID> ProductDB = database.GetCollection<AddOnID>("MasterAddOn");

                string IdText = txtCreateID.Text;
                string assetAddOn = $"{PATHVASES}\\{IdText}-addon.jpg";

                

                if (MessageBox.Show(UIMessages.AddOn(7, _EnglishChecked), UIMessages.AddOn(5, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdText != null && IdText != "")
                    {
                        AddOnID addon = new AddOnID() { ID = IdText, Url = assetAddOn };
                        int NoCreated = ProductDB.Find(d => d.ID == IdText).ToList().Count;
                        textBox1.Text = NoCreated.ToString();
                        if (NoCreated == 0)
                        {
                            if (FileImg1 == null)
                                FileImg1 = Image.FromFile($"{PATHVASES}\\NoAddOn.jpg");
                            FileImg1.Save(assetAddOn);
                            ProductDB.InsertOne(addon);
                            MessageBox.Show(string.Format(UIMessages.AddOn(8, _EnglishChecked), IdText), UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCreateID.Text = null;
                            pcbCreate.Image = pcbCreate.InitialImage;
                        }
                        else
                        {
                            throw new CreateReferenceExistException(string.Format(UIMessages.AddOn(9, _EnglishChecked), IdText));
                        }
                    }
                    else
                    {
                        throw new CreateEmptyFieldsException();
                    }
                }
            }
            catch (CreateReferenceExistException ex1)
            {
                Mongoose.LoadError(ex1, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(ex1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CreateEmptyFieldsException ex2)
            {
                Mongoose.LoadError(ex2, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.AddOn(10, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Borra el registro que tiene seleccionado en el ComboBox.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<AddOnID> ProductDB = database.GetCollection<AddOnID>("MasterAddOn");

                string IdDelete = (string)cbDeleteID.SelectedItem;

                if (MessageBox.Show(UIMessages.AddOn(5, _EnglishChecked), UIMessages.AddOn(4, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdDelete != null)
                    {
                        List<string> tt = ProductDB.Find(f => f.ID == IdDelete).ToList().Select(s => s.Url).ToList();
                        FileInfo info = new FileInfo(tt[0]);
                        if (info.Exists)
                            info.Delete();
                        ProductDB.DeleteOne(d => d.ID == IdDelete);

                        MessageBox.Show(string.Format(UIMessages.AddOn(7, _EnglishChecked), IdDelete), UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                        cbDeleteID.Items.Clear();
                        foreach (var item in tt1)
                        {
                            cbDeleteID.Items.Add(item);
                        }
                        pcbDelete.Image = pcbDelete.InitialImage;
                    }
                    else
                    {
                        throw new CreateEmptyFieldsException();
                    }
                }
            }
            catch (CreateEmptyFieldsException ex2)
            {
                Mongoose.LoadError(ex2, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.AddOn(10, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Muestra el registro segun el ID seleccionado en el ComboBox.
        /// </summary>
        private void cbDeleteID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<AddOnID> ProductDB = database.GetCollection<AddOnID>("MasterAddOn");

                List<AddOnID> lst = ProductDB.Find(d => d.ID == (string)cbDeleteID.SelectedItem).ToList();
                if (lst.Count > 0)
                {
                    FileStream fs = File.OpenRead(lst[0].Url);
                    pcbDelete.Image = Image.FromStream(fs);
                    fs.Close();
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
        /// Dependiendo de cual checker este activo, muestra la parte del CRUD indicada.
        /// </summary>
        public void RefreshCRUD()
        {
            if (chbCreate.Checked)
            {
                CRUD_Create();
            }

            if (chbDelete.Checked)
            {
                CRUD_Delete();
            }
        }
        /// <summary>
        /// Activa/Desactiva la seccion de crear del CRUD.
        /// </summary>
        public void CRUD_Create()
        {
            try
            {
                txtCreateID.Text = null;
                pcbCreate.Image = pcbCreate.InitialImage;
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }

        }
        /// <summary>
        /// Activa/Desactiva la seccion de borrar del CRUD.
        /// </summary>
        public void CRUD_Delete()
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<AddOnID> ProductDB = database.GetCollection<AddOnID>("MasterAddOn");

                pcbDelete.Image = pcbDelete.InitialImage;
                List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                cbDeleteID.Items.Clear();
                foreach (string item in tt1)
                {
                    cbDeleteID.Items.Add(item);
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
        /// Inicializa los nombre de todos los textos segun el idioma.
        /// </summary>
        private void SetUIMessages()
        {
            if (_EnglishChecked)
            {
                gbOptions.Text = Properties.ResourceUI_en_US.T019;
                chbCreate.Text = Properties.ResourceUI_en_US.T020;
                chbDelete.Text = Properties.ResourceUI_en_US.T022;
                lblImg.Text = Properties.ResourceUI_en_US.T024;
                btnOpenCreate.Text = Properties.ResourceUI_en_US.T018;
                btnCreate.Text = Properties.ResourceUI_en_US.T020;
                btnDelete.Text = Properties.ResourceUI_en_US.T022;
            }
            else
            {
                gbOptions.Text = Properties.ResourceUI_es_CO.T019;
                chbCreate.Text = Properties.ResourceUI_es_CO.T020;
                chbDelete.Text = Properties.ResourceUI_es_CO.T022;
                lblImg.Text = Properties.ResourceUI_es_CO.T024;
                btnOpenCreate.Text = Properties.ResourceUI_es_CO.T018;
                btnCreate.Text = Properties.ResourceUI_es_CO.T020;
                btnDelete.Text = Properties.ResourceUI_es_CO.T022;
            }
        }
        /// <summary>
        /// Boton para pedir ayuda.
        /// </summary>
        private void AddOn_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.AddOn(19, _EnglishChecked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gbOptions_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(11, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void pcbDelete_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(12, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void pcbCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(13, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void txtCreateID_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(14, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbDeleteID_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(15, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnOpenCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(16, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnDelete_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(17, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddOn(18, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddOn(1, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
