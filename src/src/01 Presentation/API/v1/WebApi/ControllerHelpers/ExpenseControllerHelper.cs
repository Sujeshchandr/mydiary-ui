using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.DTO;

namespace MyDiary.API.ControllerHelpers
{
    public class ExpenseControllerHelper
    {
        #region PUBLIC METHODS

        public List<ExpenseJSON> Map_ExpensesDTOList_To_ExpenseJSONList(IList<IExpense> expenseDTOList)
        {
            return (from i in expenseDTOList select Map_ExpenseDTO_To_JSON(i)).ToList();
        }

        public List<IExpense> Map_ExpenseJSONList_To_ExpensesDTOList(List<ExpenseJSON> expensesJSON)
        {
            return (from i in expensesJSON select Map_ExpenseJSON_To_DTO(i)).ToList();
        }

        #endregion

        #region PRIVATE METHODS

        private ExpenseJSON Map_ExpenseDTO_To_JSON(IExpense i)
        {
            return new ExpenseJSON()
            {
                //RowNumber = i.RowNumber,
                ExpenseId = i.ExpenseId,
                ExpenseType = Map_ExpenseTypeDTO_To_JSON(i.Type),
                UserId = i.UserId,
                Amount = i.Amount,
                Description = i.Description,
                ExpenseDate = i.ExpenseDate,
                CreatedBy = i.CreatedBy,
                CreatedDate =i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,
                Comments = i.Comments
            };
        }

        private IExpense Map_ExpenseJSON_To_DTO(ExpenseJSON e)
        {
            return new Expense()
            {
                //RowNumber = e.RowNumber,
                ExpenseId = e.ExpenseId,
                Type = this.Map_ExpenseTypeJSON_To_DTO(e.ExpenseType),
                UserId = e.UserId,
                Amount = e.Amount,
                Description = e.Description,
                ExpenseDate = e.ExpenseDate,
                CreatedBy = e.CreatedBy,
                ModifiedBy = e.ModifiedBy,
                Comments = e.Comments,
                ModifiedDate = e.ModifiedDate,
                CurrentUser = new People() { UserId = e.UserId }
            };
        }

        private ExpenseTypeJSON Map_ExpenseTypeDTO_To_JSON(IExpenseType expenseTypeDTO)
        {
            return new ExpenseTypeJSON()
            {
                Type = expenseTypeDTO.Type,
                TypeId = expenseTypeDTO.TypeId
            };
        }

        private IExpenseType Map_ExpenseTypeJSON_To_DTO(ExpenseTypeJSON expenseTypeJSON)
        {
            return new ExpenseType()
            {
                Type = expenseTypeJSON.Type,
                TypeId = expenseTypeJSON.TypeId,
            };
        }

        #endregion
    }
}