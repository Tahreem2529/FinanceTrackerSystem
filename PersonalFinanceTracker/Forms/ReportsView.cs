using App.Core.Contracts;
using App.Core.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class ReportsView : UserControl
    {
        private IExpenseService _expenseService;
        private ICategoryService _categoryService;
        private IBudgetService _budgetService;

        public ReportsView(string connString)
        {
            InitializeComponent();
            _expenseService = new DbExpenseService(connString);
            _categoryService = new DbCategoryService(connString);
            _budgetService = new DbBudgetService(connString);
        }

        private void ReportsView_Load(object sender, EventArgs e)
        {
            LoadSummary();
            LoadCharts();
        }

        private void LoadSummary()
        {
            var expenses = ((IExpenseService)_expenseService).GetAll();
            var budgets = ((IBudgetService)_budgetService).GetAll();

            decimal totalExpenses = expenses.Sum(ex => ex.Amount);
            decimal totalBudget = budgets.Sum(b => b.BudgetAmount);
            decimal remaining = totalBudget - totalExpenses;

            lblTotalExpenses.Text = $"Total Expenses: Rs. {totalExpenses:F2}";
            lblTotalBudget.Text = $"Total Budget: Rs. {totalBudget:F2}";
            lblRemaining.Text = $"Remaining: Rs. {remaining:F2}";
        }

        private void LoadCharts()
        {
            try
            {
                var expenses = ((IExpenseService)_expenseService).GetAll();
                var categories = ((ICategoryService)_categoryService).GetAll();

                // Chart 1 — Pie Chart — Category wise
                var pieChart = new PieChart();
                pieChart.Dock = DockStyle.Fill;

                if (expenses.Count > 0)
                {
                    var grouped = expenses
                        .GroupBy(ex => ex.CategoryId)
                        .Select(g => new
                        {
                            CategoryId = g.Key,
                            Total = g.Sum(ex => ex.Amount)
                        }).ToList();

                    var pieSeries = new List<ISeries>();
                    foreach (var item in grouped)
                    {
                        var cat = categories.FirstOrDefault(c => c.CategoryId == item.CategoryId);
                        string catName = cat != null ? cat.CategoryName : "Unknown";
                        pieSeries.Add(new PieSeries<decimal>
                        {
                            Name = catName,
                            Values = new[] { item.Total }
                        });
                    }
                    pieChart.Series = pieSeries.ToArray();
                    pieChart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
                }

                pnlChart1.Controls.Clear();
                pnlChart1.Controls.Add(pieChart);

                // Chart 2 — Bar Chart — Monthly Expenses
                var barChart = new CartesianChart();
                barChart.Dock = DockStyle.Fill;

                if (expenses.Count > 0)
                {
                    var monthly = expenses
                        .GroupBy(ex => new { ex.ExpenseDate.Year, ex.ExpenseDate.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                        .Select(g => new
                        {
                            Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                            Total = g.Sum(ex => ex.Amount)
                        }).ToList();

                    barChart.Series = new ISeries[]
                    {
                new ColumnSeries<decimal>
                {
                    Name = "Monthly Expenses",
                    Values = monthly.Select(m => m.Total).ToArray()
                }
                    };

                    barChart.XAxes = new Axis[]
                    {
                new Axis { Labels = monthly.Select(m => m.Month).ToArray() }
                    };
                    barChart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
                }

                pnlChart2.Controls.Clear();
                pnlChart2.Controls.Add(barChart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chart error: " + ex.Message);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSummary();
            LoadCharts();
        }
}
}