using Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExpenseManager
{
    public interface IExpenseService
    {
        Task<int> AddAsync(VmExpense dto, int userId);
        Task<int> UpdateAsync(VmExpense dto, int userId);
        Task<List<VmExpense>> GetBetween(int start, int displayLength, string searchValue, out int totalLength);
        Expense GetById(int id);
        IEnumerable<Expense> GetAll();
        bool Delete(int id);
    }
}
