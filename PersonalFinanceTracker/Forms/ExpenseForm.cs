using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class ExpenseForm : Form
    {
        private IExpenseService _expenseService;
        private ICategoryService _categoryService;
        private string _mode;
        private Expense _expense;

        public ExpenseForm(string mode, Expense expense,
            IExpenseService expenseService, ICategoryService categoryService)
        {
            InitializeComponent();
            _mode = mode;
            _expense = expense;
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        private void ExpenseForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            if (_mode == "Edit" && _expense != null)
            {
                txbDescription.Text = _expense.Description;
                txbAmount.Text = _expense.Amount.ToString();
                dtpExpense.Value = _expense.ExpenseDate;
                // Select category in combobox
                foreach (var item in cmbCategory.Items)
                {
                    var cat = item as Category;
                    if (cat != null && cat.CategoryId == _expense.CategoryId)
                    {
                        cmbCategory.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                dtpExpense.Value = DateTime.Today;
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txbAmount.Text))
            {
                MessageBox.Show("Amount is required!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txbAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (amount <= 0)
            {
                MessageBox.Show("Amount must be greater than zero!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCategory = cmbCategory.SelectedItem as Category;

            if (_mode == "Add")
            {
                var expense = new Expense
                {
                    ExpenseId = Guid.NewGuid().ToString(),
                    CategoryId = selectedCategory.CategoryId,
                    Description = txbDescription.Text,
                    Amount = amount,
                    ExpenseDate = dtpExpense.Value
                };
                ((IExpenseService)_expenseService).Add(expense);
            }
            else if (_mode == "Edit")
            {
                _expense.CategoryId = selectedCategory.CategoryId;
                _expense.Description = txbDescription.Text;
                _expense.Amount = amount;
                _expense.ExpenseDate = dtpExpense.Value;
                ((IExpenseService)_expenseService).Update(_expense);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}