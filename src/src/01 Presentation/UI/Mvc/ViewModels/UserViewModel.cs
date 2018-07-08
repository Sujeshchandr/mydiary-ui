using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.UI.Controllers;

namespace MyDiary.UI.ViewModels
{
    public class UserViewModel
    {
       public int UserId { get; set; }
       public string EmailId { get; set; }
       public string FirstName { get; set; }
       public string MiddleName { get; set; }
       public string LastName { get; set; }
       public string Password { get; set; }
       public int SiteId { get; set; }
       public string SiteUserId { get; set; }
       public List<RoleViewModel> UserRoles { get; set; }
       public List<ImageViewModel> UserImages { get; set; }
    }
}