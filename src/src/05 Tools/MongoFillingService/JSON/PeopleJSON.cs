using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.MongoFillingService
{
    public class PeopleJSON
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<RoleJSON> UserRoles { get; set; }       
        public List<ImageJSON> UserImages { get; set; }
        public int SiteId { get; set; }
        public string SiteUserId { get; set; } 
    }
}