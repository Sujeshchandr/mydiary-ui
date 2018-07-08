using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IFood
    {
        string session { get; set; }
        string amount { get; set; }
        string item { get; set; }        
    }
}
