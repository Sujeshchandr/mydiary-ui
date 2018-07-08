using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class Role : IRole
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
    }
}
