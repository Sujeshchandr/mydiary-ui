using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class IncomeViewModel
    {
        public int IncomeId { get; set; }
        public IncomeTypeViewModel IncomeType { get; set; }
        public float Amount { get; set; }
        public string IncomeDate { get; set; }
        public int UserId { get; set; }       
        public int CreatedBy { get; set; }      
        public int? ModifiedBy { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
    }
}