using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class AddPackage : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;
        string mongoDBConnection = "mongodb://localhost:27017";

        // ------------------------------
        //              Form            |
        // ------------------------------

        /// <summary>
        /// Inicializa las variables globales.
        /// </summary>
        public AddPackage(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa los componentes en la ventana.
        /// </summary>
        private void AddPackage_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();
            SetUIMessages();
            chbCreate.Checked = true;
        }
        /// <summary>
        /// Habilita la ventana que abrio esta.
        /// </summary>
        private void AddPackage_FormClosed(object sender, FormClosedEventArgs e)
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
                //-- Create
                btnCreate.Visible = true;
                txtCreatePackage.Visible = true;
                //-- Delete
                btnDelete.Visible = false;
                cbDeletePackage.Visible = false;
                //-- Others
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
                //-- Create
                btnCreate.Visible = false;
                txtCreatePackage.Visible = false;
                //-- Delete
                btnDelete.Visible = true;
                cbDeletePackage.Visible = true;
                //-- Others
                chbCreate.Checked = false;
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
                IMongoCollection<Package> PackageDB = database.GetCollection<Package>("MasterPackage");

                //Package prueba = new Package() { ID = "pack-p", package = 0 };

             
                string IdText = txtCreatePackage.Text;

                

                if (MessageBox.Show(UIMessages.Package(1, _EnglishChecked), UIMessages.Package(5, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdText != null && IdText != "")
                    {
                        
                        Package package = new Package() { ID = $"pack-{IdText}", package = Int32.Parse(IdText) };
                        int NoCreated = PackageDB.Find(d => d.ID == $"pack-{IdText}").ToList().Count;
                       
                        if (NoCreated == 0)
                        {
                            
                            PackageDB.InsertOne(package);
                            MessageBox.Show(string.Format(UIMessages.Package(7, _EnglishChecked), $"pack-{IdText}"), UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCreatePackage.Text = null;
                        }
                        else
                        {
                            throw new CreateReferenceExistException(string.Format(UIMessages.Package(8, _EnglishChecked), $"pack-{IdText}"));
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
                MessageBox.Show(UIMessages.Package(9, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                IMongoCollection<Package> PackageDB = database.GetCollection<Package>("MasterPackage");
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("MasterProduct");

                string IdDelete = (string)cbDeletePackage.SelectedItem;
                List<Package> infoDelete = PackageDB.Find(d => d.ID == IdDelete).ToList();

                if (MessageBox.Show(UIMessages.Package(2, _EnglishChecked), UIMessages.Package(4, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdDelete != null)
                    {
                        List<string> numReferences = ProductDB.Find(d => d.Package == infoDelete[0].package).ToList().Select(s => s.ID).ToList();
                        if (numReferences.Count > 0)
                        {
                            string msg = UIMessages.Package(11, _EnglishChecked);
                            foreach (string item in numReferences)
                            {
                                msg += string.Format(UIMessages.Package(12, _EnglishChecked), item);
                            }
                            throw new CreateRelationalFieldsException(msg);
                        }
                        else
                        {
                            PackageDB.DeleteOne(d => d.ID == IdDelete);

                            MessageBox.Show(string.Format(UIMessages.Package(10, _EnglishChecked), IdDelete), UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);

                            List<string> tt1 = PackageDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                            cbDeletePackage.Items.Clear();
                            foreach (string item in tt1)
                            {
                                cbDeletePackage.Items.Add(item);
                            }
                        }
                    }
                    else
                    {
                        throw new CreateEmptyFieldsException();
                    }
                }
            }
            catch (CreateRelationalFieldsException ex3)
            {
                Mongoose.LoadError(ex3, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(ex3.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CreateEmptyFieldsException ex2)
            {
                Mongoose.LoadError(ex2, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
                MessageBox.Show(UIMessages.Package(9, _EnglishChecked), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtCreatePackage.Text = null;
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
                IMongoCollection<Package> PackageDB = database.GetCollection<Package>("MasterPackage");

                List<string> tt1 = PackageDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                cbDeletePackage.Items.Clear();
                foreach (string item in tt1)
                {
                    cbDeletePackage.Items.Add(item);
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
                this.Text = Properties.ResourceUI_en_US.T023;
                lblID.Text = Properties.ResourceUI_en_US.T023;
                gbOptions.Text = Properties.ResourceUI_en_US.T019;
                chbCreate.Text = Properties.ResourceUI_en_US.T020;
                chbDelete.Text = Properties.ResourceUI_en_US.T022;
                btnCreate.Text = Properties.ResourceUI_en_US.T020;
                btnDelete.Text = Properties.ResourceUI_en_US.T022;
            }
            else
            {
                this.Text = Properties.ResourceUI_es_CO.T023;
                lblID.Text = Properties.ResourceUI_es_CO.T023;
                gbOptions.Text = Properties.ResourceUI_es_CO.T019;
                chbCreate.Text = Properties.ResourceUI_es_CO.T020;
                chbDelete.Text = Properties.ResourceUI_es_CO.T022;
                btnCreate.Text = Properties.ResourceUI_es_CO.T020;
                btnDelete.Text = Properties.ResourceUI_es_CO.T022;
            }
        }

        private void AddPackage_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(UIMessages.Package(18, _EnglishChecked), "EliteFlower", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void gbOptions_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Package(13, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbDeletePackage_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Package(14, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCreate_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Package(15, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Package(16, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCreatePackage_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string msg = UIMessages.Package(17, _EnglishChecked);
            MessageBox.Show(msg, UIMessages.Package(3, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCreatePackage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
