﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
   public class Food :IFood
   {
       public string session { get; set; }
       public string amount { get; set; }
       public string item { get; set; }   

    }
}
