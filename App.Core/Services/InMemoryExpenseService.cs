using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class InMemoryExpenseService : IExpenseService
    {
       
            private List<Expense> _expenses;

            public InMemoryExpenseService()
            {
                _expenses = new List<Expense>();
            }

            List<Expense> IExpenseService.GetAll()
            {
                return _expenses.ToList();
            }

            Expense IExpenseService.GetById(string id)
            {
                return _expenses.FirstOrDefault(e => e.ExpenseId == id);
            }

            void IExpenseService.Add(Expense expense)
            {
                if (expense == null)
                    throw new ArgumentNullException("Expense is null");
                _expenses.Add(expense);
            }

            void IExpenseService.Update(Expense expense)
            {
                var existing = _expenses.FirstOrDefault(e => e.ExpenseId == expense.ExpenseId);
                if (existing == null)
                    throw new ArgumentException($"Expense with Id={expense.ExpenseId} not found");
                existing.CategoryId = expense.CategoryId;
                existing.Description = expense.Description;
                existing.Amount = expense.Amount;
                existing.ExpenseDate = expense.ExpenseDate;
            }

            void IExpenseService.Delete(string id)
            {
                _expenses.RemoveAll(e => e.ExpenseId == id);
            }
        }
    }


