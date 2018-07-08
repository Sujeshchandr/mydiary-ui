using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.UI.ViewModels.Paging;

namespace MyDiary.UI.ViewModels.Income
{
    public class IncomeSummaryViewModel
    {
        public List<IncomeViewModel> IncomeViewModels { get; set; }        
        public float TotalAmount { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
    }
}