using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels.Paging
{
    public class PagingViewModel
    {
        public int MaximumNoOfPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
    }
}