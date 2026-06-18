using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class Expense
    {
        public string ExpenseId { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime ExpenseDate { get; set; }

    }
}
