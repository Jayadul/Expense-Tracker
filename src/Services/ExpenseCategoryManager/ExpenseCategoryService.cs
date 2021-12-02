using Entities.Domains;
using Repositories.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExpenseCategoryManager
{
    public class ExpenseCategoryService: IExpenseCategoryService
    {
        private IUnitOfWork _unitOfWork;
        public ExpenseCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(VmExpenseCategory dto, int userId)
        {
            try
            {
                var newrecord = new ExpenseCategory
                {
                    Id = 0,
                    Name = dto.Name,
                    CreatedBy = userId,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.ExpenseCategoryRepo.Add(newrecord);
                await _unitOfWork.SaveChangesAsync();
                return newrecord.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<int> UpdateAsync(VmExpenseCategory dto, int userId)
        {
            try
            {
                var data = _unitOfWork.ExpenseCategoryRepo.Get(dto.Id);
                if (data != null)
                {
                    data.Name = dto.Name;
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
        public Task<List<VmExpenseCategory>> GetBetween(int start, int displayLength, string searchValue, out int totalLength)
        {
            var context = _unitOfWork.ExpenseCategoryRepo.GetAllQueryable();

            totalLength = context.Count();
            displayLength = totalLength;
            if (displayLength == -1)
            {
                displayLength = totalLength;
            }

            var result = context.AsEnumerable().Skip(start).Take(displayLength).Select(s => new VmExpenseCategory
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return Task.FromResult(result);
        }
        public ExpenseCategory GetById(int id)
        {
            return _unitOfWork.ExpenseCategoryRepo.Get(id);
        }
        public IEnumerable<ExpenseCategory> GetAll()
        {
            return _unitOfWork.ExpenseCategoryRepo.GetAllEnumerable();
        }
        public bool IsUnique(string ExpenseCategoryName)
        {
            if (_unitOfWork.ExpenseCategoryRepo.Any(o => o.Name == ExpenseCategoryName))
                return true;
            else
                return false;
        }
        public bool HasChild(int id)
        {
            if (_unitOfWork.ExpenseRepo.Any(o => o.ExpenseCategoryId == id))
                return true;
            else
               return false;
        }
             
        public bool Delete(int id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                _unitOfWork.ExpenseCategoryRepo.Remove(obj);
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
