using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
   public interface IOpenLogin
    {
       string OpenUserId { get; set; }
       int SiteId { get; set; }
       int UserId { get; set; }
       IOpenLogin CreateOpenLogin(string openUserId, int siteId, int userId);
    }
}
