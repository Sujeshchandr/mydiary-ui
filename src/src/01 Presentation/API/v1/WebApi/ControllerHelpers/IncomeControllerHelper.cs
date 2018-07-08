using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.DTO;

namespace MyDiary.API.ControllerHelpers
{
    public class IncomeControllerHelper
    {
        #region PUBLIC METHODS

        public List<IncomeJSON> Map_IncomesDTOList_To_IncomeJSONList(List<IIncome> incomeDTOList)
        {
            return (from i in incomeDTOList select Map_IncomeDTO_To_JSON(i)).ToList();
        }

        public List<IIncome> Map_IncomeJSONList_To_IncomesDTOList(List<IncomeJSON> incomesJSON)
        {
            return (from i in incomesJSON select Map_IncomeJSON_To_DTO(i)).ToList();
        } 
        #endregion

        #region PRIVATE METHODS

        private IncomeJSON Map_IncomeDTO_To_JSON(IIncome i)
        {
            return new IncomeJSON()
            {
                RowNumber = i.RowNumber,
                IncomeId = i.IncomeId,
                IncomeType = Map_IncomeTypeDTO_To_JSON(i.IncomeType),
                UserId = i.UserId,
                Amount = i.Amount,
                Description = i.Description,
                IncomeDate = i.IncomeDate,
                CreatedBy = i.CreatedBy,
                ModifiedBy = i.ModifiedBy,
                Comments = i.Comments,
                ModifiedDate = i.ModifiedDate
            };
        }

        private IIncome Map_IncomeJSON_To_DTO(IncomeJSON i)
        {
            return new Income()
            {
                IncomeId = i.IncomeId,
                IncomeType = this.Map_IncomeTypeJSON_To_DTO(i.IncomeType),
                UserId = i.UserId,
                Amount = i.Amount,
                Description = i.Description,
                IncomeDate = i.IncomeDate,
                CreatedBy = i.CreatedBy,
                ModifiedBy = i.ModifiedBy,
                Comments = i.Comments,
                ModifiedDate = i.ModifiedDate
            };
        } 

        private IncomeTypeJSON Map_IncomeTypeDTO_To_JSON(IIncomeType incomeTypeDTO)
        {
            return new IncomeTypeJSON()
            {
                Type = incomeTypeDTO.Type,
                TypeId = incomeTypeDTO.TypeId
            };
        }
        private IIncomeType Map_IncomeTypeJSON_To_DTO(IncomeTypeJSON incomeTypeJSON)
        {
            return new IncomeType()
            {
                Type = incomeTypeJSON.Type,
                TypeId = incomeTypeJSON.TypeId
            };
        }
        #endregion
    }
}