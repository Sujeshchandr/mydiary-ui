using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyDiary.WCF.Json
{
    [DataContract]
    public class ImageJSON
    {
        [DataMember]
        public int ImageId { get; set; }
        [DataMember]
        public byte[] UserImage { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Length { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string UserGuid { get; set; }
        [DataMember]
        public int UploadedImageId { get; set; }
    }
}