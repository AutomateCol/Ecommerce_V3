using EliteFlower.Methods;
using System;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class Overview : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        private int _State;
        bool _EnglishChecked;

        // -----------------------------------
        //               Form                |
        // -----------------------------------

        /// <summary>
        /// Inicializa las variables globales.
        /// </summary>
        public Overview(EliteFlower frm, bool english)
        {
            _State = 1;
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }
        /// <summary>
        /// Carga la informacion que debe mostrar
        /// </summary>
        private void Overview_Load(object sender, EventArgs e)
        {
            rbPage1.Checked = true;
            label1.Text = UIMessages.Overview(13, _EnglishChecked);
            btnPrevious.Text = UIMessages.Overview(14, _EnglishChecked);
            btnNext.Text = UIMessages.Overview(15, _EnglishChecked);
            btnGetStarted.Text = UIMessages.Overview(16, _EnglishChecked);
            this.Text = UIMessages.Overview(17, _EnglishChecked);
        }
        /// <summary>
        /// Cierra la ventana y desbloquela de la ventana padre donde vino.
        /// </summary>
        private void Overview_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }

        // --------------------------------------
        //              Components              |
        // --------------------------------------

        /// <summary>
        /// Muestra la informacion de la pagina 1.
        /// </summary>
        private void rbPage1_CheckedChanged(object sender, EventArgs e)
        {
            _State = 1;
            lblTitle.Text = UIMessages.Overview(1, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(2, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img1;
            btnPrevious.Visible = false;
            btnNext.Visible = true;
            btnGetStarted.Visible = false;
        }
        /// <summary>
        /// Muestra la informacion de la pagina 2.
        /// </summary>
        private void rbPage2_CheckedChanged(object sender, EventArgs e)
        {
            _State = 2;
            lblTitle.Text = UIMessages.Overview(3, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(4, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img2;
            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnGetStarted.Visible = false;
        }
        /// <summary>
        /// Muestra la informacion de la pagina 3.
        /// </summary>
        private void rbPage3_CheckedChanged(object sender, EventArgs e)
        {
            _State = 3;
            lblTitle.Text = UIMessages.Overview(5, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(6, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img3;
            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnGetStarted.Visible = false;
        }
        /// <summary>
        /// Muestra la informacion de la pagina 4.
        /// </summary>
        private void rbPage4_CheckedChanged(object sender, EventArgs e)
        {
            _State = 4;
            lblTitle.Text = UIMessages.Overview(7, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(8, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img4;
            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnGetStarted.Visible = false;
        }
        /// <summary>
        /// Muestra la informacion de la pagina 5.
        /// </summary>
        private void rbPage5_CheckedChanged(object sender, EventArgs e)
        {
            _State = 5;
            lblTitle.Text = UIMessages.Overview(9, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(10, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img5;
            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnGetStarted.Visible = false;
        }
        /// <summary>
        /// Muestra la informacion de la pagina 6.
        /// </summary>
        private void rbPage6_CheckedChanged(object sender, EventArgs e)
        {
            _State = 6;
            lblTitle.Text = UIMessages.Overview(11, _EnglishChecked);
            rtbText.Text = UIMessages.Overview(12, _EnglishChecked);
            pictureBox1.Image = Properties.Resources.img6;
            btnPrevious.Visible = true;
            btnNext.Visible = false;
            btnGetStarted.Visible = true;
        }
        /// <summary>
        /// Se devuelve a la pagina anterior.
        /// </summary>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _State -= 1;
            switch (_State)
            {
                case 1: rbPage1.Checked = true; break;
                case 2: rbPage2.Checked = true; break;
                case 3: rbPage3.Checked = true; break;
                case 4: rbPage4.Checked = true; break;
                case 5: rbPage5.Checked = true; break;
                case 6: rbPage6.Checked = true; break;
                default: rbPage1.Checked = true; break;
            }
        }
        /// <summary>
        /// Pasa a la siguiente pagina.
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            _State += 1;
            switch (_State)
            {
                case 1: rbPage1.Checked = true; break;
                case 2: rbPage2.Checked = true; break;
                case 3: rbPage3.Checked = true; break;
                case 4: rbPage4.Checked = true; break;
                case 5: rbPage5.Checked = true; break;
                case 6: rbPage6.Checked = true; break;
                default: rbPage1.Checked = true; break;
            }
        }
        /// <summary>
        /// Sale de la ventana y le indica que no la debe mostrar otra vez.
        /// </summary>
        private void btnGetStarted_Click(object sender, EventArgs e)
        {
            Mongoose.SetOverview();
            this.Close();
        }
    }
}
