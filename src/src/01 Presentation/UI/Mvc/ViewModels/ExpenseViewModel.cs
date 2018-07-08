using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class ExpenseViewModel
    {
        public int ExpenseId { get; set; }
        public ExpenseTypeViewModel ExpenseType { get; set; }
        public string ExpenseDate { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

        public List<Error> Errors = new List<Error>();
    }


    public class Error 
    {
        public string Date{get;set;}
        public string Description {get;set;}
    }
}