using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDiary.UI.ViewModels.ToolBox;
using NLog;

namespace MyDiary.UI.Controllers
{
    public class ToolBoxController : Controller
    {

        #region PRIVATE PROPERTIES

        private ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public ToolBoxController(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
        }

        #endregion

        //
        // GET: /ToolBox/

        [Route("ToolBox")]
        [Route("ToolBox/Get")]
        public PartialViewResult Get(string type)
        {
            try
            {
                return PartialView("~/Views/Shared/_ToolBoxPartial.cshtml", GetToolBoxByType(type));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private ToolBoxViewModel GetToolBoxByType(string type)
        {
            ToolBoxViewModel toolBoxViewModel = new ToolBoxViewModel();
            toolBoxViewModel.Actions = new List<ViewModels.ToolBox.ToolBoxActions>();

            switch (type)
            {
                case "Expense":
                    toolBoxViewModel.Actions.Add(
                        new ViewModels.ToolBox.ToolBoxActions()
                        {
                            Text = "Add your expenses",
                            CssClass = "addexpenses_toolbox",
                            Action = "AddExpenses"
                        }
                    );
                    break;

                case "Income":
                    toolBoxViewModel.Actions.Add(
                       new ViewModels.ToolBox.ToolBoxActions()
                       {
                           Text = "Add your incomes",
                           CssClass = "addincomes_toolbox",
                           Action = "AddIncomes"
                       }
                   );
                    break;
                default:
                    break;
            }

            return toolBoxViewModel;
        }

    }
}

