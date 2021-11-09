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

namespace BritishMicro.TaskClerk.WebRegistration {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="RegistrationServiceSoap", Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")]
    public partial class RegistrationService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CreateRegistrationOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerifyRegistrationOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public RegistrationService() {
            this.Url = "http://www.blogitup.com/BritishMicro/RegistrationService.asmx";
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
        public event CreateRegistrationCompletedEventHandler CreateRegistrationCompleted;
        
        /// <remarks/>
        public event VerifyRegistrationCompletedEventHandler VerifyRegistrationCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.britishmicro.com/2006/03/RegistrationService/CreateRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("registrationResponse", Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")]
        public RegistrationResponse CreateRegistration([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")] RegistrationRequest registrationRequest) {
            object[] results = this.Invoke("CreateRegistration", new object[] {
                        registrationRequest});
            return ((RegistrationResponse)(results[0]));
        }
        
        /// <remarks/>
        public void CreateRegistrationAsync(RegistrationRequest registrationRequest) {
            this.CreateRegistrationAsync(registrationRequest, null);
        }
        
        /// <remarks/>
        public void CreateRegistrationAsync(RegistrationRequest registrationRequest, object userState) {
            if ((this.CreateRegistrationOperationCompleted == null)) {
                this.CreateRegistrationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateRegistrationOperationCompleted);
            }
            this.InvokeAsync("CreateRegistration", new object[] {
                        registrationRequest}, this.CreateRegistrationOperationCompleted, userState);
        }
        
        private void OnCreateRegistrationOperationCompleted(object arg) {
            if ((this.CreateRegistrationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateRegistrationCompleted(this, new CreateRegistrationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.britishmicro.com/2006/03/RegistrationService/VerifyRegistration", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("verifyResponse", Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")]
        public RegistrationResponse VerifyRegistration([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")] RegistrationRequest verifyRequest) {
            object[] results = this.Invoke("VerifyRegistration", new object[] {
                        verifyRequest});
            return ((RegistrationResponse)(results[0]));
        }
        
        /// <remarks/>
        public void VerifyRegistrationAsync(RegistrationRequest verifyRequest) {
            this.VerifyRegistrationAsync(verifyRequest, null);
        }
        
        /// <remarks/>
        public void VerifyRegistrationAsync(RegistrationRequest verifyRequest, object userState) {
            if ((this.VerifyRegistrationOperationCompleted == null)) {
                this.VerifyRegistrationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerifyRegistrationOperationCompleted);
            }
            this.InvokeAsync("VerifyRegistration", new object[] {
                        verifyRequest}, this.VerifyRegistrationOperationCompleted, userState);
        }
        
        private void OnVerifyRegistrationOperationCompleted(object arg) {
            if ((this.VerifyRegistrationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerifyRegistrationCompleted(this, new VerifyRegistrationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")]
    public partial class RegistrationRequest {
        
        private string organisationField;
        
        private string systemUserField;
        
        private string nameField;
        
        private string emailAddressField;
        
        private string countryField;
        
        private string languageField;
        
        private string applicationTypeField;
        
        private string versionIdField;
        
        private string appInstanceKeyField;
        
        private string appInstanceCreateDateField;
        
        private string registrationKeyField;
        
        /// <remarks/>
        public string Organisation {
            get {
                return this.organisationField;
            }
            set {
                this.organisationField = value;
            }
        }
        
        /// <remarks/>
        public string SystemUser {
            get {
                return this.systemUserField;
            }
            set {
                this.systemUserField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string EmailAddress {
            get {
                return this.emailAddressField;
            }
            set {
                this.emailAddressField = value;
            }
        }
        
        /// <remarks/>
        public string Country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }
        
        /// <remarks/>
        public string Language {
            get {
                return this.languageField;
            }
            set {
                this.languageField = value;
            }
        }
        
        /// <remarks/>
        public string ApplicationType {
            get {
                return this.applicationTypeField;
            }
            set {
                this.applicationTypeField = value;
            }
        }
        
        /// <remarks/>
        public string VersionId {
            get {
                return this.versionIdField;
            }
            set {
                this.versionIdField = value;
            }
        }
        
        /// <remarks/>
        public string AppInstanceKey {
            get {
                return this.appInstanceKeyField;
            }
            set {
                this.appInstanceKeyField = value;
            }
        }
        
        /// <remarks/>
        public string AppInstanceCreateDate {
            get {
                return this.appInstanceCreateDateField;
            }
            set {
                this.appInstanceCreateDateField = value;
            }
        }
        
        /// <remarks/>
        public string RegistrationKey {
            get {
                return this.registrationKeyField;
            }
            set {
                this.registrationKeyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.britishmicro.com/2006/03/RegistrationService/")]
    public partial class RegistrationResponse {
        
        private string successField;
        
        private string failReasonField;
        
        /// <remarks/>
        public string Success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
            }
        }
        
        /// <remarks/>
        public string FailReason {
            get {
                return this.failReasonField;
            }
            set {
                this.failReasonField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void CreateRegistrationCompletedEventHandler(object sender, CreateRegistrationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateRegistrationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateRegistrationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public RegistrationResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((RegistrationResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void VerifyRegistrationCompletedEventHandler(object sender, VerifyRegistrationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerifyRegistrationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerifyRegistrationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public RegistrationResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((RegistrationResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591