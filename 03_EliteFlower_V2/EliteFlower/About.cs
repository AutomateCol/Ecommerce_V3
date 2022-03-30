using System;
using System.Windows.Forms;

namespace EliteFlower
{
    public partial class About : Form
    {
        // -----------------------------------
        //          Global variables         |
        // -----------------------------------

        Form _frm;
        bool _EnglishChecked;

        // -----------------------------------
        //               Form                |
        // -----------------------------------

        public About(EliteFlower frm, bool english)
        {
            _frm = frm;
            _EnglishChecked = english;
            InitializeComponent();
        }

        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm.Enabled = true;
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (_EnglishChecked)
            {
                label1.Text = "Flowers";
                this.Text = "About";
            }
            else
            {
                label1.Text = "Flores";
                this.Text = "Acerca de";
            }
        }
    }
}
