using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDiary.Application.Services.Abstract.Expenses;

namespace MyDiary.API.Controllers
{
    public class TestController : ApiController
    {
              private IExpenseService _expenseService;
        public TestController(IExpenseService expenseService)
        {
             _expenseService = expenseService;
        }
        public bool GetBoolean(string name)
        {
            return true;

        }

        [NonAction]
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [NonAction]
        // GET api/<controller>/5
        public string Get(int id)
        {

            return "value";
        }

        [NonAction]
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        [NonAction]
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [NonAction]
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}