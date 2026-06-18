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
    public class DbExpenseService : IExpenseService
    {
        private string _connectionString;

        public DbExpenseService(string connectionString)
        {
            _connectionString = connectionString;
        }
        List<Expense> IExpenseService.GetAll()
        {
            var list = new List<Expense>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT *FROM Expenses", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Expense
                    {
                        ExpenseId = reader["ExpenseId"].ToString(),
                        CategoryId = reader["CategoryId"].ToString(),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"])
                    });
                }
            }
            return list;
        }

        Expense IExpenseService.GetById(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT * FROM Expenses WHERE ExpenseId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Expense
                    {
                        ExpenseId = reader["ExpenseId"].ToString(),
                        CategoryId = reader["CategoryId"].ToString(),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"])
                    };
                }
            }
            return null;
        }
        void IExpenseService.Add(Expense expense)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Expenses (ExpenseId, CategoryId, Description, Amount, ExpenseDate) " +
                    "VALUES (@id, @catId, @desc, @amount, @date)", conn);
                cmd.Parameters.AddWithValue("@id", expense.ExpenseId);
                cmd.Parameters.AddWithValue("@catId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@desc", expense.Description);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
                cmd.ExecuteNonQuery();
            }
        }

        void IExpenseService.Update(Expense expense)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Expenses SET CategoryId=@catId, Description=@desc, " +
                    "Amount=@amount, ExpenseDate=@date WHERE ExpenseId=@id", conn);
                cmd.Parameters.AddWithValue("@catId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@desc", expense.Description);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
                cmd.Parameters.AddWithValue("@id", expense.ExpenseId);
                cmd.ExecuteNonQuery();
            }
        }

        void IExpenseService.Delete(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Expenses WHERE ExpenseId=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
       
    

