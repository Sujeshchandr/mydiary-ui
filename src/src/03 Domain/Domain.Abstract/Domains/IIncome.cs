using MyDiary.Domain.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
    public interface IIncome
    {
       #region PROPERTIES

        // int RowNumber { get; set; } 
         int IncomeId { get; set; }
         int UserId { get; set; }
         IIncomeType IncomeType { get; set; }
        // List<IIncomeType> IncomeTypes { get; set; }
         float Amount { get; set; }
         int CreatedBy { get; set; }        
         int? ModifiedBy { get; set; }
         string Description { get; set; }
         string Comments { get; set; }
         DateTime IncomeDate { get; set; }
         DateTime CreatedDate { get; set; }
         DateTime? ModifiedDate { get; set; }
         int TotalCount { get; set; }
        
        #endregion

       #region METHODS

         int Add(IIncome income);
         void Edit(IIncome income);
         void DeleteById();
         List<IIncome> GetAll(int userId, int currentPage, bool fromSQLServer,List<IIncomeType> filteredIncomeTypes);
         IList<IChart> GetYearlyAmountSummary(int userId, string noOfLatestYears, bool fromSQLServer);
         IList<IChart> GetMonthlyAmountSummary(int userId, string year, bool fromSQLServer = false); //it will returns all expenses for the specified year(month by month)
         IList<IChart> GetWeeklyAmountSummary(int userId, string month, string year, bool fromSQLServer = false); ////it will returns all expenses for the specified month and year(week by week)
         IList<IChart> GetDailyAmountSummary(int userId, string date, bool fromSQLServer = false); ////it will returns all expenses for the specified date
         bool InsertAll(List<IIncome> incomes,bool toSQLServer);
         bool DeleteAll(bool toSQLServer);
         IIncome CreateIncome(int incomeId, int userId ,IIncomeType incomeType, float amount, DateTime incomeDate, int createdBy, int? modifiedBy, string description ,string comments);
       
       #endregion

    }
}
