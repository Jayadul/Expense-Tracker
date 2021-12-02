using Entities.Domains;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Expense> ExpenseRepo { get; }
        IRepository<ExpenseCategory> ExpenseCategoryRepo { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
