// ***********************************************************************
// Assembly         : MyDiary.UI
// Author           : Administrator
// Created          : 01-15-2014
//
// Last Modified By : Administrator
// Last Modified On : 01-20-2014
// ***********************************************************************
// <copyright file="ExpenseController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Web.Mvc;
using MyDiary.Application.Services.Abstract.Expenses;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.UI.ViewModels;
using MyDiary.UI.Filters;
using MyDiary.UI.ViewModels.Expense;
using MyDiary.UI.ControllerHelpers;
using Newtonsoft.Json;
using System;
using NLog;
namespace MyDiary.UI.Controllers.Expenses
{
    [AuthenticateUser]
    public class ExpenseController : Controller
    {
        #region PRIVATE PROPERTIES

        private IExpenseService _expenseService;
        private ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public ExpenseController(ILogger logger, IExpenseService expenseService)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (expenseService == null)
            {
                throw new ArgumentNullException("expenseService");
            }
            
            _logger = logger;
            _expenseService = expenseService;

        }


        #endregion

        #region PUBLIC METHODS

        [Route("Expense")]
        [Route("Expense/Index")]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View("~/Views/Expense/Index.cshtml");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("Expense/AddExpense")]
        public JsonResult AddExpense(ExpenseViewModel expenseViewModel)
        {
            try
            {
                bool result = _expenseService.Add(new ExpenseControllerHelper().MapExpenseViewModelToExpenseDTO(expenseViewModel));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }

        [HttpPut]
        [Route("Expense/EditExpense")]
        public JsonResult EditExpense(ExpenseViewModel expenseViewModel)
        {
            try
            {
                _expenseService.Update(new ExpenseControllerHelper().MapExpenseViewModelToExpenseDTO(expenseViewModel));
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                 _logger.Error(ex);
                throw;
            }
        }

        [HttpDelete]
        [Route("Expense/DeleteExpense")]
        public JsonResult DeleteExpense(int expenseId)
        {
            try
            {
                _expenseService.DeleteById(expenseId);
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                 _logger.Error(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("Expense/GetAllExpenses")]
        public JsonResult GetAllExpenses(int userId, string expenseDate, string expenseMonth , List<int> expenseTypes, int currentPage = 1)
        {
            try
            {
                if (userId == 0)
                {
                    throw new ArgumentException("user id cannot be zero");
                }

                var filters = new ExpenseFilterViewModel()
                {
                    ExpenseDate = expenseDate,
                    ExpenseTypes = expenseTypes,
                    ExpenseMonth = expenseMonth
                };

                var expenseSummaryViewModel = new ExpenseControllerHelper().MapExpenseDTOListToExpenseSummaryViewModel(
                                                                      _expenseService.GetAll(userId,
                                                                                             new ExpenseControllerHelper().MapExpenseFilterViewModelToExpenseFilter(filters),
                                                                                             currentPage,
                                                                                             false),
                                                                      currentPage);

                return Json(expenseSummaryViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("Expense/GetExpenseToolBoxTemplate")]
        public PartialViewResult GetExpenseToolBoxTemplate()
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

        #region EXPENSE TYPE

        [HttpGet]
        [Route("Expense/GetAllExpenseTypes")]
        public JsonResult GetAllExpenseTypes(int userId)
        {
            try
            {
                IList<IExpenseType> expenseTypes = _expenseService.GetAllExpenseTypes(userId);
                return Json(expenseTypes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }

        [HttpPost]
        [Route("Expense/AddExpenseType")]
        public JsonResult AddExpenseType(ExpenseTypeViewModel expenseTypeViewModel)
        {
            try
            {
                bool result = _expenseService.AddExpenseType(expenseTypeViewModel.Type, expenseTypeViewModel.UserId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }

        #endregion

        #endregion

    }
}
