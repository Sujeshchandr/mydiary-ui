using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
   public interface ILogin
    {
        int LoginId { get; set; }
        string EmailId { get; set; }
        string Password { get; set; }
        int UserId { get; set; }
        int ImageId { get; set; }
        IPeople CurrentUser { get; set; }
    }
}
