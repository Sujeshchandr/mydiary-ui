using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.DTO;
using MyDiary.UI.ViewModels;
using MyDiary.UI.ViewModels.Income;

namespace MyDiary.UI.ControllerHelpers
{
    public class IncomeControllerHelper
    {

        #region CONSTANTS

        private const int MAXNUMBEROFPAGES = 5;
        #endregion

        #region MAPPING METHOD FOR INCOMETYPES

        public List<IncomeTypeViewModel> MapIncomeTypesDTOListToViewModelList(List<IIncomeType> incomeTypesDTOList)
        {
            return (from i in incomeTypesDTOList select MapIncomeTypeDTOToViewModel(i)).ToList();
        }

        public IncomeTypeViewModel MapIncomeTypeDTOToViewModel(IIncomeType incomeTypesDTO)
        {
            return new IncomeTypeViewModel()
            {
                TypeId = incomeTypesDTO.TypeId,
                Type = incomeTypesDTO.Type
            };
        }

        public IIncomeType MapIncomeTypeViewModelToDTO(IncomeTypeViewModel incomeTypeViewModel)
        {
            return new IncomeType()
            {
                TypeId = incomeTypeViewModel.TypeId,
                Type = incomeTypeViewModel.Type,
                UserId = incomeTypeViewModel.UserId
            };
        }

        #endregion

        #region MAPPING METHOD FOR INCOMES

        public IncomeSummaryViewModel MapIncomeDTOListToIncomeSummaryViewModel(List<IIncome> incomeDTOList,int currentPage)
        {
            if (incomeDTOList == null || !incomeDTOList.Any()) return new IncomeSummaryViewModel();

            IncomeSummaryViewModel incomeSummaryViewModel = new IncomeSummaryViewModel()
            {
                IncomeViewModels = MapIncomesDTOListToViewModelList(incomeDTOList),
                TotalAmount = GetTotalIncomeAmount(incomeDTOList),
                PagingViewModel = PagingHelpers.MapPagingViewModel(MAXNUMBEROFPAGES, currentPage, incomeDTOList[0].TotalCount)
            };

            return incomeSummaryViewModel;
        }  

        public List<IncomeViewModel> MapIncomesDTOListToViewModelList(List<IIncome> incomeDTOList)
        {
            return (from i in incomeDTOList select MapIncomeDTOToViewModel(i)).ToList();
        }       

        public IIncome MapIncomeViewModelToDTO(IncomeViewModel incomeViewModel)
        {
            return new Income()
            {
               IncomeId = incomeViewModel.IncomeId,
               UserId = incomeViewModel.UserId,
               IncomeType =  this.MapIncomeTypeViewModelToDTO(incomeViewModel.IncomeType),
               Amount =incomeViewModel.Amount,
               Comments =incomeViewModel.Comments,
               Description =incomeViewModel.Description,
               CreatedBy =incomeViewModel.CreatedBy,
               IncomeDate =DateTime.Parse(incomeViewModel.IncomeDate),
               ModifiedBy =incomeViewModel.ModifiedBy             
               
            };
        }

        public IncomeViewModel MapIncomeDTOToViewModel(IIncome incomeDTO)
        {
            return new IncomeViewModel()
            {
                IncomeId = incomeDTO.IncomeId,
                UserId = incomeDTO.UserId,
                IncomeType = this.MapIncomeTypeDTOToViewModel(incomeDTO.IncomeType),
                Amount = incomeDTO.Amount,
                Comments = incomeDTO.Comments,
                Description = incomeDTO.Description,
                CreatedBy = incomeDTO.CreatedBy,
                IncomeDate = incomeDTO.IncomeDate.ToString(MyDiary.Common.Constants.DateFormats.ddMMYYYY),
                ModifiedBy = incomeDTO.ModifiedBy
            };
        }

        public IIncomeFilter MapIncomeFilterViewModelToIncomeFilter(IncomeFilterViewModel incomeFilterViewModel)
        {
            if (incomeFilterViewModel == null || (incomeFilterViewModel.IncomeDate == null && incomeFilterViewModel.IncomeTypes == null))
            {
                return new MyDiary.Application.Services.DTO.IncomeFilter() { IncomeTypes = new List<int>() };

            }
            else
            {
                return new MyDiary.Application.Services.DTO.IncomeFilter()
                {
                    IncomeTypes = incomeFilterViewModel.IncomeTypes,
                    IncomeDate = this.GetFormattedDate(incomeFilterViewModel.IncomeDate)                   
                };
            }
        }

        #endregion

        #region PRIVATE METHODS

        
        private float GetTotalIncomeAmount(List<IIncome> incomes)
        {
            return incomes.Sum(x => x.Amount);
        }

        private List<int> MapStringArrayToIntegerList(string[] stringArray)
        {
            List<int> integerList = new List<int>();
            if (stringArray == null) return integerList;
            foreach (var s in stringArray)
            {
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