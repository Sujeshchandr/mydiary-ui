using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class ExpenseDetailsViewModel
    {
       public List<ExpenseViewModel> expenses { get; set; }
       public float TotalAmount { get; set; }
       public string Title { get; set; }
    }
}