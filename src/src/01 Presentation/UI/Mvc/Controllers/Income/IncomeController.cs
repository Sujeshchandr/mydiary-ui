using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.Abstract.Incomes;
using MyDiary.Application.Services.Abstract.IncomeTypes;
using MyDiary.UI.ControllerHelpers;
using MyDiary.UI.Filters;
using MyDiary.UI.ViewModels;
using MyDiary.UI.ViewModels.Income;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyDiary.UI.Controllers.Income
{
    [AuthenticateUser]
    public class IncomeController : Controller
    {
        #region PRIVATE PROPERTIES

        IIncomeService _incomeService;
        IIncomeTypeService _incomeTypeService;
        ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public IncomeController(ILogger logger, IIncomeService incomeService, IIncomeTypeService incomeTypeService)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (incomeService == null)
            {
                throw new ArgumentNullException("incomeService");
            }

            if (incomeService == null)
            {
                throw new ArgumentNullException("incomeService");
            }

            _incomeService = incomeService; 
            _incomeTypeService =incomeTypeService;
            _logger = logger; 

        }

        #endregion

        #region INCOMETYPES

        [HttpGet]
        [Route("Income/GetAllIncomeTypes")]
        public JsonResult GetAllIncomeTypes(int userId)
        {
            List<IIncomeType> incomeTypes = _incomeTypeService.GetAllIncomeTypes(userId);
            return Json(new IncomeControllerHelper().MapIncomeTypesDTOListToViewModelList(incomeTypes), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [Route("Income/AddIncomeType")]
        public int AddIncomeType(IncomeTypeViewModel incomeTypeViewModel)
        {

            return _incomeTypeService.AddIncomeType(new IncomeControllerHelper().MapIncomeTypeViewModelToDTO(incomeTypeViewModel));

        }

        #endregion

        #region PUBLIC METHODS

        //
        // GET: /Income/
        [Route("Income")]
        [Route("Income/Index")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Income/Create

        [Route("Income/GetIncomeToolBoxTemplate")]
        public PartialViewResult GetIncomeToolBoxTemplate()
        {
            try
            {
                return PartialView("~/Views/Shared/_ToolBoxPartial.cshtml");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("Income/AddIncome")]
        public int AddIncome(IncomeViewModel incomeViewModel)
        {
            try
            {
                return _incomeService.Add(new IncomeControllerHelper().MapIncomeViewModelToDTO(incomeViewModel));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }

        [HttpPut]
        [Route("Income/EditIncome")]
        public ActionResult EditIncome(IncomeViewModel incomeViewModel)
        {
            try
            {
                _incomeService.Edit(new IncomeControllerHelper().MapIncomeViewModelToDTO(incomeViewModel));
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpDelete]
        [Route("Income/DeleteIncome")]
        public JsonResult DeleteIncome(int incomeId)
        {
            try
            {
                if (incomeId <= 0)
                {
                    throw new ArgumentNullException("income should be non negative");
                }

                _incomeService.DeleteById(incomeId);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("Income/GetAllIncomes")]
        public JsonResult GetAllIncomes(int userId,string incomeDate, List<int> incomeTypes, int currentPage = 1)
        {
            try
            {
                if (userId == 0)
                {
                    throw new ArgumentException("user id cannot be zero");
                }

                IncomeFilterViewModel filters = new IncomeFilterViewModel()
                {
                    IncomeDate = incomeDate,
                    IncomeTypes = incomeTypes
                };
                IncomeSummaryViewModel incomeSummaryViewModel = new IncomeControllerHelper().MapIncomeDTOListToIncomeSummaryViewModel(
                                           _incomeService.GetAll(userId, new IncomeControllerHelper().MapIncomeFilterViewModelToIncomeFilter(filters), currentPage, false), currentPage);

                return Json(incomeSummaryViewModel, JsonRequestBehavior.AllowGet);
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

    public class Mail
    {
        public string NotificationKeyText { get; set; }
        public string Subject { get; set; }
        public IList<People> To { get; set; }
        public IList<People> Cc { get; set; }
        public IList<People> Bcc { get; set; }
        public People From { get; set; }
        public string Message { get; set; }

    }

    public class People
    {
     public int UserId { get; set; }
     public string EmailId { get; set; }
     public IList<Role> UserRoles { get; set; }
     public string FirstName { get; set; }
     public string MiddleName { get; set; }
     public string LastName { get; set; }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
    }