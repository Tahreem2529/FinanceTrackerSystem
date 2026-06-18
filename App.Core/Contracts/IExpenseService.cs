using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        Expense GetById(string id);
         void Add(Expense expense);
        void Update(Expense expense);

        void Delete(string id);
    }
}
