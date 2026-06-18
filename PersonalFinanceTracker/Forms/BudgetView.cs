using App.Core.Contracts;
using App.Core.Models;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class BudgetView : UserControl
    {
        private IBudgetService _budgetService;
        private ICategoryService _categoryService;
        private List<Budget> _allBudgets;

        public BudgetView(string connString)
        {
            InitializeComponent();
            _budgetService = new DbBudgetService(connString);
            _categoryService = new DbCategoryService(connString);
        }

        private void LoadBudgets()
        {
            _allBudgets = ((IBudgetService)_budgetService).GetAll();
            var categories = ((ICategoryService)_categoryService).GetAll();

            var display = _allBudgets.Select(b => new
            {
                b.BudgetId,
                Category = categories.FirstOrDefault(
                    c => c.CategoryId == b.CategoryId)?.CategoryName ?? "Unknown",
                b.BudgetAmount,
                b.SpentAmount,
                b.StartDate,
                b.EndDate
            }).ToList();

            dgvBudget.DataSource = null;
            dgvBudget.DataSource = display;
            dgvBudget.Columns["BudgetId"].Visible = false;
        }

        private void LoadCategoriesFilter()
        {
            var categories = ((ICategoryService)_categoryService).GetAll();
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");
            foreach (var cat in categories)
                cmbCategory.Items.Add(cat.CategoryName);
            cmbCategory.SelectedIndex = 0;
        }

        private void BudgetView_Load(object sender, EventArgs e)
        {
            LoadCategoriesFilter();
            LoadBudgets();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var form = new BudgetForm("Add", null, _budgetService, _categoryService);
            form.ShowDialog();
            LoadBudgets();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgvBudget.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a budget to edit!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = dgvBudget.SelectedRows[0].Cells["BudgetId"].Value.ToString();
            var budget = ((IBudgetService)_budgetService).GetById(id);
            var form = new BudgetForm("Edit", budget, _budgetService, _categoryService);
            form.ShowDialog();
            LoadBudgets();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dgvBudget.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a budget to delete!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show("Are you sure you want to delete?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string id = dgvBudget.SelectedRows[0].Cells["BudgetId"].Value.ToString();
                ((IBudgetService)_budgetService).Delete(id);
                LoadBudgets();
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadCategoriesFilter();
            LoadBudgets();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_allBudgets == null) return;
            var categories = ((ICategoryService)_categoryService).GetAll();
            var filtered = _allBudgets;

            if (cmbCategory.SelectedIndex > 0)
            {
                string selectedCategory = cmbCategory.SelectedItem.ToString();
                var cat = categories.FirstOrDefault(c => c.CategoryName == selectedCategory);
                if (cat != null)
                    filtered = filtered.Where(b => b.CategoryId == cat.CategoryId).ToList();
            }

            var display = filtered.Select(b => new
            {
                b.BudgetId,
                Category = categories.FirstOrDefault(
                    c => c.CategoryId == b.CategoryId)?.CategoryName ?? "Unknown",
                b.BudgetAmount,
                b.SpentAmount,
                b.StartDate,
                b.EndDate
            }).ToList();

            dgvBudget.DataSource = null;
            dgvBudget.DataSource = display;
            if (dgvBudget.Columns.Contains("BudgetId"))
                dgvBudget.Columns["BudgetId"].Visible = false;
        }
    }
}