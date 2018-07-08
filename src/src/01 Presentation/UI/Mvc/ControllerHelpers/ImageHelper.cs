using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ControllerHelpers
{
    public static class ImageHelper
    {
        
        # region Converting Image to Byte

        public static byte[] ReadImageAsByteAray(string p_postedImageFileName, string p_fileType)
        {

            bool isValidFileType = false;

            try
            {

                //List<FileInfo> files = GetFile(p_postedImageFileName, "*.Jpg*");
                List<FileInfo> files = GetFile(p_postedImageFileName, p_fileType);
                FileInfo file = null;
                if(files != null && files.Any())
                {
                    file = files[0];
                }

                if (p_fileType.ToLower() == file.Extension.ToLower())
                {

                    isValidFileType = true;                

                }

               

                if (isValidFileType)
                {
                  return GetBytesFromImage(file.FullName);   
                }

                return null; //TO DO==> pass default image

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

        #endregion

        #region Converting Byte to Image

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {

            System.Drawing.Image newImage = new Bitmap(0,0);



            string strFileName = GetTempFolderName() + "yourfilename.gif";

            if (byteArrayIn != null)
            {

                using (MemoryStream stream = new MemoryStream(byteArrayIn))
                {

                    newImage = System.Drawing.Image.FromStream(stream);



                    newImage.Save(strFileName);



                   // img.Attributes.Add("src", strFileName);

                }



               // lblMessage.Text = "The image conversion was successful.";

            }

            else
            {

              //Response.Write("No image data found!");

            }
            return newImage;

        }

        public static byte[] Get(HttpPostedFileBase httpPostedFileBase)
        {
            byte[] data;
            using (Stream inputStream = httpPostedFileBase.InputStream)
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

        #endregion

        #region Getting Temporary Folder Name

        private static string GetTempFolderName()
        {

            string strTempFolderName = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + @"\";



            if (Directory.Exists(strTempFolderName))
            {

                return strTempFolderName;

            }

            else
            {

                Directory.CreateDirectory(strTempFolderName);

                return strTempFolderName;

            }

        }

        #endregion

        private static byte[] GetBytesFromImage(String imageFile)
        {
            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(imageFile);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public static List<FileInfo> GetFile(string MyPath, string searchFilters)
        {           
            System.Collections.Generic.List<FileInfo> FilesInfo = GetFiles(MyPath, searchFilters);
            List<FileInfo> _files = (from file in FilesInfo
                         orderby file.Name
                         select file).ToList();
            return _files;
        }

        private static List<FileInfo> GetFiles(string MyPath, string searchFilters)
        {

            DirectoryInfo _dirInfo = new DirectoryInfo(MyPath);
            return System.Linq.Enumerable.ToList(_dirInfo.GetFiles(string.Format("*{0}", searchFilters), SearchOption.AllDirectories));         

        }
        
    }
}