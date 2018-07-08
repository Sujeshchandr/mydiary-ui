using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDiary.Application.Services.Abstract.DTO
{
   public interface IExpenseType
   {

        int TypeId { get; set; }
        string Type { get; set; }
        int UserId { get; set; }

       //public List<IExpenseTypes> GetAllExpenseTypes()
       //{
       //    return new List<IExpenseTypes> 
       //     { 
       //         new ExpenseTypes {
       //             ExpenseTypeId =1,
       //             ExpenseType = "Food"
       //                 },
       //         new ExpenseTypes { 
       //             ExpenseTypeId =2,
       //             ExpenseType = "HomeRent"
       //                 }
       //     };
       //}
    }
}
