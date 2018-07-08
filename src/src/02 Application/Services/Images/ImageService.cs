using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.Abstract.Images;
using MyDiary.Application.Services.DTO;


namespace MyDiary.Application.Services.Images
{
    public class ImageService :IImageService
    {

        #region PRIVATE PROPERTIES

        private MyDiary.Domain.Abstract.Domains.IImage _imageDomain;
        private MyDiary.Domain.Abstract.Domains.IImage _imageTestDomain;
        #endregion

        #region CONSRTUCTOR
//[Named("image")]
        public ImageService(MyDiary.Domain.Abstract.Domains.IImage imageDomain)
        {
            _imageDomain = imageDomain;            
        }

        #endregion

        #region  PUBLIC METHODS

        public IImage GetUploadImageById(int uploadId)
        {
            return MapImageDomainToDTO(_imageDomain.GetUploadImageById(uploadId));
        }

        public int UploadImage(IImage image)
        {
            return _imageDomain.UploadImage(_imageDomain.CreateImage(image.ImageId, image.UserImage));
        }
       
        #endregion

        #region PRIVATE METHODS

          #region MAPPING METHODS
     
           private IImage MapImageDomainToDTO(MyDiary.Domain.Abstract.Domains.IImage imageDomain)
        {
            return new Image()
            {
                ImageId = imageDomain.ImageId,
                UserImage = imageDomain.UserImage
            };
             
           }

           private MyDiary.Domain.Abstract.Domains.IImage  MapImageDTOToDomain(IImage imageDTO)
           {
               return _imageDomain.CreateImage(imageDTO.ImageId, imageDTO.UserImage);
           }

          #endregion

        #endregion
    }
}
