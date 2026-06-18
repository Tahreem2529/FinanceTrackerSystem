using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace App.Core.Services
{
    public class DbBudgetService : IBudgetService
    {
        private string _connectionString;

        public DbBudgetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        List<Budget> IBudgetService.GetAll()
        {
            var list = new List<Budget>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Budgets", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Budget
                    {
                        BudgetId = reader["BudgetId"].ToString(),
                        CategoryId = reader["CategoryId"].ToString(),
                        BudgetAmount = Convert.ToDecimal(reader["BudgetAmount"]),
                        SpentAmount = Convert.ToDecimal(reader["SpentAmount"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"])
                    });
                }
            }
            return list;
        }

        Budget IBudgetService.GetById(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT * FROM Budgets WHERE BudgetId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Budget
                    {
                        BudgetId = reader["BudgetId"].ToString(),
                        CategoryId = reader["CategoryId"].ToString(),
                        BudgetAmount = Convert.ToDecimal(reader["BudgetAmount"]),
                        SpentAmount = Convert.ToDecimal(reader["SpentAmount"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"])
                    };
                }
            }
            return null;
        }

        void IBudgetService.Add(Budget budget)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Budgets (BudgetId, CategoryId, BudgetAmount, SpentAmount, StartDate, EndDate) " +
                    "VALUES (@id, @catId, @budgetAmount, @spentAmount, @startDate, @endDate)", conn);
                cmd.Parameters.AddWithValue("@id", budget.BudgetId);
                cmd.Parameters.AddWithValue("@catId", budget.CategoryId);
                cmd.Parameters.AddWithValue("@budgetAmount", budget.BudgetAmount);
                cmd.Parameters.AddWithValue("@spentAmount", budget.SpentAmount);
                cmd.Parameters.AddWithValue("@startDate", budget.StartDate);
                cmd.Parameters.AddWithValue("@endDate", budget.EndDate);
                cmd.ExecuteNonQuery();
            }
        }

        void IBudgetService.Update(Budget budget)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Budgets SET CategoryId=@catId, BudgetAmount=@budgetAmount, " +
                    "SpentAmount=@spentAmount, StartDate=@startDate, EndDate=@endDate " +
                    "WHERE BudgetId=@id", conn);
                cmd.Parameters.AddWithValue("@catId", budget.CategoryId);
                cmd.Parameters.AddWithValue("@budgetAmount", budget.BudgetAmount);
                cmd.Parameters.AddWithValue("@spentAmount", budget.SpentAmount);
                cmd.Parameters.AddWithValue("@startDate", budget.StartDate);
                cmd.Parameters.AddWithValue("@endDate", budget.EndDate);
                cmd.Parameters.AddWithValue("@id", budget.BudgetId);
                cmd.ExecuteNonQuery();
            }
        }

        void IBudgetService.Delete(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Budgets WHERE BudgetId=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
    

