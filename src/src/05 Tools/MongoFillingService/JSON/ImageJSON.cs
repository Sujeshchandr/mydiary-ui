﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.MongoFillingService
{
    [Serializable]
    public class ImageJSON
    {
        public int ImageId { get; set; }
        public byte[] UserImage { get; set; }
    }
}