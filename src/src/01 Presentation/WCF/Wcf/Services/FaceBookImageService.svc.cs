using MyDiary.WCF.Json;
using MyDiary.WCF.ServiceHelpers;
using MyDiary.WCF.Services.Abstract;
using NLog;
using System;
using System.Net;

namespace MyDiary.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FaceBookImageService" in code, svc and config file together.

    // NOTE: In order to launch WCF Test Client for testing this service, please select FaceBookImageService.svc or FaceBookImageService.svc.cs at the Solution Explorer and start debugging.

    public class FaceBookImageService : IFaceBookImageService
    {
        #region PRIVATE PROPERTIES

        private readonly Application.Services.Abstract.Images.IImageService _imageService;

        private readonly ILogger _logger;

        #endregion

        #region CONSTRUCTOR
        public FaceBookImageService() { }

        public FaceBookImageService(Application.Services.Abstract.Images.IImageService imageService,ILogger logger)
        {
            if (imageService == null)
            {
                throw new ArgumentNullException("imageService");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _imageService = imageService;
            _logger = logger;
        }  

        #endregion

        public ImageJSON Save(FacebookJSON facebookJson)
        {
            int uploadImageId = 0;
            Application.Services.Abstract.DTO.IImage image ;

            try
            {
                WebRequest request = WebRequest.Create(facebookJson.ImageUrl);
                WebResponse response = request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                    {
                        return null;
                    }

                    image = new Application.Services.DTO.Image()
                    {
                        UserImage = ImageSericeHelper.Get(stream)
                    };
                }

                //// Save the file to db
                uploadImageId = _imageService.UploadImage(image);                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                throw;
            }

            return new ImageJSON()
            {
                UploadedImageId = uploadImageId
            };
        }
    }
}
