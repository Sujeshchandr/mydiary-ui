using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyDiary.WCF.ServiceHelpers
{
    public static class ImageSericeHelper
    {
        public static byte[] Get(Stream stream)
        {
            byte[] data;
            using (Stream inputStream = stream)
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
    }
}