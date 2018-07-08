using NLog;
using System;
using System.Web.Mvc;

namespace MyDiary.UI.Controllers
{
    public class BootStrapController : Controller
    {

        #region PRIVATE PROPERTIES

        private ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public BootStrapController(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
        }

        #endregion

        //
        // GET: /BootStrap/

        [Route("BootStrap")]
        [Route("BootStrap/Index")]
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Index", "Home"); //default page for a logged in user
            }
            catch (Exception ex)
            {
                _logger.Error(ex);    
                throw;
            }
        }

    }
}
