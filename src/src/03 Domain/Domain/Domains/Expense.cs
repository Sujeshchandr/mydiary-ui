using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.Mongo;
using MyDiary.Domain.Domain.Abstracts;

namespace MyDiary.Domain.Domains
{
    public class Expense :IExpense
    {
       
        #region PUBLIC PROPERTIES
		     
        public int Id { get; set; }
        public IExpenseType ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public IPeople CurrentUser { get; set; }
        public float TotalCount { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

	    #endregion

        #region PRIVATE INSTANCE FIELDS

        private IExpenseRepository _expenseRepository;
        private IExpenseMongoRepository _expenseMongoRepository;

        #endregion

        #region CONSTRUCTOR

        public Expense() { }
        public Expense(IExpenseRepository expenseRepository, IExpenseMongoRepository expenseMongoRepository)
        {
            _expenseRepository = expenseRepository;
            _expenseMongoRepository = expenseMongoRepository;
        } 

        #endregion

        #region PUBLIC METHODS

        public bool Add(IExpense expense)
        {
            expense.Id =_expenseRepository.Add(expense);
            expense.CreatedBy = expense.UserId; //ToDo==>Add these Parameters in Sp
            expense.CreatedDate = DateTime.Now;
            return _expenseMongoRepository.Add(expense);

        }

        public bool Add(IList<IExpense> expenses, bool toSQLServer)
        {
            if (toSQLServer)
            {
                throw new NotImplementedException("InsertAll to SQL Server is not implemented");

            }
            else //Insert All expenses to MongoDB
            {
                if (this.DeleteAll(toSQLServer)) //Deleting all entries from mongoDB before initializing
                {
                    return _expenseMongoRepository.Add(expenses);
                }
                return false;


            }
        }

        public void Update(IExpense expense)
        {
            if (expense.Id <= 0)
            {
                throw new ArgumentException("Expense id should be greater than zero");
            }
             _expenseRepository.Update(expense);
             _expenseMongoRepository.Update(expense);
        }

        public IExpenseDetails GetAll(int userId)
        {
            return GetExpenseDetails(_expenseRepository.Get(userId));
        }
       
        public IList<IExpense> Get(int userId, int currentPage, bool fromSqlServer, IList<IExpenseType> filteredExpenseTypes,string expenseMonth)
        {
            if (fromSqlServer)
            {
                return _expenseRepository.Get(userId);

            }
            else //From MongoDB
            {
               // return _expenseMongoRepository.GetAllByLinq(userId, currentPage, this.ExpenseDate, filteredExpenseTypes);

                return _expenseMongoRepository.GetAll(userId, currentPage, this.ExpenseDate, expenseMonth, filteredExpenseTypes);

            }

        }

        public void DeleteById()
        {
            if (this.Id <= 0)
            {
                throw new ArgumentException("Expense id should be greater than zero");
            }
            _expenseRepository.Delete(this.Id);
            _expenseMongoRepository.RemoveDocumentByExpenseId(this.Id);
            
        }

        public bool DeleteAll(bool fromSqlServer)
        {
            if (fromSqlServer)
            {
                throw new NotImplementedException("Delete all from sql server is not implemented");
            }
            else //Delete All expenses from MongoDB
            {
                return _expenseMongoRepository.RemoveAllDocuments();
            }
        }

        public IList<IChart> GetYearlyAmountSummary(int userId, string noOfLatestYears, bool fromSqlServer = false)
        {
            if (fromSqlServer)
            {
                throw new NotImplementedException();
            }
            else 
            {
                throw new NotImplementedException();
            }
        }

        public IList<IChart> GetMonthlyAmountSummary(int userId, string year, bool fromSqlServer = false)
        {
            if (fromSqlServer)
            {
                throw new NotImplementedException();
            }
            else 
            {
                return new List<IChart>()
                {
                    new Chart { SeqNumber = 1, Amount = 2222},
                    new Chart { SeqNumber = 2, Amount = 3333},
                    new Chart { SeqNumber = 3, Amount = 4433},
                    new Chart { SeqNumber = 4, Amount = 6456},
                    new Chart { SeqNumber = 5, Amount = 465},
                    new Chart { SeqNumber = 6, Amount = 4464},
                    new Chart { SeqNumber = 7, Amount = 646},
                    new Chart { SeqNumber = 8, Amount = 4644},
                    new Chart { SeqNumber = 9, Amount = 465},
                    new Chart { SeqNumber = 10, Amount = 44864},
                    new Chart { SeqNumber = 11, Amount = 646},
                    new Chart { SeqNumber = 12, Amount = 444}
                };
            }
        }

        public IList<IChart> GetWeeklyAmountSummary(int userId, string month, string year, bool fromSqlServer = false)
        {
            if (fromSqlServer)
            {
                throw new NotImplementedException();

            }
            else 
            {
                throw new NotImplementedException();
            }
        }

        public IList<IChart> GetDailyAmountSummary(int userId, string date, bool fromSqlServer = false)
        {
            if (fromSqlServer)
            {
                throw new NotImplementedException();

            }
            else
            {
                throw new NotImplementedException();

            }
        }

        #endregion

        #region PRIVATE METHODS

        private IExpenseDetails GetExpenseDetails(IList<IExpense> expenseList)
        {
            if (expenseList == null) throw new ArgumentNullException("expenseList");

            IExpenseDetails expenseDetails = new ExpenseDetails();
            float totalAmount = expenseList.Sum(x => x.Amount);
            expenseDetails.TotalAmount = totalAmount;
            expenseDetails.ExpenseList = new List<IExpense>();            

            foreach (Expense expense in expenseList)
            {
                if(expense == null)throw new ArgumentNullException("expense");

                expenseDetails.ExpenseList.Add(expense);

            }

            return expenseDetails;
        }
       
        #endregion

    }
}