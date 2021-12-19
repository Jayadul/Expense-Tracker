using Entities.Domains;
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
        IEnumerable<VmExpenseCategorySelectList> GetAllCategories();
        Task<List<VmExpense>> GetBetween(string FromDate, string ToDate,int start, int displayLength, string searchValue, out int totalLength);
        Expense GetById(int id);
        IEnumerable<Expense> GetAll();
        bool Delete(int id);
    }
}
