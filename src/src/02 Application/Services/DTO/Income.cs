using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
   public class Income :IIncome
    {
       #region PUBLIC PROPERTIES
        public int RowNumber { get; set; }
        public int IncomeId { get; set; }
        public int UserId { get; set; }
       // public List<IIncomeType> IncomeType { get; set; }
        public IIncomeType IncomeType { get; set; }
        public float Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public int CreatedBy { get; set; }      
        public int? ModifiedBy { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int TotalCount { get; set; }
        #endregion

    }
}
