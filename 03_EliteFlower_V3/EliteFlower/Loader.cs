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
    public partial class Loader : Form
    {
        string _LoaderText = "";

        public Loader(string LoaderText)
        {
            _LoaderText = LoaderText;
            InitializeComponent();
        }

        private void Loader_Load(object sender, EventArgs e)
        {
            pcLoader.Location = new Point(
                    this.Width/2 - pcLoader.Width/2,
                    this.Height / 2 - pcLoader.Height/2 - 50
                );
            lblLoader.Text = _LoaderText;
            lblLoader.Location = new Point(
                    this.Width/2 - lblLoader.Width/2,
                    this.Height/2 - lblLoader.Height/2 + 50
                );
        }
    }
}
