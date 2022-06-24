using Session05.Model;
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
        public int? Id { get; }

        public UCProduct(int? id)
        {
            InitializeComponent();
            Id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
            resetForm();
        }

        private void resetForm()
        {
            textBoxName.Clear();
            textBoxPrice.Clear();
            dropDownCats.SelectedIndex = 0;
            dropDownCats.Focus();
        }

        private void save()
        {
            /**
             * NEW
             * using (var db ...)
             * p = new product { unitPrice = txtPrice.value}
             * db.products.add(p)
             * db.saveChages()
             */

            /**
             * UPDATE
             * using (var db ...)
             * p = db.find(id)
             * p.unitPrice = txtPrice.value
             * db.saveChages()
             */

            using (var db = new NorthwindEntities())
            {
                var msg = "محصول ایجاد شد";
                Product product = new Product();
                if (Id.HasValue)
                {
                    product = db.Products.Find(Id.Value);
                    msg = "محصول ویرایش شد";
                }
                else
                {
                    db.Products.Add(product);
                }

                product.ProductName = textBoxName.Text;
                product.UnitPrice = Convert.ToDecimal(textBoxPrice.Text);
                product.CategoryID = (int)(dropDownCats.SelectedValue);
                
                db.SaveChanges();
                MessageBox.Show(msg);
            }
        }

        private void UCProduct_Load(object sender, EventArgs e)
        {
            using (var db = new NorthwindEntities())
            {
                dropDownCats.DisplayMember = "CategoryName";
                dropDownCats.ValueMember = "CategoryId";
                dropDownCats.DataSource = db.Categories.Select(c => new
                {
                    CategoryName = c.CategoryName,
                    CategoryId = c.CategoryID
                }).ToList();
                if (Id.HasValue)
                {
                    //var product = db.Products.First(x => x.ProductID == Id.Value);
                    //NOT Found -> Exception, Multiple -> avali ro return mikone

                    //var product = db.Products.FirstOrDefault(x => x.ProductID == Id.Value);
                    //NOT Found -> null return, Multiple -> avali ro return mikone

                    //var product = db.Products.Single(x => x.ProductID == Id.Value);
                    //NOT Found -> Exception, Multiple -> Exception

                    //var product = db.Products.SingleOrDefault(x => x.ProductID == Id.Value);
                    //NOT Found -> null return, Multiple -> Exception


                    var product = db.Products.Find(Id.Value);
                    if (product!= null)
                    {
                        dropDownCats.SelectedValue = product.CategoryID;
                        textBoxName.Text = product.ProductName;
                        textBoxPrice.Text = product.UnitPrice.ToString();
                    }

                }
            }
        }

        private void btnSaveAndReturn_Click(object sender, EventArgs e)
        {
            save();
            (Parent.Parent as Form1).btnProsuctList.PerformClick();
        }
    }
}
