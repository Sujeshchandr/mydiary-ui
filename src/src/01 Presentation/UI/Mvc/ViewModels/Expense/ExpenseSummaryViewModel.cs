using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.UI.ViewModels.Paging;

namespace MyDiary.UI.ViewModels.Expense
{
    public class ExpenseSummaryViewModel
    {
        public List<ExpenseViewModel> ExpenseViewModels { get; set; }
        public float TotalAmount { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
    }
}