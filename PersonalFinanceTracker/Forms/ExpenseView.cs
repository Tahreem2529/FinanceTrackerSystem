using App.Core.Contracts;
using App.Core.Models;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class ExpensesView : UserControl
    {
        private IExpenseService _expenseService;
        private ICategoryService _categoryService;
        private List<Expense> _allExpenses;

        public ExpensesView(string connString)
        {
            InitializeComponent();
            _expenseService = new DbExpenseService(connString);
            _categoryService = new DbCategoryService(connString);
        }

        private void LoadExpenses()
        {
            _allExpenses = ((IExpenseService)_expenseService).GetAll();
            var categories = ((ICategoryService)_categoryService).GetAll();

            var display = _allExpenses.Select(e => new
            {
                e.ExpenseId,
                Category = categories.FirstOrDefault(
                    c => c.CategoryId == e.CategoryId)?.CategoryName ?? "Unknown",
                e.Description,
                e.Amount,
                e.ExpenseDate
            }).ToList();

            dgvExpense.DataSource = null;
            dgvExpense.DataSource = display;
            dgvExpense.Columns["ExpenseId"].Visible = false;
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

        private void ExpensesView_Load(object sender, EventArgs e)
        {
            LoadCategoriesFilter();
            LoadExpenses();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var form = new ExpenseForm("Add", null, _expenseService, _categoryService);
            form.ShowDialog();
            LoadExpenses();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpense.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to edit!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = dgvExpense.SelectedRows[0].Cells["ExpenseId"].Value.ToString();
            var expense = ((IExpenseService)_expenseService).GetById(id);
            var form = new ExpenseForm("Edit", expense, _expenseService, _categoryService);
            form.ShowDialog();
            LoadExpenses();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpense.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense to delete!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = MessageBox.Show("Are you sure you want to delete?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string id = dgvExpense.SelectedRows[0].Cells["ExpenseId"].Value.ToString();
                ((IExpenseService)_expenseService).Delete(id);
                LoadExpenses();
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadCategoriesFilter();
            LoadExpenses();
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allExpenses == null) return;
            ApplyFilter();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_allExpenses == null) return;
            ApplyFilter();
        }
        
        private void ApplyFilter()
        {
            var categories = ((ICategoryService)_categoryService).GetAll();
            var filtered = _allExpenses;

            if (!string.IsNullOrWhiteSpace(txbSearch.Text))
            {
                string search = txbSearch.Text.ToLower();
                filtered = filtered.Where(ex =>
                    ex.Description != null &&
                    ex.Description.ToLower().Contains(search)).ToList();
            }

            if (cmbCategory.SelectedIndex > 0)
            {
                string selectedCategory = cmbCategory.SelectedItem.ToString();
                var cat = categories.FirstOrDefault(c => c.CategoryName == selectedCategory);
                if (cat != null)
                    filtered = filtered.Where(ex => ex.CategoryId == cat.CategoryId).ToList();
            }

            var display = filtered.Select(ex => new
            {
                ex.ExpenseId,
                Category = categories.FirstOrDefault(
                    c => c.CategoryId == ex.CategoryId)?.CategoryName ?? "Unknown",
                ex.Description,
                ex.Amount,
                ex.ExpenseDate
            }).ToList();

            dgvExpense.DataSource = null;
            dgvExpense.DataSource = display;
            if (dgvExpense.Columns.Contains("ExpenseId"))
                dgvExpense.Columns["ExpenseId"].Visible = false;
        }
    }
}