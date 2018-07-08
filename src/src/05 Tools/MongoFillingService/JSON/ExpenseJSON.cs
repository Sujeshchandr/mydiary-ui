using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.MongoFillingService.JSON
{
   [Serializable]
   public class ExpenseJSON
   {
       public int RowNumber { get; set; }
       public int ExpenseId { get; set; }
       public int UserId { get; set; }
       public ExpenseTypeJSON ExpenseType { get; set; }
       public float Amount { get; set; }
       public DateTime ExpenseDate { get; set; }
       public DateTime CreatedDate { get; set; }
       public int CreatedBy { get; set; }
       public DateTime? ModifiedDate { get; set; }
       public int ModifiedBy { get; set; }
       public string Description { get; set; }
       public string Comments { get; set; }
       public int TotalCount { get; set; }
    }
}
