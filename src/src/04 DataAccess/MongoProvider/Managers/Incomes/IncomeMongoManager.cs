using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyDiary.MongoProvider.Connection;
using MongoDB.Driver.Linq;
using MyDiary.Domain.Abstract.Repositories.Mongo;
using MyDiary.MongoProvider.MDO;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Domains;
using MyDiary.Common.Extensions;
using MongoDB.Driver.Builders;
namespace MyDiary.MongoProvider.Managers.Incomes
{
    public class IncomeMongoManager : IIncomeMongoRepository
    {
        #region PRIVATE CONSTANTS
        private const int NumberOfRowsPerPage = 10;//To Do ==>Take value from config
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Get All Incomes by using Mongo and LINQ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="incomeDate"></param>
        /// <param name="incomeTypes"></param>
        /// <returns></returns>
        public List<IIncome> GetAll(int userId, int currentPage,DateTime incomeDate , List<IIncomeType> incomeTypes =null)
        {
            try
            {
                int rowCount = (currentPage * NumberOfRowsPerPage);
                int skipPageCount = rowCount != 0 ? (rowCount - NumberOfRowsPerPage) : 0;               

                IQueryable<IncomeMDO> incomeFilteredCollection = this.GetIncomeQuery(userId, incomeDate, incomeTypes);

                List<IncomeMDO> incomesMDO = incomeFilteredCollection
                                              .Skip(skipPageCount)//if skipPageCount is negative , skip value cannot be negative exception will throw
                                              .Take(NumberOfRowsPerPage)
                                              .ToList();                 //-----> MongoDb Call to get the filtered incomes.

                List<IIncome> incomes = this.MapIncomeMDOCollectionToDomainList(incomesMDO);
                incomes.ForEach(a => a.TotalCount = incomeFilteredCollection.Count());

                return incomes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
         
        /// <summary>
        /// Get all incomes by using MongoQuery
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="incomeDate"></param>
        /// <param name="incomeTypes"></param>
        /// <returns></returns>
        public List<IIncome> GetAllByMongoQuery(int userId, int currentPage, DateTime incomeDate, List<IIncomeType> incomeTypes = null)
        {

            MongoDB.Driver.IMongoQuery incomeMongoQuery = this.GetIncomeMongoQuery(userId, incomeDate, incomeTypes);
            int totalIncomeCount = new MongoDBManager().GetCountByQuery<IncomeMDO>("Incomes", incomeMongoQuery); //-----> MongoDb call to get the count.

            if (totalIncomeCount < ((currentPage - 1) * NumberOfRowsPerPage))
            {
                currentPage = 1;
            }
            MongoCursor<IncomeMDO> incomeMongoCursor = this.GetIncomeMongoCursor(incomeMongoQuery, currentPage);
            List<IIncome> incomes = this.MapIncomeMDOCollectionToDomainList(incomeMongoCursor.ToList());//-----> MongoDb Call to get the filtered incomes.
            incomes.ForEach(a => a.TotalCount = totalIncomeCount);
           

            return incomes;

        }

        /// <summary>
        /// Add an income Document
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public bool Add(IIncome income)
        {
            try
            {
                new MongoDBManager().Add<IncomeMDO>("Incomes", this.MapIncomeDomainToDTO(income));
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
        /// <param name="incomes"></param>
        /// <returns></returns>
        public bool Add(List<IIncome> incomes)
        {
            try
            {
                new MongoDBManager().Add<IncomeMDO>("Incomes", this.MapIncomeDomainListToMDOCollection(incomes));
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }

        }


        public void Update(IIncome income)
        {

            MongoDB.Driver.IMongoQuery incomeUpdateConditionQuery = Query.And(MongoDB.Driver.Builders.Query<IncomeMDO>.Where(e => e.UserId == income.UserId),
                                                                               MongoDB.Driver.Builders.Query<IncomeMDO>.Where(e => e.IncomeId == income.IncomeId));
            var updateStatements = MongoDB.Driver.Builders.Update
                                   .Set("IncomeType.TypeId", income.IncomeType.TypeId)
                                   .Set("IncomeType.Type", income.IncomeType.Type)
                                   .Set("Amount", income.Amount)
                                   .Set("Description", income.Description)
                                   .Set("ModifiedBy", income.UserId)
                                   .Set("IncomeDate", income.IncomeDate)
                                   .Set("Comments", (income.Comments == null) ? string.Empty : income.Comments);

            new MongoDBManager().Update<IncomeMDO>("Incomes", incomeUpdateConditionQuery, updateStatements);

        }


        /// <summary>
        /// Remove all documents from income collection.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllDocuments()
        {
            try
            {
                new MongoDBManager().RemoveAll<IncomeMDO>("Incomes");
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }
        }

        /// <summary>
        /// Remove all documents of user from income collection. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveDocumentsByUserId(int userId)
        {
            try
            {
                MongoDB.Driver.IMongoQuery removeByUserIdQuery = MongoDB.Driver.Builders.Query<Income>.EQ(e => e.UserId, userId);
                new MongoDBManager().Remove<IncomeMDO>("Incomes", removeByUserIdQuery);
                return true;
            }
            catch (WriteConcernException )
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomeId"></param>
        public void RemoveDocumentByIncomeId(int incomeId)
        {
            MongoDB.Driver.IMongoQuery removeByIncomeIdQuery = MongoDB.Driver.Builders.Query<IncomeMDO>.EQ(e => e.IncomeId, incomeId);
            new MongoDBManager().Remove<IncomeMDO>("Incomes", removeByIncomeIdQuery);
        }
      
        #endregion

        #region PRIVATE METHODS

        private MongoDB.Driver.IMongoQuery GetIncomeMongoQuery(int userId, DateTime incomeDate, List<IIncomeType> incomeTypes = null)
        {
            MongoDB.Driver.IMongoQuery incomeMongoQuery = MongoDB.Driver.Builders.Query<IncomeMDO>.Where(i => i.UserId == userId); //----> default query by userId

            if ((!incomeDate.IsDefault()))
            {
                incomeMongoQuery = Query.And(incomeMongoQuery, MongoDB.Driver.Builders.Query<IncomeMDO>.Where(i => i.IncomeDate == incomeDate));
                if (incomeTypes != null && incomeTypes.Any())
                {
                    List<int> incomeTypeIds = incomeTypes.Select(t => t.TypeId).ToList<int>();
                    incomeMongoQuery = Query.And(incomeMongoQuery, MongoDB.Driver.Builders.Query<IncomeMDO>.Where(x => x.IncomeType.TypeId.In(incomeTypeIds)));
                }

            }
            else if (incomeTypes != null && incomeTypes.Any())
            {
                List<int> incomeTypeIds = incomeTypes.Select(t => t.TypeId).ToList<int>();
                incomeMongoQuery = Query.And(incomeMongoQuery, MongoDB.Driver.Builders.Query<IncomeMDO>.Where(x => x.IncomeType.TypeId.In(incomeTypeIds)));
            }

            return incomeMongoQuery;
        }
    
        private IQueryable<IncomeMDO> GetIncomeQuery(int userId, DateTime incomeDate, List<IIncomeType> incomeTypes = null)
        {
            IQueryable<IncomeMDO> incomeCollection = new MongoDBManager().GetAll<IncomeMDO>("Incomes");
            IQueryable<IncomeMDO> incomeFilteredCollection = (from i in incomeCollection.Where(x => x.UserId == userId)
                                                              //orderby i.RowNumber ascending
                                                              select i);
            if (!incomeDate.IsDefault())
            {
                incomeFilteredCollection = incomeFilteredCollection.Where<IncomeMDO>(x => x.IncomeDate == incomeDate);
                if ((incomeTypes != null && incomeTypes.Any()))
                {
                    List<int> incomeTypeIds = incomeTypes.Select(t => t.TypeId).ToList<int>();
                    incomeFilteredCollection = incomeFilteredCollection.Where<IncomeMDO>(x => x.IncomeType.TypeId.In(incomeTypeIds));
                }
            }
            else if ((incomeTypes != null && incomeTypes.Any()))
            {
                List<int> incomeTypeIds = incomeTypes.Select(t => t.TypeId).ToList<int>();
                incomeFilteredCollection = incomeFilteredCollection.Where<IncomeMDO>(x => x.IncomeType.TypeId.In(incomeTypeIds));
            }
            return incomeFilteredCollection;
        }

        private MongoCursor<IncomeMDO> GetIncomeMongoCursor(MongoDB.Driver.IMongoQuery incomeMongoQuery, int currentPage)
        {
            int rowCount = (currentPage * NumberOfRowsPerPage);
            int skipPageCount = rowCount != 0 ? (rowCount - NumberOfRowsPerPage) : 0;
            MongoCursor<IncomeMDO> incomeMongoCursor = new MongoDBManager().Find<IncomeMDO>("Incomes", incomeMongoQuery);
            incomeMongoCursor.Skip = skipPageCount; //------> Setting the skip count
            incomeMongoCursor.Limit = NumberOfRowsPerPage; //-----> Setting the take count (Limit) 
            incomeMongoCursor.SetSortOrder(SortBy.Descending("IncomeDate", "CreatedDate"));  //Setting the sort order before executing

            return incomeMongoCursor;
        }

        #region MAPPING METHODS

        private List<IIncome> MapIncomeMDOCollectionToDomainList(List<IncomeMDO> incomeMDOList)
        {
            return (from i in incomeMDOList select MapIncomeMDOToDomain(i)).ToList();
        }

        private IIncome MapIncomeMDOToDomain(IncomeMDO incomeMDO)
        {
            return new Income()
            {
               // RowNumber =incomeMDO.RowNumber,
                IncomeId =incomeMDO.IncomeId,
                UserId =incomeMDO.UserId,
                IncomeType =  new IncomeType()
                {
                    Type = incomeMDO.IncomeType.Type,
                    TypeId = incomeMDO.IncomeType.TypeId
                },
                Description = incomeMDO.Description,
                Comments =incomeMDO.Comments,
                IncomeDate = DateTime.Parse(incomeMDO.IncomeDate.ToShortDateString()),
                CreatedBy = incomeMDO.CreatedBy,
                ModifiedBy =incomeMDO.ModifiedBy,
                ModifiedDate =incomeMDO.ModifiedDate,
                Amount = incomeMDO.Amount              
               
            };
        }

        private List<IncomeMDO> MapIncomeDomainListToMDOCollection(List<IIncome> incomeList)
        {
            return (from i in incomeList select MapIncomeDomainToDTO(i)).ToList();
        }

        private IncomeMDO MapIncomeDomainToDTO(IIncome income)
        {
            return new IncomeMDO()
            {
              //  RowNumber =income.RowNumber,
                IncomeId =income.IncomeId,
                UserId =income.UserId,
                IncomeType = new IncomeTypeMDO()
                {
                    Type = income.IncomeType.Type,
                    TypeId = income.IncomeType.TypeId
                },
                Description = income.Description,
                Comments =income.Comments,
                IncomeDate = DateTime.Parse(income.IncomeDate.ToShortDateString()),
                CreatedBy = income.CreatedBy,
                ModifiedBy =income.ModifiedBy,
                ModifiedDate =income.ModifiedDate,
                Amount = income.Amount 
            };
        }

        #endregion

        #endregion
    }

}
