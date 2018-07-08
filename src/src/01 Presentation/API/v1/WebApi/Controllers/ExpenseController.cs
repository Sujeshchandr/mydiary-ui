using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MyDiary.API.ControllerHelpers;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.Expenses;
using NLog;
using NLog.Fluent;
using System.Globalization;

namespace MyDiary.API.Controllers 
{
    public class ExpenseController : ApiController
    {
        #region PRIVATE VARIABLES

        private readonly IExpenseService _expenseService;

        private readonly ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public ExpenseController(IExpenseService expenseService, ILogger logger)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException("expenseService");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _expenseService = expenseService;
            _logger = logger;

        }

        #endregion

        #region PUBLIC METHODS

        // GET api/<controller>/5
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        public HttpResponseMessage Get(int userId)
        {
            try
            {
                List<ExpenseJSON> expenseJsonList = new ExpenseControllerHelper().Map_ExpensesDTOList_To_ExpenseJSONList(_expenseService.GetAll(userId, null, 0, true));
                return this.Request.CreateResponse(HttpStatusCode.OK, expenseJsonList);
            }
            catch (Exception ex)
            {
                ex.Data["methodName"] = "ExpenseController.Get"; // ToDo --> Need to test value duplicates!!!!

                _logger.Error()
                       .Message(CultureInfo.InvariantCulture, "Error getting expense by userId")
                       .Exception(ex)
                       .Property("userId", userId)
                       .Write();

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        #endregion
    }
}