using MyDiary.Domain.Domain.Abstracts;
using System;
using System.Collections.Generic;

namespace MyDiary.Domain.Abstract.Domains
{
    public interface IExpense
     {
         #region PROPERTIES

         int Id { get; set; }
         IExpenseType ExpenseType { get; set; }
         DateTime ExpenseDate { get; set; }
         float Amount { get; set; }
         string Description { get; set; }
         string Comments { get; set; }
         float TotalCount { get; set; }
         int CreatedBy { get; set; }
         int? ModifiedBy { get; set; }
         DateTime? ModifiedDate { get; set; }
         int UserId { get; set; }
         DateTime CreatedDate { get; set; }
         IPeople CurrentUser { get; set; }

         #endregion
          
         #region METHODS

         bool Add(IExpense expense);
         bool Add(IList<IExpense> expenses, bool toSQLServer);
         void Update(IExpense expense);
         IExpenseDetails GetAll(int userId);
         IList<IExpense> Get(int userId, int currentPage, bool fromSQLServer, IList<IExpenseType> filteredExpenseTypes,string expenseMonth);
         IList<IChart> GetYearlyAmountSummary(int userId, string noOfLatestYears, bool fromSQLServer);
         IList<IChart> GetMonthlyAmountSummary(int userId, string year, bool fromSQLServer = false); //it will returns all expenses for the specified year(month by month)
         IList<IChart> GetWeeklyAmountSummary(int userId, string month, string year,bool fromSQLServer = false); ////it will returns all expenses for the specified month and year(week by week)
         IList<IChart> GetDailyAmountSummary(int userId, string date, bool fromSQLServer = false); ////it will returns all expenses for the specified date
         bool DeleteAll(bool toSQLServer);
         void DeleteById();

       #endregion

     }
}