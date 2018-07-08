using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MyDiary.WCF.Json;

namespace MyDiary.WCF.Services.Abstract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFaceBookImageService" in both code and config file together.
    [ServiceContract]
    public interface IFaceBookImageService
    {
        [OperationContract(Name = "Save")]
        [WebInvoke(Method = "POST", UriTemplate = "Save", BodyStyle = WebMessageBodyStyle.Bare)]
        ImageJSON Save(FacebookJSON facebookJson);
    }
}
