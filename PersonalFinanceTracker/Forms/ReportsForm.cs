using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinanceTracker.Forms
{
    public partial class ReportsForm : Form
    {
        private string _connString;
        public ReportsForm(string connString)
        {
            InitializeComponent();
            _connString = connString;
        }
    }
}
