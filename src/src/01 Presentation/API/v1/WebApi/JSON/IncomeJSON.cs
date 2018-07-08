using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.API.JSON
{
    
    public class IncomeJSON
    {
        public int RowNumber { get; set; }
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public IncomeTypeJSON IncomeType { get; set; }
        public float Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
    }
}