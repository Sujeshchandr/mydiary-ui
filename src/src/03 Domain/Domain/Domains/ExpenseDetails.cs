using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Domains
{
    public class ExpenseDetails :IExpenseDetails
    {
        public ExpenseDetails()
        {
        }
        public IList<IExpense> ExpenseList { get; set; }
        public float TotalAmount { get; set; }
    }
}
