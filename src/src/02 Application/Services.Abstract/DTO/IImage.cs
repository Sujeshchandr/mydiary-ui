using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.DTO
{
    public interface IImage
    {
        int ImageId { get; set; }
        byte[] UserImage { get; set; }
    }
}
