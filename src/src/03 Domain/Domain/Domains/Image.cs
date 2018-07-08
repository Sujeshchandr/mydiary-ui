using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;

namespace MyDiary.Domain.Domains
{
   public class Image : IImage
   {
       #region PRIVATE VARIABLES 

       private IImageRepository _imageRepository;

       #endregion

       #region PUBLIC PROPERTIES

       public int ImageId { get; set; }
       public byte[] UserImage { get; set; }

       #endregion

       #region CONSTRUCTOR
       public Image() { }
       public Image(IImageRepository imageRepoistory)
        {
            _imageRepository = imageRepoistory;
        }

       #endregion

       #region PUBLIC METHODS
       public IImage CreateImage(int imageId , byte[] userImage)
        {
            return new Image(null) {
                ImageId =imageId,
                UserImage =userImage
            };
        }

        public IImage GetUploadImageById(int imageId)
        {
            return _imageRepository.GetUploadImageById(imageId);
        }
        public int UploadImage(IImage image)
        {
            return _imageRepository.UploadImage(image);
        }
       #endregion
   }
}
