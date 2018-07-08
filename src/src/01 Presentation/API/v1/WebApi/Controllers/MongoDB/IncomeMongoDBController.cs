using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDiary.API.ControllerHelpers;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.Incomes;

namespace MyDiary.API.Controllers
{
    public class IncomeMongoDBController : ApiController
    { 
        #region PRIVATE VARIABLES

        private IIncomeService _incomeService;

        #endregion

        #region CONSTRUCTOR

        public IncomeMongoDBController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        #endregion

        #region PUBLIC METHODS

        [HttpPost]
        public bool InsertAll(List<IncomeJSON> incomes)
        {
            if (incomes == null || !incomes.Any())
                throw new ArgumentNullException("Incomes cannot be null or empty");
           return _incomeService.InsertAll(new IncomeControllerHelper().Map_IncomeJSONList_To_IncomesDTOList(incomes),false);
        }

        [HttpGet]
        public bool DeleteAll()
        {
            return _incomeService.DeleteAll(false);
        }

        #endregion

    }
}