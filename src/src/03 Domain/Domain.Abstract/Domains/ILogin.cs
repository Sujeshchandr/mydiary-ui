using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
   public interface IUserLogin
    {
       int LoginId { get; set; }
       string EmailId { get; set; }
       string Password { get; set; }
       int ImageId { get; set; }
       int UserId { get; set; }
       IPeople CurrentUser { get; set; }

       IUserLogin CreateLogin(int loginId, string emailId, string password, int userId, IPeople currentUser = null );
    }
}
