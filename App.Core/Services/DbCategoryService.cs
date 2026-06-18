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
    public class DbCategoryService : ICategoryService
    {
        private string _connectionString;

        public DbCategoryService(string connectionString)
        {
            _connectionString = connectionString;
        }
        List<Category> ICategoryService.GetAll()
        {
            var list = new List<Category>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT *FROM Categories", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Category
                    {
                        CategoryId = reader["CategoryId"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                       
                    });
                }
            }
            return list;
        }

        Category ICategoryService.GetById(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT * FROM Categories WHERE CategoryId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Category
                    {
                        CategoryId = reader["CategoryId"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                    };
                }
            }
            return null;
        }
        void ICategoryService.Add(Category category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Categories (CategoryId, CategoryName, Description) " +
                    "VALUES (@id, @name, @desc)", conn);
               
                cmd.Parameters.AddWithValue("@id", category.CategoryId);
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                cmd.Parameters.AddWithValue("@desc", category.Description);
                
                cmd.ExecuteNonQuery();
            }
        }

        void ICategoryService.Update(Category  category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Categories SET CategoryName=@name, " +
                    "Description=@desc,  WHERE CategoryId=@id", conn);
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                cmd.Parameters.AddWithValue("@desc", category.Description);
                cmd.Parameters.AddWithValue("@id", category.CategoryId);
                cmd.ExecuteNonQuery();
            }
        }

        void ICategoryService.Delete(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Categories WHERE CategoryId=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
       
    



