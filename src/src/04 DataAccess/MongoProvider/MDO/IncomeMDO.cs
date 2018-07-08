using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MyDiary.MongoProvider.MDO
{
    public class IncomeMDO
    {
        public ObjectId Id {get;set;}
      //  public int RowNumber { get; set; }
        public int IncomeId { get; set; }
        public int UserId { get; set; }           
        public IncomeTypeMDO IncomeType { get; set; }
        public float Amount { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IncomeDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public  DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ModifiedDate { get; set; }
    }
}
