using Entities.Domains;
using Repositories.Context;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;
        private EFRepository<Expense> expenseRepo;
        private EFRepository<ExpenseCategory> expenseCategoryRepo;

        public UnitOfWork(ProjectDbContext context)
        {
            _context = context;
        }
        public IRepository<Expense> ExpenseRepo
        {
            get
            {
                if (expenseRepo == null)
                {
                    expenseRepo = new EFRepository<Expense>(_context);
                }
                return expenseRepo;
            }
        }

        public IRepository<ExpenseCategory> ExpenseCategoryRepo
        {
            get
            {
                if (expenseCategoryRepo == null)
                {
                    expenseCategoryRepo = new EFRepository<ExpenseCategory>(_context);
                }
                return expenseCategoryRepo;
            }
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
