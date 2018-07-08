using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.UI.ViewModels.Paging;

namespace MyDiary.UI.ControllerHelpers
{
    public static class PagingHelpers
    {
        public static PagingViewModel MapPagingViewModel(int maximumNoOfPages, int currentPage, int totalItems)
        {
            return new PagingViewModel()
            {

                MaximumNoOfPages = maximumNoOfPages,
                CurrentPage = currentPage,
                TotalItems = totalItems
            };
           
        }  
    }
}