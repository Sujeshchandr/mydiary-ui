using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.Abstract.Expenses
{
    public interface IExpenseService
    {
        bool Add(MyDiary.Application.Services.Abstract.DTO.IExpense expenseDTO);
        void Update(MyDiary.Application.Services.Abstract.DTO.IExpense expenseDTO);
        Abstract.DTO.IExpenseDetails GetAllExpenses(int userId);
        IList<Abstract.DTO.IExpense> GetAll(int userId, IExpenseFilter filters, int currentPage, bool fromSQLServer);
        bool InsertAll(IList<IExpense> expenses, bool toSQLServer);
        bool DeleteAll(bool toSQLServer);
        void DeleteById(int expenseId);

        #region EXPENSE TYPE

        bool AddExpenseType(string expenseType, int userId);
        IList<MyDiary.Domain.Abstract.Domains.IExpenseType> GetAllExpenseTypes(int userId);

        #endregion

    }
}
