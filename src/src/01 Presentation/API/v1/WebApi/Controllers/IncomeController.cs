using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDiary.API.ControllerHelpers;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.Incomes;
using NLog;

namespace MyDiary.API.Controllers
{
    public class IncomeController : ApiController
    {
        #region PRIVATE VARIABLES

        private readonly IIncomeService _incomeService;

        private readonly ILogger _logger;

        #endregion

        #region CONSTRUCTOR
        public IncomeController(IIncomeService incomeService, ILogger logger)
        {
            if (incomeService == null)
            {
                throw new ArgumentNullException("incomeService");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _incomeService = incomeService;
            _logger = logger;

        }
       #endregion

        // GET api/<controller>/5
        public HttpResponseMessage Get(int userId)
        {
            try
            {
                List<IncomeJSON> incomeJsonList = new IncomeControllerHelper().Map_IncomesDTOList_To_IncomeJSONList(_incomeService.GetAll(userId, null,0, true));
                return this.Request.CreateResponse(HttpStatusCode.OK, incomeJsonList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                 return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #region TESTING METHODS

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public void TestPost(string userId, [FromUri]A test)
        {

        }
        [HttpHead]
        public string TestPost1()
        {
            return "LMK";
        }

        [HttpGet]
        public string TestPost1GET()
        {
            return "LMK";
        }

        #endregion

    }
    public class A
    {
       public string hello {get;set;}
    }
}