using Entities.Domains;
using Repositories.UnitOfWorks;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExpenseManager
{
    public class ExpenseService: IExpenseService
    {
        private IUnitOfWork _unitOfWork;
        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(VmExpense dto, int userId)
        {
            try
            {
                var newrecord = new Expense
                {
                    Id = 0,
                    Date = DateTime.Parse(dto.Date),
                    ExpenseCategoryId = dto.ExpenseCategoryId,
                    Ammount = dto.Ammount,
                    CreatedBy = userId,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.ExpenseRepo.Add(newrecord);
                await _unitOfWork.SaveChangesAsync();
                return newrecord.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<int> UpdateAsync(VmExpense dto, int userId)
        {
            try
            {
                var data = _unitOfWork.ExpenseRepo.Get(dto.Id);
                if (data != null)
                {
                    data.Date = DateTime.Parse(dto.Date);
                    data.ExpenseCategoryId = dto.ExpenseCategoryId;
                    data.Ammount = dto.Ammount;
                    data.LastUpdatedBy = userId;
                    data.LastUpdatedDate = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                    return data.Id;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public Task<List<VmExpense>> GetBetween(int start, int displayLength, string searchValue, out int totalLength)
        {
            var context = _unitOfWork.ExpenseRepo.GetAllQueryable();
            var categoryContext = _unitOfWork.ExpenseCategoryRepo.GetAllQueryable();

            totalLength = context.Count();
            displayLength = totalLength;
            if (displayLength == -1)
            {
                displayLength = totalLength;
            }

            var result = context.AsEnumerable().Skip(start).Take(displayLength).Select(s => new VmExpense
            {
                Id = s.Id,
                Date = s.Date.ToString("dd MMM yyyy"),
                ExpenseCategoryName= categoryContext.FirstOrDefault(o=>o.Id==s.ExpenseCategoryId).Name,
                Ammount=s.Ammount
            }).ToList();
            return Task.FromResult(result);
        }

        public IEnumerable<VmExpenseCategorySelectList> GetAllCategories()
        {
            var ExpenseCategoryList = _unitOfWork.ExpenseCategoryRepo.GetAll().Select(s => new VmExpenseCategorySelectList { Id = s.Id, Name = s.Name });
            return ExpenseCategoryList;
        }

        public Expense GetById(int id)
        {
            return _unitOfWork.ExpenseRepo.Get(id);
        }
        public IEnumerable<Expense> GetAll()
        {
            return _unitOfWork.ExpenseRepo.GetAllEnumerable();
        }
        public bool Delete(int id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                _unitOfWork.ExpenseRepo.Remove(obj);
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
