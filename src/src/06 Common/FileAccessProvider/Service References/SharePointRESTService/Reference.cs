﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyDiary.FileAccessProvider.SharePointRESTService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SharePointRESTService.ISharePointRESTService")]
    public interface ISharePointRESTService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/GetFileById", ReplyAction="http://tempuri.org/ISharePointRESTService/GetFileByIdResponse")]
        int GetFileById(int fileId, int version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/GetFileById", ReplyAction="http://tempuri.org/ISharePointRESTService/GetFileByIdResponse")]
        System.Threading.Tasks.Task<int> GetFileByIdAsync(int fileId, int version);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/GetFileByFileName", ReplyAction="http://tempuri.org/ISharePointRESTService/GetFileByFileNameResponse")]
        string GetFileByFileName(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/GetFileByFileName", ReplyAction="http://tempuri.org/ISharePointRESTService/GetFileByFileNameResponse")]
        System.Threading.Tasks.Task<string> GetFileByFileNameAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/UploadAsStream", ReplyAction="http://tempuri.org/ISharePointRESTService/UploadAsStreamResponse")]
        bool UploadAsStream(System.IO.Stream stream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/UploadAsStream", ReplyAction="http://tempuri.org/ISharePointRESTService/UploadAsStreamResponse")]
        System.Threading.Tasks.Task<bool> UploadAsStreamAsync(System.IO.Stream stream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/UploadAsByteArray", ReplyAction="http://tempuri.org/ISharePointRESTService/UploadAsByteArrayResponse")]
        bool UploadAsByteArray(byte[] bytes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISharePointRESTService/UploadAsByteArray", ReplyAction="http://tempuri.org/ISharePointRESTService/UploadAsByteArrayResponse")]
        System.Threading.Tasks.Task<bool> UploadAsByteArrayAsync(byte[] bytes);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISharePointRESTServiceChannel : MyDiary.FileAccessProvider.SharePointRESTService.ISharePointRESTService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SharePointRESTServiceClient : System.ServiceModel.ClientBase<MyDiary.FileAccessProvider.SharePointRESTService.ISharePointRESTService>, MyDiary.FileAccessProvider.SharePointRESTService.ISharePointRESTService {
        
        public SharePointRESTServiceClient() {
        }
        
        public SharePointRESTServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SharePointRESTServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SharePointRESTServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SharePointRESTServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public bool UploadAsStream(System.IO.Stream stream) {
            return base.Channel.UploadAsStream(stream);
        }
        
        public System.Threading.Tasks.Task<bool> UploadAsStreamAsync(System.IO.Stream stream) {
            return base.Channel.UploadAsStreamAsync(stream);
        }
        
        public bool UploadAsByteArray(byte[] bytes) {
            return base.Channel.UploadAsByteArray(bytes);
        }
        
        public System.Threading.Tasks.Task<bool> UploadAsByteArrayAsync(byte[] bytes) {
            return base.Channel.UploadAsByteArrayAsync(bytes);
        }
    }
}