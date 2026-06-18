using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Contracts;
using App.Core.Models;

namespace App.Core.Services
{
        public class InMemoryBudgetService : IBudgetService
        {
            private List<Budget> _budgets;

            public InMemoryBudgetService()
            {
                _budgets = new List<Budget>();
            }

            List<Budget> IBudgetService.GetAll()
            {
                return _budgets.ToList();
            }

            Budget IBudgetService.GetById(string id)
            {
                return _budgets.FirstOrDefault(b => b.BudgetId == id);
            }

            void IBudgetService.Add(Budget budget)
            {
                if (budget == null)
                    throw new ArgumentNullException("Budget is null");
                _budgets.Add(budget);
            }

            void IBudgetService.Update(Budget budget)
            {
                var existing = _budgets.FirstOrDefault(b => b.BudgetId == budget.BudgetId);
                if (existing == null)
                    throw new ArgumentException($"Budget with Id={budget.BudgetId} not found");
                existing.CategoryId = budget.CategoryId;
                existing.BudgetAmount = budget.BudgetAmount;
                existing.BudgetAmount = budget.BudgetAmount;
                existing.SpentAmount = budget.SpentAmount;
                existing.StartDate = budget.StartDate;
                existing.EndDate = budget.EndDate;
            }

            void IBudgetService.Delete(string id)
            {
                _budgets.RemoveAll(b => b.BudgetId == id);
            }


        }
    }








