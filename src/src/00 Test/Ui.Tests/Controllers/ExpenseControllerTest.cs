using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDiary.Application.Services.Abstract.Expenses;
using MyDiary.UI.Controllers.Expenses;
using MyDiary.UI.ViewModels.Expense;

namespace MyDiary.UI.Tests.Controllers
{
    [TestClass]
    public class ExpenseControllerTest
    {
        #region PRIVATE PROPERTIES

        private Mock<IExpenseService> _mockExpenseService;
       
        private ExpenseController _expenseController;

        #endregion

        #region INITIALIZE

        [TestInitialize]
        public void Initialize()
        {

            _mockExpenseService = new Mock<IExpenseService>(MockBehavior.Strict);
            _expenseController = new ExpenseController(_mockExpenseService.Object);
        }

        #endregion

        #region TEST METHODS

         #region GETALLEXPENSES

        [TestMethod]
        public void GetAllExpenses_WithValidInputs_RetursCorrectData()
        {
            //Arrange
            _mockExpenseService.Setup(x=>x.GetAll(It.IsAny<int>(),It.IsAny<MyDiary.Application.Services.Abstract.DTO.IExpenseFilter>(),1,false)).Returns(this.GetMockExpenses());

            //Act
            ExpenseSummaryViewModel result = (ExpenseSummaryViewModel)_expenseController.GetAllExpenses(1,null,null).Data;

            //Assert
            Assert.AreEqual(3, result.ExpenseViewModels.Count(), "Expected {0} but returns {1}", 3, result.ExpenseViewModels.Count());
        }

       #endregion

        #endregion

        #region PRIVATE METHODS

        private List<MyDiary.Application.Services.Abstract.DTO.IExpense> GetMockExpenses()
        {
            return new List<Application.Services.Abstract.DTO.IExpense>()
            {
                new Application.Services.DTO.Expense(){
                    ExpenseId = 100,
                    Type = new Application.Services.DTO.ExpenseType()
                    {
                        Type ="Expense Type1",
                        TypeId =1
                    },
                    ExpenseDate = DateTime.Now,
                    Amount = 100,
                    Description ="Expense Description",
                    Comments ="Expense Comments",
                    CurrentUser = new MyDiary.Application.Services.DTO.People()
                    {
                        UserId = 1
                    },
                    CreatedBy = 1,
                    TotalCount = 100,
                    UserId = 1
                },
                 
                new Application.Services.DTO.Expense(){
                    ExpenseId = 101,
                    Type = new Application.Services.DTO.ExpenseType()
                    {
                        Type ="Expense Type2",
                        TypeId =2
                    },
                    ExpenseDate = DateTime.Now,
                    Amount = 100,
                    Description ="Expense Description",
                    Comments ="Expense Comments",
                    CurrentUser = new MyDiary.Application.Services.DTO.People()
                    {
                        UserId = 1
                    },
                    CreatedBy = 1,
                    TotalCount = 100,
                    UserId = 1
                },
                
                new Application.Services.DTO.Expense(){
                    ExpenseId = 102,
                    Type = new Application.Services.DTO.ExpenseType()
                    {
                        Type ="Expense Type3",
                        TypeId =3
                    },
                    ExpenseDate = DateTime.Now,
                    Amount = 100,
                    Description ="Expense Description",
                    Comments ="Expense Comments",
                    CurrentUser = new MyDiary.Application.Services.DTO.People()
                    {
                        UserId = 1
                    },
                    CreatedBy = 1,
                    TotalCount = 100,
                    UserId = 1
                }

            };
        }

        #endregion
    }
}
