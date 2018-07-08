using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.SQL
{
    public interface IExpenseRepository
    {
       #region EXPENSE

        int Add(IExpense expenseDomain);
        void Update(IExpense expenseDomain);
        IList<IExpense> Get(int userId);
        void Delete(int expenseId);

       #endregion

       #region EXPENSE TYPE
         
        bool AddExpenseType(string expenseType,int userId);
        IList<IExpenseType> GetAllExpenseTypes(int userId);
         
        #endregion
    }
}
