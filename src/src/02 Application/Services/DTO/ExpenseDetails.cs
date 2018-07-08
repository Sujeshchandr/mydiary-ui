using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class ExpenseDetails: IExpenseDetails
    {
        public ExpenseDetails()
        {

        }

       public IList<IExpense> ExpenseList { get; set; }
       public float TotalAmount { get; set; }

    }
}
