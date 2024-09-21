﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SamanBankVerify
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:Foo", ConfigurationName="SamanBankVerify.PaymentIFBindingSoap")]
    public interface PaymentIFBindingSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="verifyTransaction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        double verifyTransaction(string String_1, string String_2);
        
        [System.ServiceModel.OperationContractAttribute(Action="verifyTransaction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<double> verifyTransactionAsync(string String_1, string String_2);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        double verifyTransaction1(string String_1, string String_2);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<double> verifyTransaction1Async(string String_1, string String_2);
        
        [System.ServiceModel.OperationContractAttribute(Action="reverseTransaction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        double reverseTransaction(string String_1, string String_2, string Username, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="reverseTransaction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<double> reverseTransactionAsync(string String_1, string String_2, string Username, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="reverseTransaction1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        double reverseTransaction1(string String_1, string String_2, string Password, double Amount);
        
        [System.ServiceModel.OperationContractAttribute(Action="reverseTransaction1", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<double> reverseTransaction1Async(string String_1, string String_2, string Password, double Amount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface PaymentIFBindingSoapChannel : SamanBankVerify.PaymentIFBindingSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class PaymentIFBindingSoapClient : System.ServiceModel.ClientBase<SamanBankVerify.PaymentIFBindingSoap>, SamanBankVerify.PaymentIFBindingSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PaymentIFBindingSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(PaymentIFBindingSoapClient.GetBindingForEndpoint(endpointConfiguration), PaymentIFBindingSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentIFBindingSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PaymentIFBindingSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentIFBindingSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PaymentIFBindingSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentIFBindingSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public double verifyTransaction(string String_1, string String_2)
        {
            return base.Channel.verifyTransaction(String_1, String_2);
        }
        
        public System.Threading.Tasks.Task<double> verifyTransactionAsync(string String_1, string String_2)
        {
            return base.Channel.verifyTransactionAsync(String_1, String_2);
        }
        
        public double verifyTransaction1(string String_1, string String_2)
        {
            return base.Channel.verifyTransaction1(String_1, String_2);
        }
        
        public System.Threading.Tasks.Task<double> verifyTransaction1Async(string String_1, string String_2)
        {
            return base.Channel.verifyTransaction1Async(String_1, String_2);
        }
        
        public double reverseTransaction(string String_1, string String_2, string Username, string Password)
        {
            return base.Channel.reverseTransaction(String_1, String_2, Username, Password);
        }
        
        public System.Threading.Tasks.Task<double> reverseTransactionAsync(string String_1, string String_2, string Username, string Password)
        {
            return base.Channel.reverseTransactionAsync(String_1, String_2, Username, Password);
        }
        
        public double reverseTransaction1(string String_1, string String_2, string Password, double Amount)
        {
            return base.Channel.reverseTransaction1(String_1, String_2, Password, Amount);
        }
        
        public System.Threading.Tasks.Task<double> reverseTransaction1Async(string String_1, string String_2, string Password, double Amount)
        {
            return base.Channel.reverseTransaction1Async(String_1, String_2, Password, Amount);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentIFBindingSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.PaymentIFBindingSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentIFBindingSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://verify.sep.ir/payments/referencepayment.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.PaymentIFBindingSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://verify.sep.ir/payments/referencepayment.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            PaymentIFBindingSoap,
            
            PaymentIFBindingSoap12,
        }
    }
}
