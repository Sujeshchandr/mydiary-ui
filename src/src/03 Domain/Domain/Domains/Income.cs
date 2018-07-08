using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MyDiary.Common.Comparer;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.Mongo;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Domain.Domain.Abstracts;

namespace MyDiary.Domain.Domains
{
    public class Income : IIncome
    {
        #region PRIVATE PROPERTIES
        IIncomeRepository _incomeRepository;
        IIncomeMongoRepository _incomeMongoRepository;
        #endregion

        #region CONSTRUCTOR

        public Income() { }
        public Income(IIncomeRepository incomeRepository, IIncomeMongoRepository incomeMongoRepository)
        {
            _incomeRepository = incomeRepository;
            _incomeMongoRepository = incomeMongoRepository;
        }

        #endregion

        #region PUBLIC PROPERTIES
        public int RowNumber { get; set; } 
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public IIncomeType IncomeType { get; set; }
        //public List<IIncomeType> IncomeTypes { get; set; }
        public float Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int TotalCount { get; set; }

        #endregion

        #region PUBLIC METHODS

        public int Add(IIncome income)
        {
           int incomeId = 0;

            //for (int i = 0; i < 2000; i++)
            //{
            //    incomeId = _incomeRepository.AddIncome(income);
            //}

            incomeId = _incomeRepository.AddIncome(income);
            if (incomeId >= 1)
            {
                _incomeMongoRepository.Add(income);
                //this.RefreshMongoDB(income.UserId);
            }
            return incomeId;
        }

        public void Edit(IIncome income)
        {
            if (income.IncomeId <= 0)
            {
                throw new ArgumentException("Income id should be greater than zero");
            }
            _incomeMongoRepository.Update(income);
            _incomeRepository.Update(income);
        }

        public List<IIncome> GetAll(int userId, int currentPage, bool fromSQLServer, List<IIncomeType> filteredIncomeTypes)
        {
            if (fromSQLServer)
            {
                return _incomeRepository.GetAll(userId);
               
            }
            else //From MongoDB
            {  
                //Initialize Timer

                //Stopwatch sw1 = new Stopwatch();
                //sw1.Start();
                //List<IIncome> incomeDocuments = _incomeMongoRepository.GetAll(userId, currentPage, this.IncomeDate, filteredIncomeTypes); //Fetching all collections from MongoDB
                //sw1.Stop();

                //var mongoLingValue = sw1.ElapsedTicks;

                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                return _incomeMongoRepository.GetAllByMongoQuery(userId, currentPage, this.IncomeDate, filteredIncomeTypes);
                //sw.Stop();

                //var queryValue = sw.ElapsedTicks;

            }

            
        }

        public bool InsertAll(List<IIncome> incomes, bool toSQLServer)
        {
            if (toSQLServer)
            {
                throw new NotImplementedException("InsertAll to SQL Server is not implemented");

            }
            else //Insert All incomes to MongoDB
            {
                if (this.DeleteAll(toSQLServer)) //Deleting all entries from mongoDB before initializing
                {
                    return _incomeMongoRepository.Add(incomes);
                }
                return false;
                    

            }
        }

        public bool DeleteAll(bool toSQLServer)
        {
            if (toSQLServer)
            {
                throw new NotImplementedException("DeleteAll to SQL Server is not implemented");

            }
            else //Delete All incomes from MongoDB
            {
                return _incomeMongoRepository.RemoveAllDocuments();

            }
        }

        public void DeleteById()
        {
            if (this.IncomeId <= 0)
            {
                throw new ArgumentException("Income id should be greater than zero");
            }
            _incomeRepository.Delete(this.IncomeId);
            _incomeMongoRepository.RemoveDocumentByIncomeId(this.IncomeId);

        }

        public IIncome CreateIncome(int incomeId, int userId ,IIncomeType incomeType, float amount, DateTime IncomeDate, int createdBy, int? modifiedBy,  string description,string comments)
        {
            return new Income()
            {
                IncomeId =incomeId,
                UserId =userId,
                IncomeType =incomeType,
                Amount =amount,
                IncomeDate = IncomeDate,
                CreatedBy = createdBy,
                ModifiedBy =modifiedBy,
                Description =description,
                Comments =comments
            };
        }

        public IList<IChart> GetYearlyAmountSummary(int userId, string noOfLatestYears, bool fromSQLServer)
        {
            if (fromSQLServer)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IList<IChart> GetMonthlyAmountSummary(int userId, string year, bool fromSQLServer = false)
        {
            if (fromSQLServer)
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
                    new Chart { SeqNumber = 6, Amount = 4464}
                };
            }
        }

        public IList<IChart> GetWeeklyAmountSummary(int userId, string month, string year, bool fromSQLServer = false)
        {
            if (fromSQLServer)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IList<IChart> GetDailyAmountSummary(int userId, string date, bool fromSQLServer = false)
        {
            if (fromSQLServer)
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

        private bool RefreshMongoDB(int userId)
        {
            List<IIncome> incomes = _incomeRepository.GetAll(userId); //Fetching from SQLDB
            bool isCompleted = false;
            using (TransactionScope scope = new TransactionScope())
            {
                this.RemoveIncomesFromMongoDB(userId);//if successfull added then remove all incomes from mongoDB  
                if (incomes.Any())
                {
                    //_incomeMongoRepository.Add(incomes.OrderBy(x => x.RowNumber).ToList());//Adding incomes to MongoDB
                    _incomeMongoRepository.Add(incomes.ToList());//Adding incomes to MongoDB
                }
                scope.Complete();
                isCompleted = true;
            }
            return isCompleted;
        }

        private bool RemoveIncomesFromMongoDB(int userId)
        {
            return _incomeMongoRepository.RemoveDocumentsByUserId(userId); //Removing all documents from MongoDB by userId 
        }

        private void Test_WCF_SOAP_ClientCalls()
        {
            FileAccessProvider.Providers.IFileAccess fileAccess = new FileAccessProvider.Providers.SharePointFileAccessProvider();
            int file1 = fileAccess.GetFile(1);
            int file2 = fileAccess.GetFile(1, 2);
            string files = fileAccess.GetFile("test");
            bool result = fileAccess.Upload(new byte[] { 5, 10, 15, 20 });
            result = fileAccess.Upload(new MemoryStream(new byte[] { 5, 10, 15, 20 }));

        }

        private void Test_WCF_REST_ClientCalls()
        {
            FileAccessProvider.Providers.IFileAccess fileAccess = new FileAccessProvider.Providers.SharePointRESTFileAccessProvider();
            int file1 = fileAccess.GetFile(1);
            //int file2 = fileAccess.GetFile(1, 2);
            //string files = fileAccess.GetFile("test");
            //bool result = fileAccess.Upload(new byte[] { 5, 10, 15, 20 });
            //result = fileAccess.Upload(new MemoryStream(new byte[] { 5, 10, 15, 20 }));

        }

        #endregion

    }
}
