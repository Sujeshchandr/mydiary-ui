using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IExpenseFilter
    {
        List<int> ExpenseTypes { get; set; }
        DateTime ExpenseDate { get; set; }
        string ExpenseMonth { get; set; }
    }
}
