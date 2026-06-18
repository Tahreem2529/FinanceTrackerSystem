using App.Core.Contracts;
using App.Core.Services;
using PersonalFinanceTracker.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class DashboardForm : Form
    {
        private string _connString;
        private readonly Dictionary<Type, UserControl> _views
            = new Dictionary<Type, UserControl>();

        public DashboardForm()
        {
            InitializeComponent();
            
                _connString = ConfigurationManager
                    .ConnectionStrings["FinanceTrackerDB"].ConnectionString;
                this.WindowState = FormWindowState.Maximized;
            }
           
        

        private void ShowView<T>(Func<T> factory) where T : UserControl
        {
            var key = typeof(T);
            if (!_views.TryGetValue(key, out var view))
            {
                view = factory();
                _views.Add(key, view);
                view.Dock = DockStyle.Fill;
            }
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(view);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Dashboard view baad mein banayenge
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            ShowView(() => new ExpensesView(_connString));
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            ShowView(() => new CategoryView(_connString));
        }

        private void btnBudgets_Click(object sender, EventArgs e)
        {
            ShowView(() => new BudgetView(_connString));
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowView(() => new ReportsView(_connString));
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Settings baad mein
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}