using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyDiary.API.ControllerHelpers;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.Expenses;

namespace MyDiary.API.Controllers
{
    public class ExpenseMongoDBController  : ApiController
    {
        #region PRIVATE VARIABLES

        private IExpenseService _expenseService;

        #endregion

        #region CONSTRUCTOR

        public ExpenseMongoDBController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        #endregion

        #region PUBLIC METHODS

        [HttpPost]
        public bool InsertAll(List<ExpenseJSON> expenses)
        {
            if (expenses == null || !expenses.Any())
                throw new ArgumentNullException("Expenses cannot be null or empty");
           return _expenseService.InsertAll(new ExpenseControllerHelper().Map_ExpenseJSONList_To_ExpensesDTOList(expenses),false);
        }

        [HttpGet]
        public bool DeleteAll()
        {
            return _expenseService.DeleteAll(false);
        }

        #endregion

    }
}