using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domains
{
    public class Expense:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseCategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Ammount { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
