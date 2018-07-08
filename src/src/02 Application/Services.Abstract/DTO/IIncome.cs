using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IIncome
    {
        int RowNumber { get; set; }
        int IncomeId { get; set; }
        int UserId { get; set; }
        //List<IIncomeType> IncomeType { get; set; }
        IIncomeType IncomeType { get; set; }
        float Amount { get; set; }
        DateTime IncomeDate { get; set; }
        int CreatedBy { get; set; }     
        int? ModifiedBy { get; set; }
        string Description { get; set; }
        string Comments { get; set; }
        DateTime? ModifiedDate { get; set; }
        int TotalCount { get; set; }
    }
}
