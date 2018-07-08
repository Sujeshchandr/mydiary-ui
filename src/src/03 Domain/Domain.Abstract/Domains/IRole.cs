using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
   public interface IRole
    {
       int RoleId { get; set; }
       string RoleCode { get; set; }

       IRole CreateRole(int roleId ,string roleName);
     

    }
}
