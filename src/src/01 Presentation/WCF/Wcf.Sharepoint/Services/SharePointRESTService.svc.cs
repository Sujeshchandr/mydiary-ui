using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyDIARY.Common.WCF.Sharepoint.Contracts;

namespace MyDIARY.Common.WCF.Sharepoint.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SharePointRESTService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SharePointRESTService.svc or SharePointRESTService.svc.cs at the Solution Explorer and start debugging.
    public class SharePointRESTService : ISharePointRESTService
    {
        public int GetFile(int fileId, int version = 0)
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
        public bool Upload(Stream stream)
        {
            return true;
        }

        public bool GetFile(byte[] bytes)
        {
            return false;
        }

    }
}
