using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Common.Extensions
{
    public static class DateTimeExtensions 
    {

        public static bool IsDefault(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return true;
            }
            return false;
        }
    }
}
