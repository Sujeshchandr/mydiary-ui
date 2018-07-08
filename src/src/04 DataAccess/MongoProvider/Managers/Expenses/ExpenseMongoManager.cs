using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Domains;
using MyDiary.MongoProvider.MDO.Expense;
using MyDiary.Common.Extensions;
using MongoDB.Driver.Linq;
using MyDiary.Domain.Abstract.Repositories.Mongo;

namespace MyDiary.MongoProvider.Managers.Expenses
{
    public class ExpenseMongoManager : IExpenseMongoRepository
    {        
        #region PRIVATE CONSTANTS

        private const int NumberOfRowsPerPage = 10;//To Do ==>Take value from config
        private const string EXPENSES = "Expenses";

        #endregion
        
        #region PUBLIC METHODS

        /// <summary>
        /// Get all expenses by using MongoQuery
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="expenseDate"></param>
        /// <param name="expenseTypes"></param>
        /// <returns></returns>
        public IList<IExpense> GetAll(int userId, int currentPage, DateTime expenseDate, string expenseMonth, IList<IExpenseType> expenseTypes = null)
        {

            MongoDB.Driver.IMongoQuery expenseMongoQuery = this.GetExpenseMongoQuery(userId, expenseDate, expenseMonth,expenseTypes);
            int totalExpenseCount = new MongoDBManager().GetCountByQuery<ExpenseMDO>(EXPENSES, expenseMongoQuery); //-----> MongoDb call to get the count.

            if (totalExpenseCount < ((currentPage - 1) * NumberOfRowsPerPage))
            {
                currentPage = 1;
            }
            MongoCursor<ExpenseMDO> expenseMongoCursor = this.GetExpenseMongoCursor(expenseMongoQuery, currentPage);
            List<IExpense> expenses = this.MapExpenseMDOCollectionToDomainList(expenseMongoCursor.ToList());//-----> MongoDb Call to get the filtered incomes.
            expenses.ForEach(a => a.TotalCount = totalExpenseCount);

            return expenses;

        }

        /// <summary>
        /// Get All Expenses from Mongo using LINQ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="incomeDate"></param>
        /// <param name="expenseTypes"></param>
        /// <returns></returns>
        [Obsolete("GetAllByLinq is depreciated, please use GetAll instead.")]
        public IList<IExpense> GetAllByLinq(int userId, int currentPage,DateTime incomeDate , IList<IExpenseType> expenseTypes =null)
        {
            try
            {
                int rowCount = (currentPage * NumberOfRowsPerPage);
                int skipPageCount = rowCount != 0 ? (rowCount - NumberOfRowsPerPage) : 0;

                IQueryable<ExpenseMDO> expenseFilteredCollection = this.GetExpenseQuery(userId, incomeDate, expenseTypes);

                List<ExpenseMDO> expensesMDO = expenseFilteredCollection
                                              .Skip(skipPageCount)//if skipPageCount is negative , skip value cannot be negative exception will throw
                                              .Take(NumberOfRowsPerPage)
                                              .ToList();                 //-----> MongoDb Call to get the filtered incomes.

                List<IExpense> expenses = this.MapExpenseMDOCollectionToDomainList(expensesMDO);
                expenses.ForEach(a => a.TotalCount = expenseFilteredCollection.Count());

                return expenses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Add an expense Document
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public bool Add(IExpense expense)
        {
            try
            {
                new MongoDBManager().Add<ExpenseMDO>(EXPENSES, this.MapExpenseDomainToDTO(expense));
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }                
            
          
        }

        /// <summary>
        /// Add a list of income documents
        /// </summary>
        /// <param name="expenses"></param>
        /// <returns></returns>
        public bool Add(IList<IExpense> expenses)
        {
            try
            {
                new MongoDBManager().Add<ExpenseMDO>(EXPENSES, this.MapExpenseDomainListToMDOCollection(expenses));
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }

        }

        public void Update(IExpense expense)
        {

            MongoDB.Driver.IMongoQuery expenseUpdateConditionQuery = Query.And(MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(e => e.UserId == expense.UserId),
                                                                               MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(e => e.ExpenseId == expense.Id));
            var updateStatements = MongoDB.Driver.Builders.Update
                                   .Set("ExpenseType.TypeId", expense.ExpenseType.TypeId)
                                   .Set("ExpenseType.Type", expense.ExpenseType.Type)
                                   .Set("Amount", expense.Amount)
                                   .Set("Description", expense.Description)
                                   .Set("ModifiedBy", expense.UserId)
                                   .Set("ExpenseDate", expense.ExpenseDate)
                                   .Set("Comments", (expense.Comments == null) ? string.Empty : expense.Comments);

            new MongoDBManager().Update<ExpenseMDO>(EXPENSES, expenseUpdateConditionQuery, updateStatements);

        }

        /// <summary>
        /// Remove all documents from expense collection.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllDocuments()
        {
            try
            {
                new MongoDBManager().RemoveAll<ExpenseMDO>(EXPENSES);
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }
        }

        /// <summary>
        /// Remove all documents of user from expense collection. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveDocumentsByUserId(int userId)
        {
            try
            {
                MongoDB.Driver.IMongoQuery removeByUserIdQuery = MongoDB.Driver.Builders.Query<IExpense>.EQ(e => e.UserId, userId);
                new MongoDBManager().Remove<ExpenseMDO>(EXPENSES, removeByUserIdQuery);
                return true;
            }
            catch (WriteConcernException)
            {
                return false;
            }
        }

        public void RemoveDocumentByExpenseId(int expenseId)
        {
            MongoDB.Driver.IMongoQuery removeByExpenseIdQuery = MongoDB.Driver.Builders.Query<ExpenseMDO>.EQ(e => e.ExpenseId, expenseId);
            new MongoDBManager().Remove<ExpenseMDO>(EXPENSES, removeByExpenseIdQuery);
        }
      
        #endregion

        #region PRIVATE METHODS

        private MongoDB.Driver.IMongoQuery GetExpenseMongoQuery(int userId, DateTime expenseDate, string expenseMonth ,IList<IExpenseType> expenseTypes = null)
        {
            MongoDB.Driver.IMongoQuery expenseMongoQuery = MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(i => i.UserId == userId); //----> default query by userId



            if (!string.IsNullOrEmpty(expenseMonth))
            {
                expenseMongoQuery = Query.And(expenseMongoQuery, MongoDB.Driver.Builders.Query<ExpenseMDO>
                    .EQ(i => i.ExpenseDate.Month , 12));
                if (expenseTypes != null && expenseTypes.Any())
                {
                    List<int> expenseTypeIds = expenseTypes.Select(t => t.TypeId).ToList<int>();
                    expenseMongoQuery = Query.And(expenseMongoQuery, MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(x => x.ExpenseType.TypeId.In(expenseTypeIds)));
                }
            }
            else if ((!expenseDate.IsDefault()))
            {
                expenseMongoQuery = Query.And(expenseMongoQuery, MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(i => i.ExpenseDate == expenseDate));
                if (expenseTypes != null && expenseTypes.Any())
                {
                    List<int> expenseTypeIds = expenseTypes.Select(t => t.TypeId).ToList<int>();
                    expenseMongoQuery = Query.And(expenseMongoQuery, MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(x => x.ExpenseType.TypeId.In(expenseTypeIds)));
                }

            }
            else if (expenseTypes != null && expenseTypes.Any())
            {
                List<int> expenseTypeIds = expenseTypes.Select(t => t.TypeId).ToList<int>();
                expenseMongoQuery = Query.And(expenseMongoQuery, MongoDB.Driver.Builders.Query<ExpenseMDO>.Where(x => x.ExpenseType.TypeId.In(expenseTypeIds)));
            }

            return expenseMongoQuery;
        }

        private IQueryable<ExpenseMDO> GetExpenseQuery(int userId, DateTime expenseDate, IList<IExpenseType> expenseTypes = null)
        {
            IQueryable<ExpenseMDO> expenseCollection = new MongoDBManager().GetAll<ExpenseMDO>(EXPENSES);
            IQueryable<ExpenseMDO> expenseFilteredCollection = (from i in expenseCollection.Where(x => x.UserId == userId) select i);

            if (!expenseDate.IsDefault())
            {
                expenseFilteredCollection = expenseFilteredCollection.Where<ExpenseMDO>(x => x.ExpenseDate == expenseDate);
                if ((expenseTypes != null && expenseTypes.Any()))
                {
                    List<int> expenseTypeIds = expenseTypes.Select(t => t.TypeId).ToList<int>();
                    expenseFilteredCollection = expenseFilteredCollection.Where<ExpenseMDO>(x => x.ExpenseType.TypeId.In(expenseTypeIds));
                }
            }
            else if ((expenseTypes != null && expenseTypes.Any()))
            {
                List<int> expenseTypeIds = expenseTypes.Select(t => t.TypeId).ToList<int>();
                expenseFilteredCollection = expenseFilteredCollection.Where<ExpenseMDO>(x => x.ExpenseType.TypeId.In(expenseTypeIds));
            }
            return expenseFilteredCollection.OrderByDescending(x => x.ExpenseDate); // Order by desc by expense date (not considering Modified Date) 
        }

        private MongoCursor<ExpenseMDO> GetExpenseMongoCursor(MongoDB.Driver.IMongoQuery expenseMongoQuery, int currentPage)
        {
            int rowCount = (currentPage * NumberOfRowsPerPage);
            int skipPageCount = rowCount != 0 ? (rowCount - NumberOfRowsPerPage) : 0;
            MongoCursor<ExpenseMDO> expenseMongoCursor = new MongoDBManager().Find<ExpenseMDO>(EXPENSES, expenseMongoQuery);
            expenseMongoCursor.Skip = skipPageCount; //------> Setting the skip count
            expenseMongoCursor.Limit = NumberOfRowsPerPage; //-----> Setting the take count (Limit) 
            expenseMongoCursor.SetSortOrder(SortBy.Descending("ExpenseDate", "CreatedDate"));  //Setting the sort order before executing

            return expenseMongoCursor;
        }

        #region MAPPING METHODS

        private List<IExpense> MapExpenseMDOCollectionToDomainList(List<ExpenseMDO> expenseMDOList)
        {
            return (from i in expenseMDOList select MapExpenseMDOToDomain(i)).ToList();
        }

        private IExpense MapExpenseMDOToDomain(ExpenseMDO expenseMDO)
        {
            return new Expense()
            {

                Id = expenseMDO.ExpenseId,
                UserId =expenseMDO.UserId,
                ExpenseType = new ExpenseType()
                {
                    Type = expenseMDO.ExpenseType.Type,
                    TypeId = expenseMDO.ExpenseType.TypeId,
                },
                Description = expenseMDO.Description,
                Comments =expenseMDO.Comments,
                ExpenseDate = DateTime.Parse(expenseMDO.ExpenseDate.ToShortDateString()),
                CreatedBy = expenseMDO.CreatedBy,
                ModifiedBy =expenseMDO.ModifiedBy,
                ModifiedDate =expenseMDO.ModifiedDate,
                Amount = expenseMDO.Amount              
               
            };
        }

        private IList<ExpenseMDO> MapExpenseDomainListToMDOCollection(IList<IExpense> expenseList)
        {
            return (from i in expenseList select MapExpenseDomainToDTO(i)).ToList();
        }

        private ExpenseMDO MapExpenseDomainToDTO(IExpense expense)
        {
            return new ExpenseMDO()
            {
                ExpenseId =expense.Id,
                UserId =expense.UserId,
                ExpenseType = new ExpenseTypeMDO()
                {
                    Type = expense.ExpenseType.Type,
                    TypeId = expense.ExpenseType.TypeId
                },
                Amount = expense.Amount,
                Description = expense.Description,
                Comments =expense.Comments,
                ExpenseDate = DateTime.Parse(expense.ExpenseDate.ToShortDateString()),
                CreatedBy = expense.CreatedBy,
                CreatedDate =expense.CreatedDate,
                ModifiedBy =expense.ModifiedBy,
                ModifiedDate = expense.ModifiedDate == null ? DateTime.MinValue : expense.ModifiedDate ,
            };
        }

        #endregion


        #endregion
    }
}
