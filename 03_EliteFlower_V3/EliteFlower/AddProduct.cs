using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class AddProduct : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        Image FileImg1;
        List<int> packages;
       // readonly string PATHVASES = "C:\\Users\\aalej\\assetsVases";
        readonly string PATHVASES = "C:\\Users\\Usuario\\assetsVases";
        string mongoDBConnection = "mongodb://localhost:27017";
        Product product;

        // -----------------------------------
        //              Form                |
        // -----------------------------------

        /// <summary>
        /// Deshabilita la ventana que abrio esta.
        /// </summary>
        public AddProduct(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los componentes en la ventana.
        /// </summary>
        private void AddProduct_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessages();
            loadPackages();
            chbCreate.Checked = true;
        }
        /// <summary>
        /// Habilita la ventana que abrio esta.
        /// </summary>
        private void AddProduct_FormClosed(object sender, FormClosedEventArgs e)
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
                lblPack.Visible = true;
                lblImg.Visible = true;
                //-- Create
                txtCreateID.Visible = true;
                cbCreatePack.Visible = true;
                btnOpenCreate.Visible = true;
                btnCreate.Visible = true;
                pcbCreate.Visible = true;
                //-- Update
                cbUpdateID.Visible = false;
                cbUpdatePack.Visible = false;
                btnOpenUpdate.Visible = false;
                btnUpdate.Visible = false;
                pcbUpdate.Visible = false;
                //-- Delete
                cbDeleteID.Visible = false;
                btnDelete.Visible = false;
                pcbDelete.Visible = false;

                chbUpdate.Checked = false;
                chbDelete.Checked = false;
            }
            RefreshCRUD();
        }
        /// <summary>
        /// Habilita/Deshabilita la seccion del CRUD.
        /// </summary>
        private void chbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUpdate.Checked)
            {
                lblID.Visible = true;
                lblPack.Visible = true;
                lblImg.Visible = true;
                //-- Create
                txtCreateID.Visible = false;
                cbCreatePack.Visible = false;
                btnOpenCreate.Visible = false;
                btnCreate.Visible = false;
                pcbCreate.Visible = false;
                //-- Update
                cbUpdateID.Visible = true;
                cbUpdatePack.Visible = true;
                btnOpenUpdate.Visible = true;
                btnUpdate.Visible = true;
                pcbUpdate.Visible = true;
                //-- Delete
                cbDeleteID.Visible = false;
                btnDelete.Visible = false;
                pcbDelete.Visible = false;

                chbCreate.Checked = false;
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
                lblPack.Visible = false;
                lblImg.Visible = false;
                //-- Create
                txtCreateID.Visible = false;
                cbCreatePack.Visible = false;
                btnOpenCreate.Visible = false;
                btnCreate.Visible = false;
                pcbCreate.Visible = false;
                //-- Update
                cbUpdateID.Visible = false;
                cbUpdatePack.Visible = false;
                btnOpenUpdate.Visible = false;
                btnUpdate.Visible = false;
                pcbUpdate.Visible = false;
                //-- Delete
                cbDeleteID.Visible = true;
                btnDelete.Visible = true;
                pcbDelete.Visible = true;

                chbCreate.Checked = false;
                chbUpdate.Checked = false;
            }
            RefreshCRUD();
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
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");
               // Product product1 = new Product() { ID = "Prueba", Package = 4, Url = $"{PATHVASES}\\NoVase.jpg" };
               // ProductDB.InsertOne(product1);




                string IdText = txtCreateID.Text;
                int PackageText = (int)cbCreatePack.SelectedItem;
                string assetVase = $"{PATHVASES}\\{IdText}-vase.jpg";
                

                if (MessageBox.Show(UIMessages.AddProduct(16, _EnglishChecked), UIMessages.AddProduct(20, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdText != null && IdText != "")
                    {
                        Product product = new Product() { ID = IdText, Package = PackageText, Url = assetVase };
                             int NoCreated = ProductDB.Find(d => d.ID == IdText).ToList().Count;
                        if (NoCreated == 0)
                        {
                            if (FileImg1 == null)
                            {
                                FileImg1 = Image.FromFile($"{PATHVASES}\\NoVase.jpg");
                            }                            
                            //FileImg1.Save(assetVase);
                            ProductDB.InsertOne(product);
                            MessageBox.Show(string.Format(UIMessages.AddProduct(2, _EnglishChecked), IdText), UIMessages.AddProduct(18, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCreateID.Text = null;
                            cbCreatePack.SelectedIndex = 0;
                            pcbCreate.Image = pcbCreate.InitialImage;
                        }
                        else
                        {
                            throw new CreateReferenceExistException(string.Format(UIMessages.AddProduct(3, _EnglishChecked), IdText));
                        }
                    }
                    else
                    {
                        throw new CreateEmptyFieldsException();
                    }
                }
            }
            catch (CreateReferenceExistException exReference)
            {
                Mongoose.LoadError(exReference, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(exReference.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CreateEmptyFieldsException exEmpty)
            {
                Mongoose.LoadError(exEmpty, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.AddProduct(4, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
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
        /// Cargar la imagen del producto en la seccion de actualizar.
        /// </summary>
        private void btnOpenUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog f = new OpenFileDialog())
                {
                    f.Filter = "JPG(*.JPG)|*.jpg";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        FileImg1 = Image.FromFile(f.FileName);
                        pcbUpdate.Image = FileImg1;
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }

        }
        /// <summary>
        /// Actualiza el registro que tiene seleccionado en el ComboBox.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                string IdText = (string)cbUpdateID.SelectedItem;
                int PackageText = (int)cbUpdatePack.SelectedItem;
                string assetVase = $"{PATHVASES}\\{IdText}-vase.jpg";

                if (MessageBox.Show(UIMessages.AddProduct(17, _EnglishChecked), UIMessages.AddProduct(19, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdText != null)
                    {
                        FileImg1.Save(assetVase);
                        product = new Product() { ID = IdText, Package = PackageText, Url = assetVase };
                        ProductDB.ReplaceOne(d => d.ID == IdText, product);
                        MessageBox.Show(string.Format(UIMessages.AddProduct(5, _EnglishChecked), IdText), UIMessages.AddProduct(18, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new CreateEmptyFieldsException();
                    }
                }
            }
            catch (CreateEmptyFieldsException exEmpty)
            {
                Mongoose.LoadError(exEmpty, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.AddProduct(4, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }
        /// <summary>
        /// Muestra el registro segun el ID seleccionado en el ComboBox.
        /// </summary>
        private void cbUpdateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                List<Product> lst = ProductDB.Find(d => d.ID == (string)cbUpdateID.SelectedItem).ToList();
                if (lst.Count > 0)
                {
                    cbUpdatePack.SelectedItem = lst[0].Package;
                    FileStream fs = File.OpenRead(lst[0].Url);
                    pcbUpdate.Image = Image.FromStream(fs);
                    fs.Close();
                }
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
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                string IdDelete = (string)cbDeleteID.SelectedItem;

                if (MessageBox.Show(UIMessages.AddProduct(6, _EnglishChecked), UIMessages.AddProduct(19, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdDelete != null)
                    {
                        pcbDelete.Image = null;
                        List<string> tt = ProductDB.Find(f => f.ID == IdDelete).ToList().Select(s => s.Url).ToList();
                        FileInfo info = new FileInfo(tt[0]);
                        if (info.Exists)
                            info.Delete();
                        ProductDB.DeleteOne(d => d.ID == IdDelete);

                        MessageBox.Show(string.Format(UIMessages.AddProduct(7, _EnglishChecked), IdDelete), UIMessages.AddProduct(18, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            catch (CreateEmptyFieldsException exEmpty)
            {
                Mongoose.LoadError(exEmpty, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.AddProduct(4, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                List<Product> lst = ProductDB.Find(d => d.ID == (string)cbDeleteID.SelectedItem).ToList();
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
        /// Lee la cantidad de empaques de productos existen si este es cero
        /// no deja abrir la ventana.
        /// </summary>
        public void loadPackages()
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Package> ProductDB = database.GetCollection<Package>("MasterPackage");

                packages = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.package).ToList();

                if (packages.Count > 0)
                {
                    cbUpdatePack.Items.Clear();
                    foreach (int item in packages)
                    {
                        cbUpdatePack.Items.Add(item);
                    }
                    cbUpdatePack.SelectedItem = packages[0];
                }
                else
                {
                    if (MessageBox.Show(UIMessages.AddProduct(22, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }

        /// <summary>
        /// Dependiendo de cual checker este activo, muestra la parte del CRUD indicada.
        /// </summary>
        public void RefreshCRUD()
        {
            try
            {
                if (chbCreate.Checked)
                    CRUD_Create();

                if (chbUpdate.Checked)
                    CRUD_Update();

                if (chbDelete.Checked)
                    CRUD_Delete();

                pcbCreate.Image = pcbCreate.InitialImage;
                pcbUpdate.Image = pcbUpdate.InitialImage;
                pcbDelete.Image = pcbDelete.InitialImage;
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
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
                cbCreatePack.Items.Clear();
                foreach (int item in packages)
                {
                    cbCreatePack.Items.Add(item);
                }
                cbCreatePack.SelectedIndex = 0;
                pcbCreate.Image = Image.FromFile($"{PATHVASES}\\NoVase.jpg");
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }

        }
        /// <summary>
        /// Activa/Desactiva la seccion de actualizar del CRUD.
        /// </summary>
        public void CRUD_Update()
        {
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                cbUpdatePack.Items.Clear();
                foreach (int item in packages)
                {
                    cbUpdatePack.Items.Add(item);
                }
                cbUpdatePack.SelectedItem = packages[0];
                List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                cbUpdateID.Items.Clear();
                foreach (string item in tt1)
                {
                    cbUpdateID.Items.Add(item);
                }
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
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

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
                this.Text = Properties.ResourceUI_en_US.T007;
                gbOptions.Text = Properties.ResourceUI_en_US.T019;
                chbCreate.Text = Properties.ResourceUI_en_US.T020;
                chbUpdate.Text = Properties.ResourceUI_en_US.T021;
                chbDelete.Text = Properties.ResourceUI_en_US.T022;
                lblPack.Text = Properties.ResourceUI_en_US.T023;
                lblImg.Text = Properties.ResourceUI_en_US.T024;
                btnOpenCreate.Text = Properties.ResourceUI_en_US.T018;
                btnOpenUpdate.Text = Properties.ResourceUI_en_US.T018;
                btnCreate.Text = Properties.ResourceUI_en_US.T020;
                btnUpdate.Text = Properties.ResourceUI_en_US.T021;
                btnDelete.Text = Properties.ResourceUI_en_US.T022;
            }
            else
            {
                this.Text = Properties.ResourceUI_es_CO.T007;
                gbOptions.Text = Properties.ResourceUI_es_CO.T019;
                chbCreate.Text = Properties.ResourceUI_es_CO.T020;
                chbUpdate.Text = Properties.ResourceUI_es_CO.T021;
                chbDelete.Text = Properties.ResourceUI_es_CO.T022;
                lblPack.Text = Properties.ResourceUI_es_CO.T023;
                lblImg.Text = Properties.ResourceUI_es_CO.T024;
                btnOpenCreate.Text = Properties.ResourceUI_es_CO.T018;
                btnOpenUpdate.Text = Properties.ResourceUI_es_CO.T018;
                btnCreate.Text = Properties.ResourceUI_es_CO.T020;
                btnUpdate.Text = Properties.ResourceUI_es_CO.T021;
                btnDelete.Text = Properties.ResourceUI_es_CO.T022;
            }
        }
        /// <summary>
        /// Boton para pedir ayuda.
        /// </summary>
        private void AddProduct_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.AddProduct(1, _EnglishChecked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void gbOptions_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(8, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void txtCreateID_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(9, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbDeleteID_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(9, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbUpdateID_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(10, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbUpdatePack_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(11, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void cbCreatePack_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(11, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnOpenCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(12, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(13, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnUpdate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(14, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Da informacion acerca del funcionamiento de lo que no se sabe.
        /// </summary>
        private void btnDelete_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.AddProduct(15, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.AddProduct(21, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pcbDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
