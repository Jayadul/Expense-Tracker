using Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExpenseCategoryManager
{
    public interface IExpenseCategoryService
    {
        Task<int> AddAsync(VmExpenseCategory dto, int userId);
        Task<int> UpdateAsync(VmExpenseCategory dto, int userId);
        Task<List<VmExpenseCategory>> GetBetween(int start, int displayLength, string searchValue, out int totalLength);
        ExpenseCategory GetById(int id);
        IEnumerable<ExpenseCategory> GetAll();
        bool IsUnique(string ExpenseCategoryName);
        bool HasChild(int id);
        bool Delete(int id);
    }
}
