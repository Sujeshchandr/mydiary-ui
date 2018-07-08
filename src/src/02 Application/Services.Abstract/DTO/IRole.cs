using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IRole
    {
        int RoleId { get; set; }
        string RoleCode { get; set; }
    }
}
