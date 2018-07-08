using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using MyDiary.Application.Services.Abstract.Images;
using MyDiary.Common;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.WCF.Json;
using MyDiary.WCF.ServiceHelpers;
using MYDiary.SQLProvider.Connection;
using Ninject.Activation;
using MyDiary.Common.Parser;
using MyDiary.WCF.Services.Abstract;
using NLog;

namespace MyDiary.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ImageWcfService : IImageWcfService
    {

        #region PRIVATE PROPERTIES

        private readonly Application.Services.Abstract.Images.IImageService _imageService;

        private readonly ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public ImageWcfService(Application.Services.Abstract.Images.IImageService imageService, ILogger logger)
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

        #region PUBLIC METHODS
       
        public Stream Get(string uploadImageId)
        {
            try
            {
                MemoryStream ms = new MemoryStream(this.GetUploadImageById(int.Parse(uploadImageId)));
                ms.Position = 0;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Slug", "title");
                WebOperationContext.Current.OutgoingRequest.Method = "GET";
                WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";

                return ms;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }

        public Stream GetByUserId(string userId)
        {
            try
            {
                int uploadImageId = 5836; //ToDo ==> get imageId of user from api
                MemoryStream ms = new MemoryStream(this.GetUploadImageById(uploadImageId));
                ms.Position = 0;
                WebOperationContext.Current.OutgoingRequest.Headers.Add("Slug", "title");
                WebOperationContext.Current.OutgoingRequest.Method = "GET";
                WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";

                return ms;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }

        public ImageJSON Upload()
        {
            try
            {               
               
                MultipartFormDataParser parser = new MultipartFormDataParser(HttpContext.Current.Request.InputStream);

                ImageJSON imageJson = new ImageJSON();
                int uploadImageId = 0;

                // Single file access:
                FilePart file = parser.Files.First();
                string filename = file.FileName;
                if (file != null && file.Data != null)
                {
                    // Save the file to db
                    Application.Services.Abstract.DTO.IImage image = new Application.Services.DTO.Image();
                    image.UserImage = parser.ReadFully(file.Data);
                    uploadImageId = _imageService.UploadImage(image);
                    imageJson.UploadedImageId = uploadImageId;
                }
                else
                {
                    throw new WebException("The posted file was not recognised.", WebExceptionStatus.SendFailure);
                }
                
                return imageJson;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
            
          
        }

      
        #endregion

        #region PRIVATE METHODS

        private byte[] GetUploadImageById(int uploadId)
        {
            MyDiary.Application.Services.Abstract.DTO.IImage image = _imageService.GetUploadImageById(uploadId);
            if(image != null)
                return image.UserImage;
            else
                return new byte[1];
        }

        private IImage GetImageFromDataTable(DataTable dtImage)
        {
            if (dtImage != null && dtImage.Rows.Count > 0)
            {

                foreach (DataRow dr in dtImage.Rows)
                {
                    return GetImageFomDataRow(dr);
                }
            }
            return new MyDiary.Domain.Domains.Image(null);

        }

        private IImage GetImageFomDataRow(DataRow drImage)
        {
            IImage image = new MyDiary.Domain.Domains.Image(null);
            foreach (DataColumn dc in drImage.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.Image.Fields.IMAGE:
                        image.UserImage = (byte[])(drImage[Constants.StoredProcedures.Image.Fields.IMAGE]);
                        break;
                }
            }
            return image;
        }

        private static byte[] GetImage(HttpPostedFile httpPostedFile)
        {
            byte[] data;
            using (Stream inputStream = httpPostedFile.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        //profilePic = getUrlImage("https://graph.facebook.com/" + me.id + "/picture")
        private Image getUrlImage(string url)
        {
            WebResponse result = null;
            Image rImage = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                result = request.GetResponse();
                Stream stream = result.GetResponseStream();
                BinaryReader br = new BinaryReader(stream);
                byte[] rBytes = br.ReadBytes(1000000);
                br.Close();
                result.Close();
                MemoryStream imageStream = new MemoryStream(rBytes, 0, rBytes.Length);
                imageStream.Write(rBytes, 0, rBytes.Length);
                rImage = System.Drawing.Image.FromStream(imageStream, true);
                imageStream.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(c.Message);
            }
            finally
            {
                if (result != null) result.Close();
            }
            return rImage;

        }
        #endregion             
       
    }
 
}
