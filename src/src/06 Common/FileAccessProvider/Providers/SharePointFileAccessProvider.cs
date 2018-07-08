using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.FileAccessProvider.SharePointCommonService;
//using MyDiary.FileAccessProvider.SharePointCommonService;

namespace MyDiary.FileAccessProvider.Providers
{
    public class SharePointFileAccessProvider :IFileAccess
    {
        public int GetFile(int fileId)
        {
            int result;
            using (SharePointCommonService.SharePointCommonServiceClient proxy = new SharePointCommonServiceClient())
            {
                result = proxy.GetFileById(1,0);// version is optional value
            }
            return result;
        }

        public int GetFile(int fileId, int version)
        {
            int result;
            using (SharePointCommonService.SharePointCommonServiceClient proxy = new SharePointCommonServiceClient())
            {
                 result = proxy.GetFileById(1, version);
            }
            return result;
        }

        public string GetFile(string fileName)
        {
            string result;
            using (SharePointCommonService.SharePointCommonServiceClient proxy = new SharePointCommonServiceClient())
            {
                result = proxy.GetFileByFileName(fileName);
            }
            return result;

        }

        public bool Upload(Stream stream)
        {
            bool result;
            using (SharePointCommonService.SharePointCommonServiceClient proxy = new SharePointCommonServiceClient())
            {

                result = proxy.UploadAsStream(stream);
            }
            return result;
        }

        public bool Upload(byte[] bytes)
        {
            bool result;
            using (SharePointCommonService.SharePointCommonServiceClient proxy = new SharePointCommonServiceClient())
            {
                result = proxy.UploadAsByteArray(bytes);
            }
            return result;
        }
    }
}
