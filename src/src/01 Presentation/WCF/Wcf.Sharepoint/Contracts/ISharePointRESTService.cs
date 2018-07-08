using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyDIARY.Common.WCF.Sharepoint.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISharePointRESTService" in both code and config file together.
    [ServiceContract]
    public interface ISharePointRESTService
    {
        [OperationContract(Name = "GetFileById")]
        [WebInvoke(Method = "GET", UriTemplate = "GetFile/?fileId={fileId}&version={version}", BodyStyle = WebMessageBodyStyle.Bare)]
        int GetFile(int fileId, int version = 0);

        [OperationContract(Name = "GetFileByFileName")]
        [WebInvoke(Method = "GET", UriTemplate = "GetFile/{fileName}", BodyStyle = WebMessageBodyStyle.Bare)]
        string GetFile(string fileName);

        [OperationContract(Name = "UploadAsStream")]
        //[DataContractFormat]
        [WebInvoke(Method = "POST", UriTemplate = "Upload/stream", BodyStyle = WebMessageBodyStyle.Bare)]
        bool Upload(Stream stream);

        [OperationContract(Name = "UploadAsByteArray")]
        [WebInvoke(Method = "POST", UriTemplate = "Upload/bytes", BodyStyle = WebMessageBodyStyle.Bare)]
        bool GetFile(byte[] bytes);
    }
}
