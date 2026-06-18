using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core.Services;

namespace PersonalFinanceTracker.Forms
{
    public partial class CategoryForm : Form
    {
        private ICategoryService _categoryService;
        private string _mode;
        private Category _category;
        
        public CategoryForm(string mode, Category category, ICategoryService categoryService)
            {
                InitializeComponent();
                _mode = mode;
                _category = category;
                _categoryService = categoryService;
            }

       

        private void CategoryForm_Load(object sender, EventArgs e)
            {
                if (_mode == "Edit" && _category != null)
                {
                    txbCategoryName.Text = _category.CategoryName;
                    txbDescription.Text = _category.Description;
                }
            }

            private void btnSave_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txbCategoryName.Text))
                {
                    MessageBox.Show("Category Name is required!",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_mode == "Add")
                {
                    var category = new Category
                    {
                        CategoryId = Guid.NewGuid().ToString(),
                        CategoryName = txbCategoryName.Text,
                        Description = txbDescription.Text
                    };
                    ((ICategoryService)_categoryService).Add(category);
                }
                else if (_mode == "Edit")
                {
                    _category.CategoryName = txbCategoryName.Text;
                    _category.Description = txbDescription.Text;
                    ((ICategoryService)_categoryService).Update(_category);
                }
                this.Close();
            }

            private void btnCancel_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        }
    }
       