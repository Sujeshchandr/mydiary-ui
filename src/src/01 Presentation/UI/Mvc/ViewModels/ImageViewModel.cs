using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.Controllers
{
    public class ImageViewModel
    {
        public int ImageId { get; set; }
        public byte[] UserImage { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public string UserGuid { get; set; }
        public int UploadedImageId { get; set; }
    }
}