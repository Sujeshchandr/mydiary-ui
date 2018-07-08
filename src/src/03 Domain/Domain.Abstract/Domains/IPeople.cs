using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
    public interface IPeople
    {
        #region PROPERTIES

        int UserId { get; set; }
        string EmailId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        List<IRole> UserRoles { get; set; }
        string Password { get; set; }
        int SiteId { get; set; }
        string SiteUserId { get; set; }
        List<IImage> UserImages{get;set;}

        #endregion

        #region METHODS

        int Add(IPeople user);
        IPeople LogIn(IUserLogin login);
        IPeople LoginByOpenId(IOpenLogin openLogin);
        int UploadImage(IImage image);
        List<IPeople> GetAll();

        IPeople CreateUser(int userId, string emailId, string firstName, string middleName, string lastName, int siteId,string siteUserId,List<IRole> roles, string password, List<IImage> userImages);

       #endregion
       
    }
}
