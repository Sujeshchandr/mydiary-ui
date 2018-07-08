using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.DTO;

namespace MyDiary.API.ControllerHelpers
{
    public class LoginControllerHelper
    {
        public ILogin Map_LoginJSON_To_DTO(LoginJson loginJson)
        {
            return new Login()
            {
                EmailId = loginJson.EmailId,
                Password =loginJson.Password
            };
        }
    }
}