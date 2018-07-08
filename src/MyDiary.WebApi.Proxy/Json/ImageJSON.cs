using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDairy.WebApi.Proxy.Json
{
    public class ImageJSON
    {
        public int ImageId { get; set; }
        public byte[] UserImage { get; set; }
    }
}