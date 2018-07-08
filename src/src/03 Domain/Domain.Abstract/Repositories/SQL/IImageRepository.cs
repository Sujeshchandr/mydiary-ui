using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.SQL
{
    public interface IImageRepository
    {
        IImage GetUploadImageById(int uploadId);
        int UploadImage(IImage image);
    }
}
