using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class BudgetForm : Form
    {
        private IBudgetService _budgetService;
        private ICategoryService _categoryService;
        private string _mode;
        private Budget _budget;

        public BudgetForm(string mode, Budget budget,
            IBudgetService budgetService, ICategoryService categoryService)
        {
            InitializeComponent();
            _mode = mode;
            _budget = budget;
            _budgetService = budgetService;
            _categoryService = categoryService;
        }

        private void LoadCategories()
        {
            var categories = ((ICategoryService)_categoryService).GetAll();
            cmbCategory.Items.Clear();
            foreach (var cat in categories)
                cmbCategory.Items.Add(cat);
            cmbCategory.DisplayMember = "CategoryName";
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void BudgetForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            if (_mode == "Edit" && _budget != null)
            {
                txbBudgetAmount.Text = _budget.BudgetAmount.ToString();
                txbSpentAmount.Text = _budget.SpentAmount.ToString();
                dtpStartDate.Value = _budget.StartDate;
                dtpEndDate.Value = _budget.EndDate;
                foreach (var item in cmbCategory.Items)
                {
                    var cat = item as Category;
                    if (cat != null && cat.CategoryId == _budget.CategoryId)
                    {
                        cmbCategory.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Today.AddMonths(1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txbBudgetAmount.Text))
            {
                MessageBox.Show("Budget Amount is required!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txbBudgetAmount.Text, out decimal budgetAmount))
            {
                MessageBox.Show("Please enter a valid budget amount!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (budgetAmount <= 0)
            {
                MessageBox.Show("Budget amount must be greater than zero!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txbSpentAmount.Text, out decimal spentAmount))
            {
                spentAmount = 0;
            }
            if (dtpStartDate.Value >= dtpEndDate.Value)
            {
                MessageBox.Show("End date must be after start date!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCategory = cmbCategory.SelectedItem as Category;

            if (_mode == "Add")
            {
                var budget = new Budget
                {
                    BudgetId = Guid.NewGuid().ToString(),
                    CategoryId = selectedCategory.CategoryId,
                    BudgetAmount = budgetAmount,
                    SpentAmount = spentAmount,
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value
                };
                ((IBudgetService)_budgetService).Add(budget);
            }
            else if (_mode == "Edit")
            {
                _budget.CategoryId = selectedCategory.CategoryId;
                _budget.BudgetAmount = budgetAmount;
                _budget.SpentAmount = spentAmount;
                _budget.StartDate = dtpStartDate.Value;
                _budget.EndDate = dtpEndDate.Value;
                ((IBudgetService)_budgetService).Update(_budget);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}