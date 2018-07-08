using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;

namespace MyDiary.Domain.Domains
{
   public class People : IPeople
   {
       #region PRIVATE PROPERTIES

       IPeopleRepository _peopleRepository;
       IImage _imageDomain;
       IRole _roleDomain;

       #endregion

       #region CONSTRUCTOR
       public People() { }
       public People(IPeopleRepository peopleRepository,IImage imageDomain,IRole roleDomain)
       {
           _peopleRepository = peopleRepository;
           _imageDomain = imageDomain;
           _roleDomain = roleDomain;

       }

       #endregion

       #region PUBLIC PROPERTIES

       public int UserId { get; set; }
       public string EmailId { get; set; }
       public string FirstName { get; set; }
       public string MiddleName { get; set; }
       public string LastName { get; set; }
       public List<IRole> UserRoles { get; set; }
       public string Password { get; set; }
       public List<IImage> UserImages { get; set; }
       public int SiteId { get; set; }
       public string SiteUserId { get; set; } 

       #endregion

       #region PUBLIC METHODS

       public int Add(IPeople user)
       {
           if (user == null) 
               throw new ArgumentNullException("User");
           if (user.UserRoles == null)
               throw new ArgumentNullException("User Roles");
           
           int userId = _peopleRepository.AddUser(user);

           _peopleRepository.AddUserLoginInformation(new UserLogin()
           {
               UserId = userId,
               EmailId = user.EmailId,
               Password = user.Password

           });

           return userId;
       }

       public IPeople LogIn(IUserLogin login)
       {
           if (login == null)
           {
               throw new ArgumentNullException("login");
           }

           if (string.IsNullOrEmpty(login.EmailId))
           {
               throw new ArgumentNullException("Emaild");
           }

           if (string.IsNullOrEmpty(login.Password))
           {
               throw new ArgumentNullException("Password");
           }

           return _peopleRepository.GetByLoginDetails(login.EmailId, login.Password);
       }

       public IPeople LoginByOpenId(IOpenLogin openLogin)
       {
           if (openLogin == null)
               throw new ArgumentNullException("openLogin");
           if (openLogin.SiteId == 0)
               throw new ArgumentNullException("SiteId");
           if (string.IsNullOrEmpty(openLogin.OpenUserId))
               throw new ArgumentNullException("OpenUserId");

           return _peopleRepository.GetByOpenSiteDetails(openLogin.SiteId, openLogin.OpenUserId);
       }

       public int UploadImage(IImage image)
       {
           return _peopleRepository.UploadImage(image);
       }

       public List<IPeople> GetAll()
       {
           return _peopleRepository.GetAll();
       }

       public IPeople CreateUser(int userId, string emailId, string firstName, string middleName, string lastName, int siteId,string siteUserId, List<IRole> roles, string password, List<IImage> userImages)
       {

           return new People()
           {
               UserId = userId,
               EmailId = emailId,
               FirstName = firstName,
               MiddleName = middleName,
               LastName = lastName,
               Password =password,
               SiteId = siteId,
               SiteUserId =  siteUserId,
               UserRoles = (from r in roles select _roleDomain.CreateRole(r.RoleId, r.RoleCode)).ToList(),
               UserImages = (from i in userImages select _imageDomain.CreateImage(i.ImageId, i.UserImage)).ToList()

           };
       }

       #endregion

       #region PRIVATE METHODS
       private IRole CreateRole(IRole role)
       {
           return new Role {
               RoleId = role.RoleId,
               RoleCode =role.RoleCode
           };

       }
       #endregion
             
   }
}
