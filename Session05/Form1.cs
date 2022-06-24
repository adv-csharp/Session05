using Session05.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void btnProsuctList_Click(object sender, EventArgs e)
        {
            var uc = new UCProductList();
            uc.Width = panelMain.Width;
            uc.Height = panelMain.Height;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(uc);
        }
    }
}
