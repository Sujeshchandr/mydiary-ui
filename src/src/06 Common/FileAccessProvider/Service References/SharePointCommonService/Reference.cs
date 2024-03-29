﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyDiary.FileAccessProvider.SharePointCommonService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SharePointCommonService.ISharePointCommonService")]
    public interface ISharePointCommonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/GetFileById", ReplyAction="http://tempuri.org/ISharePointCommonService/GetFileByIdResponse")]
        int GetFileById(int fileId, int version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/GetFileById", ReplyAction="http://tempuri.org/ISharePointCommonService/GetFileByIdResponse")]
        System.Threading.Tasks.Task<int> GetFileByIdAsync(int fileId, int version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/GetFileByFileName", ReplyAction="http://tempuri.org/ISharePointCommonService/GetFileByFileNameResponse")]
        string GetFileByFileName(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/GetFileByFileName", ReplyAction="http://tempuri.org/ISharePointCommonService/GetFileByFileNameResponse")]
        System.Threading.Tasks.Task<string> GetFileByFileNameAsync(string fileName);
        
        // CODEGEN: Generating message contract since the wrapper name (UploadData) of message UploadData does not match the default value (UploadAsStream)
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/UploadAsStream", ReplyAction="http://tempuri.org/ISharePointCommonService/UploadAsStreamResponse")]
        MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse UploadAsStream(MyDiary.FileAccessProvider.SharePointCommonService.UploadData request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/UploadAsStream", ReplyAction="http://tempuri.org/ISharePointCommonService/UploadAsStreamResponse")]
        System.Threading.Tasks.Task<MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse> UploadAsStreamAsync(MyDiary.FileAccessProvider.SharePointCommonService.UploadData request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/UploadAsByteArray", ReplyAction="http://tempuri.org/ISharePointCommonService/UploadAsByteArrayResponse")]
        bool UploadAsByteArray(byte[] bytes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointCommonService/UploadAsByteArray", ReplyAction="http://tempuri.org/ISharePointCommonService/UploadAsByteArrayResponse")]
        System.Threading.Tasks.Task<bool> UploadAsByteArrayAsync(byte[] bytes);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadData {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream Stream;
        
        public UploadData() {
        }
        
        public UploadData(System.IO.Stream Stream) {
            this.Stream = Stream;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool result;
        
        public UploadResponse() {
        }
        
        public UploadResponse(bool result) {
            this.result = result;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISharePointCommonServiceChannel : MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SharePointCommonServiceClient : System.ServiceModel.ClientBase<MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService>, MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService {
        
        public SharePointCommonServiceClient() {
        }
        
        public SharePointCommonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SharePointCommonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SharePointCommonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SharePointCommonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetFileById(int fileId, int version) {
            return base.Channel.GetFileById(fileId, version);
        }
        
        public System.Threading.Tasks.Task<int> GetFileByIdAsync(int fileId, int version) {
            return base.Channel.GetFileByIdAsync(fileId, version);
        }
        
        public string GetFileByFileName(string fileName) {
            return base.Channel.GetFileByFileName(fileName);
        }
        
        public System.Threading.Tasks.Task<string> GetFileByFileNameAsync(string fileName) {
            return base.Channel.GetFileByFileNameAsync(fileName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService.UploadAsStream(MyDiary.FileAccessProvider.SharePointCommonService.UploadData request) {
            return base.Channel.UploadAsStream(request);
        }
        
        public bool UploadAsStream(System.IO.Stream Stream) {
            MyDiary.FileAccessProvider.SharePointCommonService.UploadData inValue = new MyDiary.FileAccessProvider.SharePointCommonService.UploadData();
            inValue.Stream = Stream;
            MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse retVal = ((MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService)(this)).UploadAsStream(inValue);
            return retVal.result;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse> MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService.UploadAsStreamAsync(MyDiary.FileAccessProvider.SharePointCommonService.UploadData request) {
            return base.Channel.UploadAsStreamAsync(request);
        }
        
        public System.Threading.Tasks.Task<MyDiary.FileAccessProvider.SharePointCommonService.UploadResponse> UploadAsStreamAsync(System.IO.Stream Stream) {
            MyDiary.FileAccessProvider.SharePointCommonService.UploadData inValue = new MyDiary.FileAccessProvider.SharePointCommonService.UploadData();
            inValue.Stream = Stream;
            return ((MyDiary.FileAccessProvider.SharePointCommonService.ISharePointCommonService)(this)).UploadAsStreamAsync(inValue);
        }
        
        public bool UploadAsByteArray(byte[] bytes) {
            return base.Channel.UploadAsByteArray(bytes);
        }
        
        public System.Threading.Tasks.Task<bool> UploadAsByteArrayAsync(byte[] bytes) {
            return base.Channel.UploadAsByteArrayAsync(bytes);
        }
    }
}
