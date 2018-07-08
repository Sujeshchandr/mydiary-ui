using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDiary.UI.Controllers.Home
{
    public class HomeController : Controller
    {

       #region PRIVATE PROPERTIES

        private ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public HomeController(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
        }

        [Route("Home")]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }
        #endregion
    }
}
