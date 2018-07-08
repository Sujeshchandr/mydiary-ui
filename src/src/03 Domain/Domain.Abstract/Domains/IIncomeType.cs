using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
   public interface IIncomeType
   {
        #region PROPERTIES

        int TypeId { get; set; }
        string Type { get; set; }
        int UserId { get; set; }

       #endregion

        #region METHODS

        List<IIncomeType> GetAll(int userId);
        int Add(IIncomeType incomeType);
        IIncomeType Create(int TypeId, string Type, int userId);

        #endregion

   }
}
