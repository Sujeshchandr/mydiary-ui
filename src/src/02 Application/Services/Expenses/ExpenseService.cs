using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.Abstract.Expenses;
using MyDiary.Application.Services.DTO;
using MyDiary.Common.Extensions;

namespace MyDiary.Application.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        #region PRIVATE PROPERTIES

        private MyDiary.Domain.Abstract.Domains.IExpenseType _expenseTypeDomain;
        private MyDiary.Domain.Abstract.Domains.IExpense _expenseDomain;

        #endregion

        #region CONSTRUCTORS

        public ExpenseService() { }

        public ExpenseService(MyDiary.Domain.Abstract.Domains.IExpenseType expenseTypeDomain, MyDiary.Domain.Abstract.Domains.IExpense expenseDomain)
        {
            _expenseTypeDomain = expenseTypeDomain;
            _expenseDomain = expenseDomain;
        }

        #endregion

        #region PUBLIC METHODS

        public MyDiary.Application.Services.Abstract.DTO.IExpenseDetails GetAllExpenses(int userId)
        {
            //var domain = _expenseDomain.GetTestFunction();
            //var testDomain = _expenseTestDomain.GetTestFunction();

            return MapExpenseDetialsDomainToDTO(_expenseDomain.GetAll(userId));
        }


        public bool Add(MyDiary.Application.Services.Abstract.DTO.IExpense expenseDTO)
        {
            return _expenseDomain.Add(MapExpenseDTOToExpenseDomain(expenseDTO));
        }

        public void Update(MyDiary.Application.Services.Abstract.DTO.IExpense expenseDTO)
        {
            _expenseDomain.Update(MapExpenseDTOToExpenseDomain(expenseDTO));
        }

        public IList<MyDiary.Application.Services.Abstract.DTO.IExpense> GetAll(int userId, IExpenseFilter filters, int currentPage, bool fromSQLServer)
        {
            IList<MyDiary.Domain.Abstract.Domains.IExpenseType> filteredExpenseTypes = new List<MyDiary.Domain.Abstract.Domains.IExpenseType>();

            var expenseMonth = string.Empty;

            if (filters != null)
            {
                if (filters.ExpenseTypes.Any())
                {
                    filteredExpenseTypes = this.Map_ExpenseTypeIds_To_ExpenseTypes(filters.ExpenseTypes);
                }
                if (!filters.ExpenseDate.IsDefault())
                {
                    _expenseDomain.ExpenseDate = filters.ExpenseDate;
                }
                else if(!string.IsNullOrEmpty(filters.ExpenseMonth))
                {
                    expenseMonth  = filters.ExpenseMonth;
                }
            }

            return MapExpenseDomainListToExpenseDTOList(_expenseDomain.Get(userId, currentPage, fromSQLServer, filteredExpenseTypes,expenseMonth));
        }

        public bool InsertAll(IList<Abstract.DTO.IExpense> expenses, bool toSQLServer)
        {
            return _expenseDomain.Add(this.MapExpenseDTOListToExpenseDomainList(expenses), toSQLServer);
        }

        public bool DeleteAll(bool fromSQLServer)
        {
            return _expenseDomain.DeleteAll(fromSQLServer);
        }

        public void DeleteById(int expenseId)
        {
            _expenseDomain.Id = expenseId;
            _expenseDomain.DeleteById();
        }

        #region EXPENSE TYPE

        public bool AddExpenseType(string expenseType, int userId)
        {
             return _expenseTypeDomain.AddExpenseType(expenseType, userId);
        }

        public IList<MyDiary.Domain.Abstract.Domains.IExpenseType> GetAllExpenseTypes(int userId)
        {
            return _expenseTypeDomain.GetAllExpenseTypes(userId);

        }

        #endregion

        #endregion

        #region MAPPING FUNCTIONS

        private IList<MyDiary.Domain.Abstract.Domains.IExpense> MapExpenseDTOListToExpenseDomainList(IList<MyDiary.Application.Services.Abstract.DTO.IExpense> expenseDTOList)
        {
            return (from i in expenseDTOList select MapExpenseDTOToExpenseDomain(i)).ToList();

        }
        private MyDiary.Domain.Abstract.Domains.IExpense MapExpenseDTOToExpenseDomain(MyDiary.Application.Services.Abstract.DTO.IExpense expenseDTO)
        {
            if (expenseDTO == null)
                throw new ArgumentNullException("ExpenseDTO cannot be null");
            if (expenseDTO.Type == null)
                throw new ArgumentNullException("ExpenseDTO ==>Type cannot be null");
            if (expenseDTO.CurrentUser == null)
                throw new ArgumentNullException("ExpenseDTO ==>CurrentUser cannot be null");

            return new MyDiary.Domain.Domains.Expense()
            {
                Id = expenseDTO.ExpenseId,
                ExpenseType = new MyDiary.Domain.Domains.ExpenseType()
                {
                    Type = expenseDTO.Type.Type,
                    UserId = expenseDTO.Type.UserId,
                    TypeId = expenseDTO.Type.TypeId
                },
                Amount = expenseDTO.Amount,
                ExpenseDate = expenseDTO.ExpenseDate,
                Description = expenseDTO.Description,
                Comments = expenseDTO.Comments,
                CurrentUser = new MyDiary.Domain.Domains.People()
                {
                    UserId = expenseDTO.CurrentUser.UserId
                },
                UserId = expenseDTO.CurrentUser.UserId


            };
        }

        private IExpenseDetails MapExpenseDetialsDomainToDTO(MyDiary.Domain.Abstract.Domains.IExpenseDetails expenseDetailsDomain)
        {
            if (expenseDetailsDomain == null) throw new ArgumentNullException("expenseDetailsDomain");
            if (expenseDetailsDomain.ExpenseList == null) throw new ArgumentNullException("expenseDetailsDomain-->ExpenseList");
            IExpenseDetails expenseDetails = new ExpenseDetails();
            expenseDetails.TotalAmount = expenseDetailsDomain.TotalAmount;
            expenseDetails.ExpenseList = MapExpenseDomainListToExpenseDTOList(expenseDetailsDomain.ExpenseList);
            return expenseDetails;

        }

        private IList<MyDiary.Application.Services.Abstract.DTO.IExpense> MapExpenseDomainListToExpenseDTOList(IList<MyDiary.Domain.Abstract.Domains.IExpense> expenseDomainList)
        {
            if (expenseDomainList == null)
                throw (new ArgumentNullException("ExpenseDomainList cannot be null"));

            IList<MyDiary.Application.Services.Abstract.DTO.IExpense> expenseDTOList = new List<IExpense>();

            if (expenseDomainList.Count == 0) return expenseDTOList;


            foreach (var expenseDomain in expenseDomainList)
            {
                if (expenseDomain == null)
                    throw (new ArgumentNullException("ExpenseDomain cannot be null"));
                if (expenseDomain.ExpenseType == null)
                    throw (new ArgumentNullException("ExpenseDomain ==>Type cannot be null"));
                expenseDTOList.Add(new MyDiary.Application.Services.DTO.Expense()
                                  {
                                      ExpenseId = expenseDomain.Id,
                                      Type = new MyDiary.Application.Services.DTO.ExpenseType
                                      {
                                          UserId = expenseDomain.ExpenseType.UserId,
                                          TypeId =expenseDomain.ExpenseType.TypeId,
                                          Type = expenseDomain.ExpenseType.Type
                                      },
                                      Amount = expenseDomain.Amount,
                                      ExpenseDate = expenseDomain.ExpenseDate,
                                      Description = expenseDomain.Description,
                                      Comments = expenseDomain.Comments,
                                      UserId = (expenseDomain.CurrentUser == null) ? expenseDomain.UserId : expenseDomain.CurrentUser.UserId,
                                      TotalCount = Convert.ToInt32( expenseDomain.TotalCount.ToString()),
                                      CreatedBy = expenseDomain.CreatedBy,
                                      CreatedDate = expenseDomain.CreatedDate,
                                      ModifiedBy = expenseDomain.ModifiedBy,
                                      ModifiedDate = expenseDomain.ModifiedDate,
                                      

                                  }
                );
            }

            return expenseDTOList;
        }

        private List<MyDiary.Domain.Abstract.Domains.IExpenseType> Map_ExpenseTypeIds_To_ExpenseTypes(List<int> expenseTypeIds)
        {
            List<MyDiary.Domain.Abstract.Domains.IExpenseType> incomeTypes = new List<MyDiary.Domain.Abstract.Domains.IExpenseType>();
            foreach (int incomeTypeId in expenseTypeIds)
            {
                incomeTypes.Add(_expenseTypeDomain.Create(incomeTypeId, string.Empty, 0));

            }
            return incomeTypes;
        }

        #endregion

    }
}
