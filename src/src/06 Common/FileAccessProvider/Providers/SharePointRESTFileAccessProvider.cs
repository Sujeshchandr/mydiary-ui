using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.FileAccessProvider.Providers
{
   public class SharePointRESTFileAccessProvider :IFileAccess
    {

        public int GetFile(int fileId)
        {
            WebClient client = new WebClient();
            client.Headers.Add("content-type", "application/json");
            string url ="http://localhost:64986/Services/SharePointRESTService.svc/GetFile?fileId="+fileId;
            string result = client.DownloadString(url);
            return int.Parse(result);
        }

        public int GetFile(int fileId, int version)
        {
            throw new NotImplementedException();
        }

        public string GetFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Upload(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool Upload(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
