using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Domains
{
    public class ExpenseType : IExpenseType
    {
        private IExpenseRepository _expenseRepository;

        public ExpenseType() { }
        public ExpenseType(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }


        public class Food
        {
            public string session { get; set; }
            public string amount { get; set; }
            public string item { get; set; }

        }
     
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }

        public IList<IExpenseType> GetAllExpenseTypes(int userId)
        {
            return _expenseRepository.GetAllExpenseTypes(userId);
        }

        public bool AddExpenseType(string expenseType,int userId)
        {
            return _expenseRepository.AddExpenseType(expenseType,userId);
        }

        public IExpenseType Create(int TypeId, string Type, int userId)
        {
            return new ExpenseType()
            {
                TypeId = TypeId,
                Type = Type,
                UserId = userId
            };
        }
    }
}