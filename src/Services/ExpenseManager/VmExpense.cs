using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExpenseManager
{
    public class VmExpense
    {
        public int Id { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public string Date { get; set; }
        public decimal Ammount { get; set; }
    }
}
