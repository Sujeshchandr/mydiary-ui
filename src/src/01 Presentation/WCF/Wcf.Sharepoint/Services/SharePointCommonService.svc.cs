using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyDIARY.Common.WCF.Sharepoint.Contracts;

namespace MyDIARY.Common.WCF.Sharepoint.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ISharePointCommonService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ISharePointCommonService.svc or ISharePointCommonService.svc.cs at the Solution Explorer and start debugging.
    public class SharePointCommonService : ISharePointCommonService
    {
        //public int GetFile(int fileId)
        //{
        //    return 1;
        //}

        public int GetFile(int fileId, int version=0)
        {
            if (version > 0)
                return 2;
            else
                return 1;
        }
        public string GetFile(string fileName)
        {
            return "File Name";
        }
        public UploadResponse Upload(UploadData stream)
        {
           UploadResponse response = new UploadResponse();
           return response;
        }

        public bool GetFile(byte[] bytes)
        {
            return false;
        }

    }
}
