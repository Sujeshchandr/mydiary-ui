using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IExpense
    {
        int ExpenseId { get; set; }
        IExpenseType Type { get; set; }
        DateTime ExpenseDate { get; set; }
        float Amount { get; set; }
        string Description { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string Comments { get; set; }
        DateTime? ModifiedDate { get; set; }
        IPeople CurrentUser { get; set; }
        int? ModifiedBy { get; set; }
        int TotalCount { get; set; }
        int UserId { get; set; }
        bool GetTestFunction();
    }
}
