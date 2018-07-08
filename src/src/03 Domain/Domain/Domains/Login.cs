using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Domains
{
    public class UserLogin : IUserLogin
    {  
        public int LoginId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public IPeople CurrentUser { get; set; }

        public IUserLogin CreateLogin(int loginId, string emailId, string password, int userId, IPeople currentUser = null)
        {
            return new UserLogin()
            {
                LoginId =loginId,
                EmailId = emailId,
                Password =password,
                UserId = userId,
                CurrentUser = currentUser

            };
        }


        
    }

   

}
