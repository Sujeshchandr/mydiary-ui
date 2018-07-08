using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.Abstract.People;
using System.Transactions;

namespace MyDiary.Application.Services.People
{
   public class PeopleService :IPeopleService
    {

       #region PRIVATE PROPERTIES
       private MyDiary.Domain.Abstract.Domains.IPeople _peopleDomain;
       private MyDiary.Domain.Abstract.Domains.IRole _roleDomain;
       private MyDiary.Domain.Abstract.Domains.IImage _imageDomain;
       MyDiary.Domain.Abstract.Domains.IUserLogin _loginDomain;
       MyDiary.Domain.Abstract.Domains.IOpenLogin _openLoginDomain;
     #endregion

       #region CONSTRUCTOR
       public PeopleService(MyDiary.Domain.Abstract.Domains.IPeople peopleDomain,MyDiary.Domain.Abstract.Domains.IRole roleDomain,MyDiary.Domain.Abstract.Domains.IImage imageDomain,
           MyDiary.Domain.Abstract.Domains.IUserLogin loginDomain, MyDiary.Domain.Abstract.Domains.IOpenLogin openloginDomain)
       {
           _peopleDomain = peopleDomain;
           _roleDomain =roleDomain;
           _imageDomain =imageDomain;
           _loginDomain =loginDomain;
           _openLoginDomain = openloginDomain;
       }
       #endregion

       #region PUBLIC METHODS

       public int Add(IPeople user)
       {
           if (user == null)
               throw new ArgumentNullException("User");
           if (user.UserRoles == null)
               throw new ArgumentNullException("User roles");
           int userId =0;
           using (TransactionScope scope = new TransactionScope())
           {
               userId = _peopleDomain.Add(MapPeopleDTOtoDomain(user));
               scope.Complete();
           }
           return userId;
       }

       public IPeople LogIn(ILogin login)
       {
           if (login == null)
           {
               throw new ArgumentNullException("loginViewModel");
           }

           if (string.IsNullOrEmpty(login.EmailId))
           {
               throw new ArgumentNullException("Emaild");
           }

           if (string.IsNullOrEmpty(login.Password))
           {
               throw new ArgumentNullException("Password");
           }

           return this.Map_PeopleDomain_To_PeopleDTO(_peopleDomain.LogIn(MapLoginDTOToDomain(login)));
       }

       public IPeople LogInByOpenId(IOpenLogin openLogin)
       {
           return this.Map_PeopleDomain_To_PeopleDTO(_peopleDomain.LoginByOpenId(MapOpenLoginDTOToDomain(openLogin)));
       }

       public int UploadImage(IImage image)
       {
           return _peopleDomain.UploadImage(_imageDomain.CreateImage(image.ImageId, image.UserImage));
       }

       public List<IPeople> GetAll()
       {
           return this.Map_PeopleDomainList_To_PeopleDTOList(_peopleDomain.GetAll());
       }

       #endregion

       #region PRIVATE METHODS

       private List<IPeople> Map_PeopleDomainList_To_PeopleDTOList(List<MyDiary.Domain.Abstract.Domains.IPeople> peopleDomainList)
       {
           return (from p in peopleDomainList select this.Map_PeopleDomain_To_PeopleDTO(p)).ToList<IPeople>();
       }

       private IPeople Map_PeopleDomain_To_PeopleDTO(MyDiary.Domain.Abstract.Domains.IPeople peopleDomain)
       {
           return new MyDiary.Application.Services.DTO.People()
           {
               UserId = peopleDomain.UserId,
               FirstName = peopleDomain.FirstName,
               MiddleName = peopleDomain.MiddleName,
               LastName = peopleDomain.LastName,
               EmailId = peopleDomain.EmailId,
               UserRoles = this.Map_RoleDomainList_To_DTO(peopleDomain.UserRoles),
               UserImages = this.Map_ImageDomainList_To_DTO(peopleDomain.UserImages),
               SiteId = peopleDomain.SiteId,
               SiteUserId = peopleDomain.SiteUserId
           };
       }

       private MyDiary.Domain.Abstract.Domains.IPeople MapPeopleDTOtoDomain(IPeople user)
       {
         return  _peopleDomain.CreateUser(user.UserId,user.EmailId,user.FirstName,user.MiddleName,
                                   user.LastName, user.SiteId,user.SiteUserId,MapRoleDTOtoDomain(user.UserRoles), user.Password,MapUserImagesDTOtoDomain(user.UserImages));
       }

       private List<IRole> Map_RoleDomainList_To_DTO(List<MyDiary.Domain.Abstract.Domains.IRole> roleDomainList)
       {
           return (from r in roleDomainList select Map_RoleDomain_To_DTO(r)).ToList<IRole>();
       }

       private IRole Map_RoleDomain_To_DTO(MyDiary.Domain.Abstract.Domains.IRole roleDomain)
       {
           return new MyDiary.Application.Services.DTO.Role()
           {
               RoleId = roleDomain.RoleId,
               RoleCode = roleDomain.RoleCode
           };
       }

       private List<IImage> Map_ImageDomainList_To_DTO(List<MyDiary.Domain.Abstract.Domains.IImage> imageDomainList)
       {
           return (from i in imageDomainList select Map_ImageDomain_To_DTO(i)).ToList<IImage>();
       }

       private IImage Map_ImageDomain_To_DTO(MyDiary.Domain.Abstract.Domains.IImage imageDomain)
       {
           return new MyDiary.Application.Services.DTO.Image()
           {
               ImageId = imageDomain.ImageId,
               UserImage = imageDomain.UserImage
           };
       }

       private List<MyDiary.Domain.Abstract.Domains.IRole> MapRoleDTOtoDomain(List<IRole> roles)
       {
           return (from r in roles select _roleDomain.CreateRole(r.RoleId, r.RoleCode)).ToList();
       }

       private List<MyDiary.Domain.Abstract.Domains.IImage> MapUserImagesDTOtoDomain(List<IImage> userImages)
       {
           return (from i in userImages select _imageDomain.CreateImage(i.ImageId,i.UserImage)).ToList();

       }

       private MyDiary.Domain.Abstract.Domains.IUserLogin MapLoginDTOToDomain(ILogin loginDTO)
       {
           return _loginDomain.CreateLogin(loginDTO.LoginId,loginDTO.EmailId, loginDTO.Password, loginDTO.UserId);
       }

       private MyDiary.Domain.Abstract.Domains.IOpenLogin MapOpenLoginDTOToDomain(IOpenLogin loginDTO)
       {
           return _openLoginDomain.CreateOpenLogin(loginDTO.OpenUserId, loginDTO.SiteId, loginDTO.UserId);
       }

       private ILogin MapLoginDomainToDTO(MyDiary.Domain.Abstract.Domains.IUserLogin loginDomain)
       {
           return new Application.Services.DTO.Login()
           {
               LoginId = loginDomain.LoginId,
               UserId = loginDomain.UserId,
               EmailId = loginDomain.EmailId,
               ImageId = loginDomain.ImageId,
               CurrentUser = new MyDiary.Application.Services.DTO.People()
               {
                   FirstName = loginDomain.CurrentUser.FirstName,
                   MiddleName =loginDomain.CurrentUser.MiddleName,
                   LastName =loginDomain.CurrentUser.LastName
               }

           };

       }

       #endregion
      
    }
}
