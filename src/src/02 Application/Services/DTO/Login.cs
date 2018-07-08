using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class Login :ILogin
    {
        public int LoginId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public IPeople CurrentUser { get; set; }
    }
}
