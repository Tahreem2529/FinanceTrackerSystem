using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(string id);
         void Add(Category category);
        void Update(Category category);
         void Delete(string id);
       
    }
}
