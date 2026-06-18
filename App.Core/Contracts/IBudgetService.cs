using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models;

namespace App.Core.Contracts
{
    public interface IBudgetService
    {
        List<Budget> GetAll();
        Budget GetById(string id);
        void Add(Budget budget);
        void Update(Budget budget);
         void Delete(string id);

    }
}
