using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
   public class IncomeFilter : IIncomeFilter
   {
      public List<int> IncomeTypes { get; set; }
      public DateTime IncomeDate { get; set; }
       
    }
}
