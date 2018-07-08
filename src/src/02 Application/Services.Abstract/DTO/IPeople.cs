using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IPeople
    {
        int UserId { get; set; }
        string EmailId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        int SiteId { get; set; }
        string SiteUserId { get; set; }
        List<IRole> UserRoles { get; set; }
        List<IImage> UserImages { get; set; }
    }
}
