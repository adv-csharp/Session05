using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session05.UI
{
    public partial class UCProduct : UCBase
    {
        public UCProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
             * using (var db ...)
             * p = new product { unitPrice = txtPrice.value}
             * db.products.add(p)
             * db.saveChages()
             */
        }
    }
}
