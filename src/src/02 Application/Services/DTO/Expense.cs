using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class Expense :IExpense
    {
        public int ExpenseId { get; set; }
        public IExpenseType Type { get; set; }
        public DateTime ExpenseDate { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public IPeople CurrentUser { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int TotalCount { get; set; }
        public int UserId { get; set; }
        public bool GetTestFunction()
        {
          return  false;
        }
    }
}
