﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.34209 版自动生成。
// 
#pragma warning disable 1591

namespace NewAsiaOASystem.Web.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTestDataTableOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDkxbyUIdjsonOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCountDkxbyUidOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsEPDsModelOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertProductOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::NewAsiaOASystem.Web.Properties.Settings.Default.NewAsiaOASystem_Web_localhost_WebService;
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
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event GetTestDataTableCompletedEventHandler GetTestDataTableCompleted;
        
        /// <remarks/>
        public event GetDkxbyUIdjsonCompletedEventHandler GetDkxbyUIdjsonCompleted;
        
        /// <remarks/>
        public event GetCountDkxbyUidCompletedEventHandler GetCountDkxbyUidCompleted;
        
        /// <remarks/>
        public event InsEPDsModelCompletedEventHandler InsEPDsModelCompleted;
        
        /// <remarks/>
        public event InsertProductCompletedEventHandler InsertProductCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTestDataTable", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTestDataTable() {
            object[] results = this.Invoke("GetTestDataTable", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTestDataTableAsync() {
            this.GetTestDataTableAsync(null);
        }
        
        /// <remarks/>
        public void GetTestDataTableAsync(object userState) {
            if ((this.GetTestDataTableOperationCompleted == null)) {
                this.GetTestDataTableOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTestDataTableOperationCompleted);
            }
            this.InvokeAsync("GetTestDataTable", new object[0], this.GetTestDataTableOperationCompleted, userState);
        }
        
        private void OnGetTestDataTableOperationCompleted(object arg) {
            if ((this.GetTestDataTableCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTestDataTableCompleted(this, new GetTestDataTableCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDkxbyUIdjson", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetDkxbyUIdjson(string uid, int SetFrist, int SetMax) {
            object[] results = this.Invoke("GetDkxbyUIdjson", new object[] {
                        uid,
                        SetFrist,
                        SetMax});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDkxbyUIdjsonAsync(string uid, int SetFrist, int SetMax) {
            this.GetDkxbyUIdjsonAsync(uid, SetFrist, SetMax, null);
        }
        
        /// <remarks/>
        public void GetDkxbyUIdjsonAsync(string uid, int SetFrist, int SetMax, object userState) {
            if ((this.GetDkxbyUIdjsonOperationCompleted == null)) {
                this.GetDkxbyUIdjsonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDkxbyUIdjsonOperationCompleted);
            }
            this.InvokeAsync("GetDkxbyUIdjson", new object[] {
                        uid,
                        SetFrist,
                        SetMax}, this.GetDkxbyUIdjsonOperationCompleted, userState);
        }
        
        private void OnGetDkxbyUIdjsonOperationCompleted(object arg) {
            if ((this.GetDkxbyUIdjsonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDkxbyUIdjsonCompleted(this, new GetDkxbyUIdjsonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCountDkxbyUid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetCountDkxbyUid(string uid) {
            object[] results = this.Invoke("GetCountDkxbyUid", new object[] {
                        uid});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetCountDkxbyUidAsync(string uid) {
            this.GetCountDkxbyUidAsync(uid, null);
        }
        
        /// <remarks/>
        public void GetCountDkxbyUidAsync(string uid, object userState) {
            if ((this.GetCountDkxbyUidOperationCompleted == null)) {
                this.GetCountDkxbyUidOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCountDkxbyUidOperationCompleted);
            }
            this.InvokeAsync("GetCountDkxbyUid", new object[] {
                        uid}, this.GetCountDkxbyUidOperationCompleted, userState);
        }
        
        private void OnGetCountDkxbyUidOperationCompleted(object arg) {
            if ((this.GetCountDkxbyUidCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCountDkxbyUidCompleted(this, new GetCountDkxbyUidCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsEPDsModel", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsEPDsModel(string DdId, string Jjname, string kdgs) {
            object[] results = this.Invoke("InsEPDsModel", new object[] {
                        DdId,
                        Jjname,
                        kdgs});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void InsEPDsModelAsync(string DdId, string Jjname, string kdgs) {
            this.InsEPDsModelAsync(DdId, Jjname, kdgs, null);
        }
        
        /// <remarks/>
        public void InsEPDsModelAsync(string DdId, string Jjname, string kdgs, object userState) {
            if ((this.InsEPDsModelOperationCompleted == null)) {
                this.InsEPDsModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsEPDsModelOperationCompleted);
            }
            this.InvokeAsync("InsEPDsModel", new object[] {
                        DdId,
                        Jjname,
                        kdgs}, this.InsEPDsModelOperationCompleted, userState);
        }
        
        private void OnInsEPDsModelOperationCompleted(object arg) {
            if ((this.InsEPDsModelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsEPDsModelCompleted(this, new InsEPDsModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertProduct", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InsertProduct(string Pname, string P_bianhao) {
            this.Invoke("InsertProduct", new object[] {
                        Pname,
                        P_bianhao});
        }
        
        /// <remarks/>
        public void InsertProductAsync(string Pname, string P_bianhao) {
            this.InsertProductAsync(Pname, P_bianhao, null);
        }
        
        /// <remarks/>
        public void InsertProductAsync(string Pname, string P_bianhao, object userState) {
            if ((this.InsertProductOperationCompleted == null)) {
                this.InsertProductOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertProductOperationCompleted);
            }
            this.InvokeAsync("InsertProduct", new object[] {
                        Pname,
                        P_bianhao}, this.InsertProductOperationCompleted, userState);
        }
        
        private void OnInsertProductOperationCompleted(object arg) {
            if ((this.InsertProductCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertProductCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void GetTestDataTableCompletedEventHandler(object sender, GetTestDataTableCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTestDataTableCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTestDataTableCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void GetDkxbyUIdjsonCompletedEventHandler(object sender, GetDkxbyUIdjsonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDkxbyUIdjsonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDkxbyUIdjsonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void GetCountDkxbyUidCompletedEventHandler(object sender, GetCountDkxbyUidCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCountDkxbyUidCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCountDkxbyUidCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void InsEPDsModelCompletedEventHandler(object sender, InsEPDsModelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsEPDsModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsEPDsModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void InsertProductCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591