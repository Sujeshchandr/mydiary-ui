using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.DTO
{
    public class Image : IImage
    {
        public int ImageId { get; set; }
        public byte[] UserImage { get; set; }
    }
}
