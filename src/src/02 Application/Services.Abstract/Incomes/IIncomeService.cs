using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;



namespace MyDiary.Application.Services.Abstract.Incomes
{
   public interface IIncomeService
    {
        int Add(MyDiary.Application.Services.Abstract.DTO.IIncome incomeDTO);
        void Edit(Abstract.DTO.IIncome incomeDTO);
        List<IIncome> GetAll(int userId, IIncomeFilter filters, int currentPage, bool fromSQLServer);
        bool InsertAll(List<IIncome> incomes, bool toSQLServer);
        bool DeleteAll(bool toSQLServer);
        void DeleteById(int expenseId);
    }
}
