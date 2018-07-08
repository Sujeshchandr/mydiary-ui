using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
    public interface IExpenseDetails
    {
        IList<IExpense> ExpenseList { get; set; }
        float TotalAmount { get; set; }
    }
}
