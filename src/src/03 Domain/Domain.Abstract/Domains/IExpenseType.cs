using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyDiary.Domain.Abstract.Domains
{

    public interface IExpenseType
    {
         int TypeId { get; set; }
         string Type { get; set; }
         int UserId { get; set; }
         IList<IExpenseType> GetAllExpenseTypes(int userId);
         bool AddExpenseType(string expenseType,int userId);
         IExpenseType Create(int TypeId, string Type, int userId);
    }
}