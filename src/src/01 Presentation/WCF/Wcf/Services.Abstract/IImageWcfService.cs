using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MyDiary.WCF.Json;

namespace MyDiary.WCF.Services.Abstract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IImageWcfService
    {
        [OperationContract(Name = "Get")]
        [WebGet(UriTemplate = "Get/{uploadImageId}")]
        Stream Get(string uploadImageId);

        [OperationContract(Name = "GetByUserId")]
        [WebGet(UriTemplate = "get/user/{userId}")]
        Stream GetByUserId(string userId);

        [OperationContract(Name = "Upload")]
        [DataContractFormat]
        [WebInvoke(Method = "POST", UriTemplate = "Upload",BodyStyle = WebMessageBodyStyle.Bare)]
        ImageJSON Upload();

      

    }
    
}
