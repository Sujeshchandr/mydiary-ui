using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class ExpenseFilter :IExpenseFilter
    {
        public List<int> ExpenseTypes { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseMonth { get; set; }
    }
}
