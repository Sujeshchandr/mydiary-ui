using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
   public class OpenLogin :IOpenLogin
    {
       public string OpenUserId { get; set; }
       public int SiteId { get; set; }
       public int UserId { get; set; }
    }
}
