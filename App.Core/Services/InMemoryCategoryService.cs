using App.Core.Contracts;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class InMemoryCategoryService : ICategoryService
    {
        private List<Category> _categories;

        public InMemoryCategoryService()
        {
            _categories = new List<Category>();
        }

        List<Category> ICategoryService.GetAll()
        {
            return _categories.ToList();
        }

        Category ICategoryService.GetById(string id)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == id);
        }

        void ICategoryService.Add(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("Category is null");
            _categories.Add(category);
        }

        void ICategoryService.Update(Category category)
        {
            var existing = _categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existing == null)
                throw new ArgumentException($"Category with Id={category.CategoryId} not found");
            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;
           
        }

        void ICategoryService.Delete(string id)
        {
            _categories.RemoveAll(c => c.CategoryId == id);
        }

        
    }
}



    

