using App.Core.Contracts;
using App.Core.Models;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class CategoryView : UserControl
    {
        private ICategoryService _categoryService;
        private List<Category> _allCategories;

        public CategoryView(string connString)
        {
            InitializeComponent();
            _categoryService = new DbCategoryService(connString);
        }

        private void LoadCategories()
        {
            _allCategories = ((ICategoryService)_categoryService).GetAll();
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = _allCategories;
            dgvCategory.Columns["CategoryId"].Visible = false;
            dgvCategory.Columns["CategoryName"].HeaderText = "Category Name";
            dgvCategory.Columns["Description"].HeaderText = "Description";
        }

        private void CategoryView_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var form = new CategoryForm("Add", null, _categoryService);
            form.ShowDialog();
            LoadCategories();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to edit!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = dgvCategory.SelectedRows[0].Cells["CategoryId"].Value.ToString();
            var category = ((ICategoryService)_categoryService).GetById(id);
            var form = new CategoryForm("Edit", category, _categoryService);
            form.ShowDialog();
            LoadCategories();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to delete!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show("Are you sure you want to delete?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string id = dgvCategory.SelectedRows[0].Cells["CategoryId"].Value.ToString();
                ((ICategoryService)_categoryService).Delete(id);
                LoadCategories();
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbSearch.Text))
            {
                dgvCategory.DataSource = null;
                dgvCategory.DataSource = _allCategories;
                if (dgvCategory.Columns.Contains("CategoryId"))
                    dgvCategory.Columns["CategoryId"].Visible = false;
                return;
            }
            string search = txbSearch.Text.ToLower();
            var filtered = _allCategories.Where(c =>
                (c.CategoryName != null && c.CategoryName.ToLower().Contains(search)) ||
                (c.Description != null && c.Description.ToLower().Contains(search))).ToList();
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = filtered;
            if (dgvCategory.Columns.Contains("CategoryId"))
                dgvCategory.Columns["CategoryId"].Visible = false;
        }
    }
}