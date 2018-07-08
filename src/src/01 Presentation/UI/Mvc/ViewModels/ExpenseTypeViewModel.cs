using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class ExpenseTypeViewModel
    {
        public class Food
        {
            public string session { get; set; }
            public string amount { get; set; }
            public string item { get; set; }

        }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}