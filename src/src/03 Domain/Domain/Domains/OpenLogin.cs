using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Domains
{
    public class OpenLogin : IOpenLogin
    {
       public string OpenUserId { get; set; }
       public int SiteId { get; set; }
       public int UserId { get; set; }
       public IOpenLogin CreateOpenLogin(string openUserId, int siteId, int userId)
       {
           return new OpenLogin()
               {
                   OpenUserId = openUserId,
                   SiteId = siteId,
                   UserId = userId

               };
       }

    }
}
