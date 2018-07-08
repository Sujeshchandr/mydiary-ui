using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Domains
{
    public class Role :IRole
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }

        public IRole CreateRole(int roleId  , string roleCode)
        {
            return new Role { 
                RoleId =roleId,
                RoleCode =roleCode
            };
        }
    }
}
