using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.MongoFillingService
{
    [Serializable]
    public class IncomeTypeJSON
    {
        public int TypeId { get; set; }

        public string Type { get; set; }

    }
}