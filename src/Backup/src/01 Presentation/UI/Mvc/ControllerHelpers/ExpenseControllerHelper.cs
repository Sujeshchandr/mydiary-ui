using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.DTO;
using MyDiary.UI.ViewModels;
using MyDiary.UI.ViewModels.Expense;

namespace MyDiary.UI.ControllerHelpers
{
    public class ExpenseControllerHelper
    {
        #region CONSTANTS

        private const int MAXNUMBEROFPAGES = 5;

        #endregion

        #region MAPPING METHOD FOR EXPENSE TYPES

        public List<ExpenseTypeViewModel> MapExpenseTypesDTOListToViewModelList(List<IExpenseType> expenseTypesDTOList)
        {
            return (from i in expenseTypesDTOList select MapExpenseTypeDTOToViewModel(i)).ToList();
        }

        public ExpenseTypeViewModel MapExpenseTypeDTOToViewModel(IExpenseType expenseTypesDTO)
        {
            return new ExpenseTypeViewModel()
            {
                TypeId = expenseTypesDTO.TypeId,
                Type = expenseTypesDTO.Type
            };
        }

        public IExpenseType MapExpenseTypeViewModelToDTO(ExpenseTypeViewModel expenseTypeViewModel)
        {
            return new ExpenseType()
            {
                TypeId = expenseTypeViewModel.TypeId,
                Type = expenseTypeViewModel.Type,
                UserId = expenseTypeViewModel.UserId
            };
        }

        #endregion

        #region MAPPING METHOD FOR EXPENSES

        public ExpenseSummaryViewModel MapExpenseDTOListToExpenseSummaryViewModel(IList<IExpense> expenseDTOList, int currentPage)
        {
            if (expenseDTOList == null || !expenseDTOList.Any()) return new ExpenseSummaryViewModel();

            ExpenseSummaryViewModel expenseSummaryViewModel = new ExpenseSummaryViewModel()
            {
                ExpenseViewModels = MapExpensesDTOListToViewModelList(expenseDTOList),
                TotalAmount = this.GetTotalExpenseAmount(expenseDTOList),
                PagingViewModel = PagingHelpers.MapPagingViewModel(MAXNUMBEROFPAGES, currentPage, expenseDTOList[0].TotalCount)
            };

            return expenseSummaryViewModel;
        }

        public List<ExpenseViewModel> MapExpensesDTOListToViewModelList(IList<IExpense> expenseDTOList)
        {
            return (from i in expenseDTOList select MapExpenseDTOToViewModel(i)).ToList();
        }

        public IExpense MapExpenseViewModelToDTO(ExpenseViewModel expenseViewModel)
        {
            return new Expense()
            {
                ExpenseId = expenseViewModel.ExpenseId,
                UserId = expenseViewModel.UserId,
                Type = this.MapExpenseTypeViewModelToDTO(expenseViewModel.ExpenseType),
                Amount = expenseViewModel.Amount,
                Comments = expenseViewModel.Comments,
                Description = expenseViewModel.Description,
                CreatedBy = expenseViewModel.CreatedBy,
                ExpenseDate = DateTime.Parse(expenseViewModel.ExpenseDate),
                ModifiedBy = expenseViewModel.ModifiedBy

            };
        }

        public ExpenseViewModel MapExpenseDTOToViewModel(IExpense expenseDTO)
        {
            return new ExpenseViewModel()
            {
                ExpenseId = expenseDTO.ExpenseId,
                UserId = expenseDTO.UserId,
                ExpenseType = this.MapExpenseTypeDTOToViewModel(expenseDTO.Type),
                Amount = expenseDTO.Amount,
                Comments = expenseDTO.Comments,
                Description = expenseDTO.Description,
                CreatedBy = expenseDTO.CreatedBy,
                ExpenseDate = expenseDTO.ExpenseDate.ToString(MyDiary.Common.Constants.DateFormats.ddMMYYYY),
                ModifiedBy = expenseDTO.ModifiedBy
            };
        }

        public IExpenseFilter MapExpenseFilterViewModelToExpenseFilter(ExpenseFilterViewModel expenseFilterViewModel)
        {
            if (expenseFilterViewModel.ExpenseDate == null &&  expenseFilterViewModel.ExpenseTypes == null  )
            {
                return new MyDiary.Application.Services.DTO.ExpenseFilter() { ExpenseTypes = new List<int>() };

            }
            else
            {
                return new MyDiary.Application.Services.DTO.ExpenseFilter()
                {
                   // ExpenseTypes = this.MapStringArrayToIntegerList(expenseFilterViewModel.ExpenseTypes),
                    ExpenseTypes = expenseFilterViewModel.ExpenseTypes,
                    ExpenseDate = this.GetFormattedDate(expenseFilterViewModel.ExpenseDate),
                    ExpenseMonth = expenseFilterViewModel.ExpenseMonth
                };
            }
        }

        public ExpenseDetailsViewModel MapExpenseDetailsDTOToViewModel(Application.Services.Abstract.DTO.IExpenseDetails expenseDetailsDTO)
        {
            if (expenseDetailsDTO == null) throw new ArgumentNullException("ExpenseDetailsDTO cannot be null");
            if (expenseDetailsDTO.ExpenseList == null) throw new ArgumentNullException("ExpenseDetailsDTO-->ExpenseList cannot be null");
            //ExpenseDetailsViewModel 
            List<ExpenseViewModel> expenseViewModelList = new List<ExpenseViewModel>();

            if (expenseDetailsDTO.ExpenseList.Count == 0)
                return new ExpenseDetailsViewModel
                {
                    Title = "Expense Summary",
                    TotalAmount = 0,
                    expenses = null
                };

            foreach (var expenseDTO in expenseDetailsDTO.ExpenseList)
            {
                if (expenseDTO.Type == null)
                    throw new ArgumentNullException("ExpenseDTO ==> Type cannot be null");
                expenseViewModelList.Add(new ExpenseViewModel
                {

                    ExpenseType = new ExpenseTypeViewModel
                    {
                        TypeId = expenseDTO.Type.TypeId,
                        Type = expenseDTO.Type.Type
                    },
                    Amount = expenseDTO.Amount,
                    ExpenseDate = expenseDTO.ExpenseDate.ToString(),
                    Description = expenseDTO.Description.Length > 35 ? expenseDTO.Description.Substring(0, 34) + "..." : expenseDTO.Description,
                    Comments = expenseDTO.Comments,

                });
            }
            ExpenseDetailsViewModel expenseDetails = new ExpenseDetailsViewModel()
            {
                expenses = expenseViewModelList,
                Title = "Expense Summary",
                TotalAmount = expenseDetailsDTO.TotalAmount
            };

            return expenseDetails;

        }

        public List<ExpenseViewModel> MapExpenseDTOToExpenseViewModel(List<Application.Services.Abstract.DTO.IExpense> expenseDTOList)
        {
            if (expenseDTOList == null)
                throw new ArgumentNullException("ExpenseDTOList cannot be null");

            List<ExpenseViewModel> expenseViewModelList = new List<ExpenseViewModel>();
            if (expenseDTOList.Count == 0) return expenseViewModelList;

            foreach (var expenseDTO in expenseDTOList)
            {
                if (expenseDTO.Type == null)
                    throw new ArgumentNullException("ExpenseDTO ==> Type cannot be null");
                expenseViewModelList.Add(new ExpenseViewModel
                {

                    ExpenseType = new ExpenseTypeViewModel
                    {
                        TypeId = expenseDTO.Type.TypeId,
                        Type = expenseDTO.Type.Type
                    },
                    Amount = expenseDTO.Amount,
                    ExpenseDate = expenseDTO.ExpenseDate.ToString(),
                    Description = expenseDTO.Description,
                    Comments = expenseDTO.Comments
                });
            }


            return expenseViewModelList;
        }

        public Application.Services.Abstract.DTO.IExpense MapExpenseViewModelToExpenseDTO(ExpenseViewModel expenseViewModel)
        {
            if (expenseViewModel == null)
                throw new ArgumentNullException("ExpenseViewModel cannot be null");
            if (expenseViewModel.ExpenseType == null)
                throw new ArgumentNullException("ExpenseViewModel ==> Type cannot be null");
            return new Application.Services.DTO.Expense
            {
                ExpenseId = expenseViewModel.ExpenseId,
                Type = new Application.Services.DTO.ExpenseType
                {
                    TypeId = expenseViewModel.ExpenseType.TypeId,
                    Type = expenseViewModel.ExpenseType.Type
                },
                Amount = expenseViewModel.Amount,
                ExpenseDate = Convert.ToDateTime(expenseViewModel.ExpenseDate),
                Description = expenseViewModel.Description,
                Comments = expenseViewModel.Comments,
                CurrentUser = new Application.Services.DTO.People
                {
                    UserId = expenseViewModel.UserId
                },
                ModifiedBy =  expenseViewModel.ModifiedBy
            };

        }

        #endregion

        #region PRIVATE METHODS


        private float GetTotalExpenseAmount(IList<IExpense> expenses)
        {
            return expenses.Sum(x => x.Amount);
        }

        private List<int> MapStringArrayToIntegerList(string[] stringArray)
        {
            List<int> integerList = new List<int>();
            if (stringArray == null) return integerList;
            foreach (var s in stringArray)
            {
                if (!string.IsNullOrEmpty(s))
                    integerList.Add(int.Parse(s.ToString()));
            }
            return integerList;
        }

        private DateTime GetFormattedDate(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime)) return DateTime.MinValue;
            try
            {
                return DateTime.Parse(dateTime);
            }
            catch (FormatException)
            {
                // NOTE ::   DateTime.Parse throw exception when passing input like "14/02/2015" 
                //as FormatException ::String was not recognized as a valid DateTime.
                return DateTime.ParseExact(dateTime, MyDiary.Common.Constants.DateFormats.ddMMYYYY, CultureInfo.InvariantCulture);

            }
        }
        #endregion
    }
}