using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EliteFlower.Exceptions;
using EliteFlower.Methods;
using EliteFlower.Models;
using MongoDB.Driver;


namespace EliteFlower
{
    public partial class BalanceTemplate : Form
    {

        // -----------------------------------
        //          Global variables         |
        // -----------------------------------
        Form _frm;
        bool _EnglishChecked;
        List<int> packages;
        string mongoDBConnection = "mongodb://localhost:27017";
        Product product;

        bool flagDelete = false;


        // -----------------------------------
        //              Form                 |
        // -----------------------------------

        public BalanceTemplate(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();


        }



        // -----------------------------------
        //            Load Data              |
        // -----------------------------------

         
        private void BalanceTemplate_Load_Data()
        {

             
            //Mongoose.DeleteCountIDs("Data");
            //Mongoose.DeleteIDAddOn();
            //Mongoose.LoadCountIDs("Data", 1);
            //Mongoose.GetDistinctAddOn("Data", 10);

            //Mongoose.SetWorkUpFalse();
            //Mongoose.SetWorkMesaninFalse();


            Mongoose.LoadCountIDs("Data", 1);
            Mongoose.GetDistinctAddOn("Data", 10);


            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();

            List<string> nameVS = FillVases(Mongoose.GetMasterProducts("MasterProduct"));
            //List<string> nameAD = FillVases(Mongoose.GetNamesAddOn("Data"));
            Utils.SetComboBox(nameVS, new List<ComboBox> { CB_ID1_1, CB_ID2_1, CB_ID3_1 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { CB_ID1_2, CB_ID2_2, CB_ID3_2 });
            Utils.SetComboBox(nameVS, new List<ComboBox> { CB_ID1_3, CB_ID2_3, CB_ID3_3 });
            bool checkbool = CheckDB(Mongoose.GetNameVases("Data"), true);



        }

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
                string msg = "gcgjcfhjv";
                foreach (string item in noDB)
                {
                    msg += $"ID: {item}\n";
                }
                msg += "shshshs";
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

        private List<string> FillVases(List<string> vases)
        {
            List<String> nameVs = vases.Distinct().ToList();
            nameVs.Add("NV");
            return nameVs;
        }

        //public static List<string> GetNameVases(string document)
        //{
        //    MongoClient client = new MongoClient("mongodb://localhost:27017");
        //    IMongoDatabase database = client.GetDatabase("EliteFlower");
        //    IMongoCollection<VaseCount> IDDistinctDB = database.GetCollection<VaseCount>("BalanceIDProducts");
        //    return IDDistinctDB.Find(s => s.File == document).ToList().Select(s => s.Vase).ToList();
        //}

        /// Habilita la ventana que abrio esta.   
        public void BalanceTemplate_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }



        // -----------------------------------
        //     Create Data                   |
        // -----------------------------------


        private void btnCreate_Click(object sender, EventArgs e)
        {
            check_percentage();
            Console.WriteLine(check_percentage());

            if (check_percentage()) { 
            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Btemplate> ProductDB = database.GetCollection<Btemplate>("BalanceTemplate");


                string IdText = textBox1.Text;
                Console.WriteLine(IdText);
                string S1_1 = (string)CB_ID1_1.SelectedItem;
                string S1_2 = (string)CB_ID2_1.SelectedItem;
                string S1_3 = (string)CB_ID3_1.SelectedItem;

                string S2_1 = (string)CB_ID1_2.SelectedItem;
                string S2_2 = (string)CB_ID2_2.SelectedItem;
                string S2_3 = (string)CB_ID3_2.SelectedItem;

                string S3_1 = (string)CB_ID1_3.SelectedItem;
                string S3_2 = (string)CB_ID2_3.SelectedItem;
                string S3_3 = (string)CB_ID3_3.SelectedItem;


                string P1 = S1_T1.Text;
                string P2 = S1_T2.Text;
                string P3 = S1_T3.Text;
                string P4 = S2_T1.Text;
                string P5 = S2_T2.Text;
                string P6 = S2_T3.Text;
                string P7 = S3_T1.Text;
                string P8 = S3_T2.Text;
                string P9 = S3_T3.Text;



                string[] config = { S1_1, S1_2, S1_3, S2_1, S2_2, S2_3, S3_1, S3_2, S3_3 };
                string[] percentage = { P1, P2, P3, P4, P5, P6, P7, P8, P9 };
  
                for (int i = 0; i < percentage.Length; i++)
                {
                    Console.WriteLine(percentage[i]);
                }



                if (true)
                {
                    if (IdText != null && IdText != "")  
                    {
                        Btemplate template = new Btemplate() { ID = IdText, Template = config, Percentage=percentage };

                        int NoCreated = ProductDB.Find(d => d.ID == IdText).ToList().Count;
                        if (NoCreated == 0)
                        {

                            ProductDB.InsertOne(template);
                            MessageBox.Show(string.Format(UIMessages.AddProduct(2, _EnglishChecked), IdText), UIMessages.AddProduct(18, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            else
            {
                MessageBox.Show("Percentage values dont sum up to 100%");
            }

        }



        private void BalanceTemplate_Load_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            mongoDBConnection = Mongoose.GetMongoDBConnection();

            BalanceTemplate_Load_Data();
            chbCreate.Checked = true;
            //_frm.Enabled = true;


        }

        private void chbCreate_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCreate.Checked)
            {
                CB_ID1_1.Visible = true;
                CB_ID2_1.Visible = true;
                CB_ID2_1.Visible = true;

                CB_ID1_2.Visible = true;
                CB_ID2_2.Visible = true;
                CB_ID3_2.Visible = true;

                CB_ID1_3.Visible = true;
                CB_ID2_3.Visible = true;
                CB_ID3_3.Visible = true;

                CB_ID1_1.Enabled = true;
                CB_ID2_1.Enabled = true;
                CB_ID3_1.Enabled = true;
                                   
                CB_ID1_2.Enabled = true;
                CB_ID2_2.Enabled = true;
                CB_ID3_2.Enabled = true;
                                  
                CB_ID1_3.Enabled = true;
                CB_ID2_3.Enabled = true;
                CB_ID3_3.Enabled = true;

                textBox1.Visible = true;
                Update_template.Visible = false;
                Del_Template.Visible = false;
                btnCreate.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;

                chbCreate.Checked = true;
                chbUpdate.Checked = false;
                chbDelete.Checked = false;

                S1_T1.Text = "0";
                S1_T2.Text = "0";
                S1_T3.Text = "0";

                S2_T1.Text = "0";
                S2_T2.Text = "0";
                S2_T3.Text = "0";

                S3_T1.Text = "0";
                S3_T2.Text = "0";
                S3_T3.Text = "0";  

               //if (CB_ID1_1.SelectedItem == "NV")
               // {
               //     S1_T1.Enabled = false;
               // }
               // else
               // {
               //     S1_T1.Enabled = true;
               // }
            }
            RefreshCRUD();
        }

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

                //pcbCreate.Image = pcbCreate.InitialImage;
                //pcbUpdate.Image = pcbUpdate.InitialImage;
                //pcbDelete.Image = pcbDelete.InitialImage;
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }


        public void CRUD_Create()
        {
            try
            {
                textBox1.Text = null;
                Update_template.Items.Clear();
                //foreach (int item in packages)
                //{
                //    Update_template.Items.Add(item);
                //}
                //Update_template.SelectedIndex = 0;
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
                IMongoCollection<Btemplate> ProductDB = database.GetCollection<Btemplate>("BalanceTemplate");


                //foreach (int item in packages)
                //{
                //    cbUpdatePack.Items.Add(item);
                //}
                //cbUpdatePack.SelectedItem = packages[0];
                List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                Update_template.Items.Clear();
                foreach (string item in tt1)
                {
                    Update_template.Items.Add(item);
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
                IMongoCollection<Btemplate> ProductDB = database.GetCollection<Btemplate>("BalanceTemplate");

                List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                Del_Template.Items.Clear();
                foreach (string item in tt1)
                {
                    Del_Template.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Mongoose.LoadError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, GetType().FullName);
            }
        }


        private void chbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUpdate.Checked)
            {
                CB_ID1_1.Visible = true;
                CB_ID2_1.Visible = true;
                CB_ID2_1.Visible = true;

                CB_ID1_2.Visible = true;
                CB_ID2_2.Visible = true;
                CB_ID3_2.Visible = true;

                CB_ID1_3.Visible = true;
                CB_ID2_3.Visible = true;
                CB_ID3_3.Visible = true;

                textBox1.Visible = false;
                Update_template.Visible = true;
                Del_Template.Visible = false;
                btnCreate.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = false;

                chbCreate.Checked = false;
                chbUpdate.Checked = true;
                chbDelete.Checked = false;
            }
            RefreshCRUD();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {



            string S1_1 = (string)CB_ID1_1.SelectedItem;
            string S1_2 = (string)CB_ID2_1.SelectedItem;
            string S1_3 = (string)CB_ID3_1.SelectedItem;

            string S2_1 = (string)CB_ID1_2.SelectedItem;
            string S2_2 = (string)CB_ID2_2.SelectedItem;
            string S2_3 = (string)CB_ID3_2.SelectedItem;

            string S3_1 = (string)CB_ID1_3.SelectedItem;
            string S3_2 = (string)CB_ID2_3.SelectedItem;
            string S3_3 = (string)CB_ID3_3.SelectedItem;


            string[] config = { S1_1, S1_2, S1_3, S2_1, S2_2, S2_3, S3_1, S3_2, S3_3 };

            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Product> ProductDB = database.GetCollection<Product>("BalanceTemplate");

                string IdText = (string)Update_template.SelectedItem;
                Console.WriteLine(IdText);
                //int PackageText = (int)cbUpdatePack.SelectedItem;
                //string assetVase = $"{PATHVASES}\\{IdText}-vase.jpg";

                if (MessageBox.Show(UIMessages.AddProduct(17, _EnglishChecked), UIMessages.AddProduct(19, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdText != null)
                    {
                        //FileImg1.Save(assetVase);
                        Btemplate template = new Btemplate() { ID = IdText, Template = config };
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

        private void chbDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDelete.Checked)
            {
                CB_ID1_1.Visible = true;
                CB_ID2_1.Visible = true;
                CB_ID3_1.Visible = true;

                CB_ID1_2.Visible = true;
                CB_ID2_2.Visible = true;
                CB_ID3_2.Visible = true;

                CB_ID1_3.Visible = true;
                CB_ID2_3.Visible = true;
                CB_ID3_3.Visible = true;

                CB_ID1_1.Enabled = false;
                CB_ID2_1.Enabled = false;
                CB_ID3_1.Enabled = false;

                CB_ID1_2.Enabled = false;
                CB_ID2_2.Enabled = false;
                CB_ID3_2.Enabled = false;
                                   
                CB_ID1_3.Enabled = false;
                CB_ID2_3.Enabled = false;
                CB_ID3_3.Enabled = false;

                textBox1.Visible = false;
                Update_template.Visible = false;
                Del_Template.Visible = true;
                btnCreate.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = true;

                chbCreate.Checked = false;
                chbUpdate.Checked = false;
                chbDelete.Checked = true;

                flagDelete = true;
            }
            RefreshCRUD();


        }
        private void Del_Template_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Template();
        }


        private static List<Btemplate> GetTemplate(string texto)
        {

            Console.WriteLine(texto);


            MongoClient client = Mongoose.GetDBConnection();
            IMongoDatabase database = Mongoose.GetDataBase(client);
            IMongoCollection<Btemplate> TemplateDB = database.GetCollection<Btemplate>("BalanceTemplate");
            //List<Btemplate> doc = TemplateDB.Find(f => f._id== 1).ToList();

            List<Btemplate> doc = TemplateDB.Find(d => d.ID == texto).ToList();
            string id = doc[0].ID;
            string[] temp = doc[0].Template;
            Console.WriteLine(id);

            for (int i = 0; i < temp.Length; i++)
            {
                Console.WriteLine(temp[i]);
            }




            Console.WriteLine(doc);
            //List<Statics> Static = StaticsDB.Find(s => s.BalancedWork).ToList();

            //Console.WriteLine(documents.ToString());

            //foreach (var doc in documents)
            //{               
            //        var vases = doc.GetValue(i);         
            //    Console.WriteLine(vases);

            //
            //


            return doc;
        }



        public void Get_Template()
        {
            string texto = Del_Template.Text.ToString();
            Console.WriteLine("texto = ");
            Console.WriteLine(texto);


            if (texto != "")
            {
                List<Btemplate> doc = GetTemplate(texto);

                string id = doc[0].ID;
                string[] temp = doc[0].Template;
                string[] per = doc[0].Percentage;

                CB_ID1_1.Text = temp[0];
                CB_ID2_1.Text = temp[1];
                CB_ID3_1.Text = temp[2];

                CB_ID1_2.Text = temp[3];
                CB_ID2_2.Text = temp[4];
                CB_ID3_2.Text = temp[5];

                CB_ID1_3.Text = temp[6];
                CB_ID2_3.Text = temp[7];
                CB_ID3_3.Text = temp[8];


                S1_T1.Text = per[0];
                S1_T2.Text = per[1];
                S1_T3.Text = per[2];

                S2_T1.Text = per[3];
                S2_T2.Text = per[4];
                S2_T3.Text = per[5];

                S3_T1.Text = per[6];
                S3_T2.Text = per[7];
                S3_T3.Text = per[8];
            }
            else
            {
                //Console.Write("texto vacio");
            }

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            try
            {
                MongoClient client = new MongoClient(mongoDBConnection);
                IMongoDatabase database = client.GetDatabase("EliteFlower");
                IMongoCollection<Btemplate> ProductDB = database.GetCollection<Btemplate>("BalanceTemplate");

                string IdDelete = (string)Del_Template.SelectedItem;

                if (MessageBox.Show(UIMessages.AddProduct(6, _EnglishChecked), UIMessages.AddProduct(19, _EnglishChecked), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IdDelete != null)
                    {
                        //List<string> tt = ProductDB.Find(f => f.ID == IdDelete).ToList().Select(s => s.Url).ToList();
                        //FileInfo info = new FileInfo(tt[0]);
                        //if (info.Exists)
                        //    info.Delete();
                        ProductDB.DeleteOne(d => d.ID == IdDelete);

                        MessageBox.Show(string.Format(UIMessages.AddProduct(7, _EnglishChecked), IdDelete), UIMessages.AddProduct(18, _EnglishChecked), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        List<string> tt1 = ProductDB.Find(d => d.ID != "").ToList().Select(s => s.ID).ToList();
                        Del_Template.Items.Clear();
                        foreach (var item in tt1)
                        {
                            Del_Template.Items.Add(item);
                        }
                        //pcbDelete.Image = pcbDelete.InitialImage;
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

        private void Del_Template_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Get_Template();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void S1_T2_TextChanged(object sender, EventArgs e)
        {

        }

        public bool check_percentage()
        {

            bool check= false;
            string CB1 = CB_ID1_1.Text;
            string CB2 = CB_ID2_1.Text;
            string CB3 = CB_ID3_1.Text;

            string CB4 = CB_ID1_2.Text;
            string CB5 = CB_ID2_2.Text;
            string CB6 = CB_ID3_2.Text;

            string CB7 = CB_ID1_3.Text;
            string CB8 = CB_ID2_3.Text;
            string CB9 = CB_ID3_3.Text;

            int p1 = Int32.Parse(S1_T1.Text);
            int p2 = Int32.Parse(S1_T2.Text);
            int p3 = Int32.Parse(S1_T3.Text);

            int p4 = Int32.Parse(S2_T1.Text);
            int p5 = Int32.Parse(S2_T2.Text);
            int p6 = Int32.Parse(S2_T3.Text); 
             
            int p7 = Int32.Parse(S3_T1.Text);
            int p8 = Int32.Parse(S3_T2.Text);
            int p9 = Int32.Parse(S3_T3.Text);

            mongoDBConnection = Mongoose.GetMongoDBConnection();

            List<string> nameVS = Mongoose.GetMasterProducts("MasterProduct");

            string[] Vases = nameVS.ToArray();
            string[] CB_text = { CB1, CB2, CB3, CB4, CB5, CB6, CB7, CB8, CB9 };
            int[] p_values = { p1, p2, p3, p4, p5, p6, p7, p8, p9 };
            //string[] Vases = { "GINGER", "LARGE", "SLENDER"};

            //Console.WriteLine( "Master products="); 

            //for (int i = 0; i < Vases.Length; i++)
            //{
            //    Console.WriteLine(Vases[i]);
            //}

            for (int i = 0; i < Vases.Length; i++)
            {
                int suma = 0;

                for (int j=0; j< CB_text.Length; j++)
                {
                    if (Vases[i] == CB_text[j])
                    {
                        suma += p_values[j];

                        if (suma < 100 ^ suma > 100)
                        {

                            check = false;
                            Console.WriteLine("La suma es =");
                            Console.WriteLine(suma);
                        }

                        else if (suma==100)
                        {
                            check=true;
                        }
                    }
                }

            }

            return check;

        }

        private void CB_ID1_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID1_1.SelectedItem == "NV")
            {
                S1_T1.Enabled = false;
            }
            else
            {
                S1_T1.Enabled = true;
            }

        }

        private void CB_ID2_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID2_1.SelectedItem == "NV")
            {
                S1_T2.Enabled = false;
            }
            else
            {
                S1_T2.Enabled = true;
            }
        }

        private void CB_ID3_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID3_1.SelectedItem == "NV")
            {
                S1_T3.Enabled = false;
            }
            else
            {
                S1_T3.Enabled = true;
            }
        }

        private void CB_ID1_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID1_2.SelectedItem == "NV")
            {
                S2_T1.Enabled = false;
            }
            else
            {
                S2_T1.Enabled = true;
            }
        }

        private void CB_ID2_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID2_2.SelectedItem == "NV")
            {
                S2_T2.Enabled = false;
            }
            else
            {
                S2_T2.Enabled = true;
            }
        }

        private void CB_ID3_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID3_2.SelectedItem == "NV")
            {
                S2_T3.Enabled = false;
            }
            else
            {
                S2_T3.Enabled = true;
            }
        }

        private void CB_ID1_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID1_3.SelectedItem == "NV")
            {
                S3_T1.Enabled = false;
            }
            else
            {
                S3_T1.Enabled = true;
            }
        }

        private void CB_ID2_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ID2_3.SelectedItem == "NV")
            {
                S3_T2.Enabled = false;
            }
            else
            {
                S3_T2.Enabled = true;
            }
        }

        private void CB_ID3_3_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (CB_ID3_3.SelectedItem == "NV")
            {
                S3_T3.Enabled = false;
            }
            else
            {
                S3_T3.Enabled = true;
            }
        }
    }
}
