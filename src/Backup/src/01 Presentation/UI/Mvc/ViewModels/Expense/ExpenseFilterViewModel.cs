using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels.Expense
{
    public class ExpenseFilterViewModel
    {
        public List<int> ExpenseTypes { get; set; }
        public string ExpenseDate { get; set; }
        public string ExpenseMonth { get; set; }

    }
}