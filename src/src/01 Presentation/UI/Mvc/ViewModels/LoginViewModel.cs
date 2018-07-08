using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class LoginViewModel
    {
        public int LoginId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
       
    }
}