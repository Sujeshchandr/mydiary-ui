using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
   public class ExpenseType :IExpenseType
    {
       public int TypeId { get; set; }
       public string Type { get; set; }
       public int UserId { get; set; }
    }
}
