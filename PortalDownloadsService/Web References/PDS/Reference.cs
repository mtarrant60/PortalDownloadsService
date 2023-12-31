﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace PortalDownloadsService.PDS {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DownloadSoap", Namespace="http://tempuri.org/")]
    public partial class Download : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetClientUNCFolderOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFilesOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateDocumentFolderOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadFromSharePointOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadFileOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Download() {
            this.Url = global::PortalDownloadsService.Properties.Settings.Default.PrototypeDownloaWebService_PDS_Download;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetClientUNCFolderCompletedEventHandler GetClientUNCFolderCompleted;
        
        /// <remarks/>
        public event GetFilesCompletedEventHandler GetFilesCompleted;
        
        /// <remarks/>
        public event CreateDocumentFolderCompletedEventHandler CreateDocumentFolderCompleted;
        
        /// <remarks/>
        public event DownloadFromSharePointCompletedEventHandler DownloadFromSharePointCompleted;
        
        /// <remarks/>
        public event DownloadFileCompletedEventHandler DownloadFileCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetClientUNCFolder", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetClientUNCFolder(string legalCaseId) {
            object[] results = this.Invoke("GetClientUNCFolder", new object[] {
                        legalCaseId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetClientUNCFolderAsync(string legalCaseId) {
            this.GetClientUNCFolderAsync(legalCaseId, null);
        }
        
        /// <remarks/>
        public void GetClientUNCFolderAsync(string legalCaseId, object userState) {
            if ((this.GetClientUNCFolderOperationCompleted == null)) {
                this.GetClientUNCFolderOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetClientUNCFolderOperationCompleted);
            }
            this.InvokeAsync("GetClientUNCFolder", new object[] {
                        legalCaseId}, this.GetClientUNCFolderOperationCompleted, userState);
        }
        
        private void OnGetClientUNCFolderOperationCompleted(object arg) {
            if ((this.GetClientUNCFolderCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetClientUNCFolderCompleted(this, new GetClientUNCFolderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFiles", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PortalDoc[] GetFiles(string legalCaseId) {
            object[] results = this.Invoke("GetFiles", new object[] {
                        legalCaseId});
            return ((PortalDoc[])(results[0]));
        }
        
        /// <remarks/>
        public void GetFilesAsync(string legalCaseId) {
            this.GetFilesAsync(legalCaseId, null);
        }
        
        /// <remarks/>
        public void GetFilesAsync(string legalCaseId, object userState) {
            if ((this.GetFilesOperationCompleted == null)) {
                this.GetFilesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFilesOperationCompleted);
            }
            this.InvokeAsync("GetFiles", new object[] {
                        legalCaseId}, this.GetFilesOperationCompleted, userState);
        }
        
        private void OnGetFilesOperationCompleted(object arg) {
            if ((this.GetFilesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFilesCompleted(this, new GetFilesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateDocumentFolder", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateDocumentFolder(string legalCaseId) {
            object[] results = this.Invoke("CreateDocumentFolder", new object[] {
                        legalCaseId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateDocumentFolderAsync(string legalCaseId) {
            this.CreateDocumentFolderAsync(legalCaseId, null);
        }
        
        /// <remarks/>
        public void CreateDocumentFolderAsync(string legalCaseId, object userState) {
            if ((this.CreateDocumentFolderOperationCompleted == null)) {
                this.CreateDocumentFolderOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateDocumentFolderOperationCompleted);
            }
            this.InvokeAsync("CreateDocumentFolder", new object[] {
                        legalCaseId}, this.CreateDocumentFolderOperationCompleted, userState);
        }
        
        private void OnCreateDocumentFolderOperationCompleted(object arg) {
            if ((this.CreateDocumentFolderCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateDocumentFolderCompleted(this, new CreateDocumentFolderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadFromSharePoint", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] DownloadFromSharePoint(string file, string clientFolder) {
            object[] results = this.Invoke("DownloadFromSharePoint", new object[] {
                        file,
                        clientFolder});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void DownloadFromSharePointAsync(string file, string clientFolder) {
            this.DownloadFromSharePointAsync(file, clientFolder, null);
        }
        
        /// <remarks/>
        public void DownloadFromSharePointAsync(string file, string clientFolder, object userState) {
            if ((this.DownloadFromSharePointOperationCompleted == null)) {
                this.DownloadFromSharePointOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadFromSharePointOperationCompleted);
            }
            this.InvokeAsync("DownloadFromSharePoint", new object[] {
                        file,
                        clientFolder}, this.DownloadFromSharePointOperationCompleted, userState);
        }
        
        private void OnDownloadFromSharePointOperationCompleted(object arg) {
            if ((this.DownloadFromSharePointCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadFromSharePointCompleted(this, new DownloadFromSharePointCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] DownloadFile(string FName) {
            object[] results = this.Invoke("DownloadFile", new object[] {
                        FName});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void DownloadFileAsync(string FName) {
            this.DownloadFileAsync(FName, null);
        }
        
        /// <remarks/>
        public void DownloadFileAsync(string FName, object userState) {
            if ((this.DownloadFileOperationCompleted == null)) {
                this.DownloadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadFileOperationCompleted);
            }
            this.InvokeAsync("DownloadFile", new object[] {
                        FName}, this.DownloadFileOperationCompleted, userState);
        }
        
        private void OnDownloadFileOperationCompleted(object arg) {
            if ((this.DownloadFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadFileCompleted(this, new DownloadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class PortalDoc {
        
        private string fileField;
        
        private string clientFolderField;
        
        private string fileFullNameField;
        
        private string extensionField;
        
        private System.DateTime createdField;
        
        private long sizeField;
        
        /// <remarks/>
        public string File {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
            }
        }
        
        /// <remarks/>
        public string ClientFolder {
            get {
                return this.clientFolderField;
            }
            set {
                this.clientFolderField = value;
            }
        }
        
        /// <remarks/>
        public string FileFullName {
            get {
                return this.fileFullNameField;
            }
            set {
                this.fileFullNameField = value;
            }
        }
        
        /// <remarks/>
        public string Extension {
            get {
                return this.extensionField;
            }
            set {
                this.extensionField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime Created {
            get {
                return this.createdField;
            }
            set {
                this.createdField = value;
            }
        }
        
        /// <remarks/>
        public long Size {
            get {
                return this.sizeField;
            }
            set {
                this.sizeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetClientUNCFolderCompletedEventHandler(object sender, GetClientUNCFolderCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetClientUNCFolderCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetClientUNCFolderCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetFilesCompletedEventHandler(object sender, GetFilesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFilesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFilesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PortalDoc[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PortalDoc[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void CreateDocumentFolderCompletedEventHandler(object sender, CreateDocumentFolderCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateDocumentFolderCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateDocumentFolderCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void DownloadFromSharePointCompletedEventHandler(object sender, DownloadFromSharePointCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadFromSharePointCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadFromSharePointCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void DownloadFileCompletedEventHandler(object sender, DownloadFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591