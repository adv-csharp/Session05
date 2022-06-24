using Session05.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session05.UI
{
    public partial class UCProductList : UCBase
    {
        public UCProductList()
        {
            InitializeComponent();
        }

        private void UCProductList_Load(object sender, EventArgs e)
        {
            using (var db = new NorthwindEntities())
            {
                dropDownCategories.DisplayMember = "CategoryName";
                dropDownCategories.ValueMember = "CategoryId";
                var cats = db.Categories.Select(c => new
                {
                    CategoryName = c.CategoryName,
                    CategoryId = c.CategoryID
                }).ToList();
                //cats.Insert(0, new { CategoryId = -1, CategoryName = "all" }); 🐞
                cats.Insert(0, new { CategoryName = "all", CategoryId = -1 });
                dropDownCategories.DataSource = cats;

            }
            loadData();
        }

        private void loadData()
        {
            using (var db = new NorthwindEntities())
            {
                //lazy - LINQ
                // IQueryable
                //  samte sql nemiran
                // IEnumerable
                //  data to c# (memory) miyad

                //toList -> IQueryable ejra to db -> data return


                //dataGridView1.DataSource  = db.Products.Include(x => x.Category).ToList();

                //Anaynemouse Object
                var catId = Convert.ToInt32(dropDownCategories.SelectedValue);
                var query = db.Products.Include(x => x.Category).Select(p => new
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    Discontinued = p.Discontinued,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category.CategoryName,
                });                                  
                if(catId > -1)
                {
                    query = query.Where(p => p.CategoryID == catId);
                }

                var list = query.OrderByDescending(x => x.ProductID).ToList();                
                dataGridView1.DataSource = list;
            }
        }

        private void dropDownCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductId"].Value);
            if(dataGridView1.CurrentCell.OwningColumn.Name == "Edit")
            {
                (Parent.Parent as Form1).NewProduct(id);
            }
            if (dataGridView1.CurrentCell.OwningColumn.Name == "Delete")
            {
                /**
                 * DELETE
                 * using (var db ...)
                 * p = db.find(id)
                 * db.products.remove(p)
                 * db.saveChages()
                 */

                if(MessageBox.Show("مطمئن هستید؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                using (var db = new NorthwindEntities())
                {
                    var p = db.Products.Find(id);
                    db.Products.Remove(p);
                    db.SaveChanges();
                }

                loadData();
            }
        }
    }
}
