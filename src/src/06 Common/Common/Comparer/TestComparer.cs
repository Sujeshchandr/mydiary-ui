using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Common.Comparer
{
    public class TestComparer : EqualityComparer<DataRow>
    {
        public TestComparer()
        {

        }

        public override bool Equals(DataRow x, DataRow y)
        {
            if((x==null) || (y ==null)) return false;
            return x["Amount"] == y["Amount"];
        }

        public override int GetHashCode(DataRow obj)
        {
            return 0;
            //throw new NotImplementedException();
        }
    }
}
