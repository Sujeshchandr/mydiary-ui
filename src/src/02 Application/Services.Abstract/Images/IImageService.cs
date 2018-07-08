using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.Application.Services.Abstract.Images
{
   public interface IImageService
    {   
       #region  METHODS

       IImage GetUploadImageById(int uploadId);
       int UploadImage(IImage image);
       
      #endregion
    }
}
