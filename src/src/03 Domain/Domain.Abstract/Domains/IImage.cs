using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Domain.Abstract.Domains
{
    public interface IImage
    {
        int ImageId { get; set; }
        byte[] UserImage { get; set; }

        IImage CreateImage(int imageId , byte[] userImage);
        IImage GetUploadImageById(int imageId);
        int UploadImage(IImage image);
       
    }
}
