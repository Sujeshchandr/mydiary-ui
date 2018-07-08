using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDairy.WebApi.Proxy.Json
{
    public class LoginJson
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}