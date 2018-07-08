using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class People :IPeople
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int SiteId { get; set; }
        public string SiteUserId { get; set; }
        public List<IRole> UserRoles { get; set; }
        public List<IImage> UserImages { get; set; }
    }
}
