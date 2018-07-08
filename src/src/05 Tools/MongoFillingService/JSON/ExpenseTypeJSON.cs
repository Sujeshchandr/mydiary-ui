using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.MongoFillingService.JSON
{
    [Serializable]
    public class ExpenseTypeJSON
    {
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}
