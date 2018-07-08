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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IISharePointCommonService" in both code and config file together.
    [ServiceContract]
    public interface ISharePointCommonService
    {
        //[OperationContract(Name="GetFileById")]
        //[WebInvoke(Method = "GET", UriTemplate = "GetFile/?fileId={fileId}", BodyStyle = WebMessageBodyStyle.Bare)]
        //int GetFile(int fileId);

        [OperationContract(Name = "GetFileById")]
        [WebInvoke(Method = "GET", UriTemplate = "GetFile/?fileId={fileId}&version={version}", BodyStyle = WebMessageBodyStyle.Bare)]
        int GetFile(int fileId,int version=0);

        [OperationContract(Name = "GetFileByFileName")]
        [WebInvoke(Method = "GET", UriTemplate = "GetFile/{fileName}", BodyStyle = WebMessageBodyStyle.Bare)]      
        string GetFile(string fileName);

        [OperationContract(Name="UploadAsStream")]
        //[DataContractFormat]
        [WebInvoke(Method = "POST", UriTemplate = "Upload/stream", BodyStyle = WebMessageBodyStyle.Bare)]
        UploadResponse Upload(UploadData stream);

        [OperationContract(Name="UploadAsByteArray")]
        [WebInvoke(Method = "POST", UriTemplate = "Upload/bytes", BodyStyle = WebMessageBodyStyle.Bare)]
        bool GetFile(byte[] bytes);
    }

    [MessageContract]
    public class UploadResponse
    {
        [MessageBodyMember(Order = 1)]
        public bool result { get; set; }
    }

    [MessageContract]
    public class UploadData
    {
        [MessageBodyMember(Order = 1)]
        public Stream Stream { get; set; }
    }
}
