using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;
using ILogin = MyDiary.Application.Services.Abstract.DTO.ILogin;
using IPeople = MyDiary.Application.Services.Abstract.DTO.IPeople;

namespace MyDiary.Application.Services.Abstract.People
{
   public interface IPeopleService
   {
       #region  METHODS

       int Add(IPeople user); 
       IPeople LogIn(ILogin login);
       IPeople LogInByOpenId(IOpenLogin openLogin);
       int UploadImage(IImage image);
       List<IPeople> GetAll();

       #endregion
   }
}
