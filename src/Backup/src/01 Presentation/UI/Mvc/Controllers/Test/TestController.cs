using System.Web.Mvc;
using System;
using MyDiary.UI.ViewModels.Test.JqueryDataTable;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace MyDiary.UI.Controllers.Expenses
{
    [AllowAnonymous]
    public class TestController : Controller
    {

        #region PUBLIC METHODS

        [Route("Test")]
        [Route("Test/Index")]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View("~/Views/Test/Vue/Index.cshtml");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult JqueryDataTable()
        {
            try
            {
                return View("~/Views/Test/Index_JqueryDataTable.cshtml");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public ContentResult GetJsonForDataTable(JQDTParams param)
        {
            try
            {
                JqueryDataTable jsonData = null;
                var camelCaseFormatter = new JsonSerializerSettings();
                camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

                if ((param.draw % 2) == 1)
                {
                    using (StreamReader r = new StreamReader("F:/MyProjects/02 PITS/MyDiary/src/01 Presentation/UI/Mvc/Controllers/Test/dataTable.json"))
                    {
                        string json = r.ReadToEnd();
                        jsonData = JsonConvert.DeserializeObject<JqueryDataTable>(json, camelCaseFormatter);

                        if (param.length > 0)
                        {
                            jsonData.Data = jsonData.Data
                                                    .ToList()
                                                    .Skip(param.start)
                                                    .Take(param.length)
                                                    .ToList();
                        }
                        //jsonData.RecordsFiltered = jsonData.Data.Count();
                        jsonData.Draw = param.draw;
                    }
                }
                else
                {
                    jsonData = new JqueryDataTable() { Data = new List<Employee>() };
                }

                return Content(JsonConvert.SerializeObject(jsonData), "application/json");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
