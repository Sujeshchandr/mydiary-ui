using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDiary.Domain.Abstract.Domains
{
    interface IFood
    {
        string session { get; set; }
        string amount { get; set; }
        string item { get; set; }
    }
}
