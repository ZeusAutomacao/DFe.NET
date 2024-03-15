using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
#if DEBUG
using System.Diagnostics;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
#endif

namespace NFe.Wsdl
{
    public class SoapBindingClient<T> : ClientBase<T> where T : class, IChannel
    {
        public SoapBindingClient(string endpointAddressUri, string proxyAddress = null)
            : base(GetCustomBidingWithProxy(proxyAddress), new EndpointAddress(endpointAddressUri))
        {
#if DEBUG
            Endpoint.EndpointBehaviors.Add(new SimpleEndpointBehavior());
#endif
        }

        private static Binding GetCustomBidingWithProxy(string proxyAddress)
        {
            var textMessage = new TextMessageEncodingBindingElement(
                    MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None), Encoding.UTF8);

            var httpsTransport = new HttpsTransportBindingElement
            {
                RequireClientCertificate = true,
                MaxReceivedMessageSize = int.MaxValue
            };

            if (proxyAddress != null)
            {
                var proxyParts = proxyAddress.Split(':');

                httpsTransport.BypassProxyOnLocal = false;
                httpsTransport.ProxyAddress = new Uri("http://" + proxyParts.GetValue(0) + ":" + proxyParts.GetValue(1));
                httpsTransport.MaxBufferSize = int.MaxValue;
                httpsTransport.UseDefaultWebProxy = false;
                httpsTransport.ProxyAuthenticationScheme = System.Net.AuthenticationSchemes.Basic;
            }

            return new CustomBinding(textMessage, httpsTransport);
        }

#if DEBUG
        public class SimpleMessageInspector : IClientMessageInspector
        {
            public object BeforeSendRequest(ref Message request, IClientChannel channel)
            {
                Debug.WriteLine("SOAP request: " + request);
                return null;
            }

            public void AfterReceiveReply(ref Message reply, object correlationState)
            {
                Debug.WriteLine("SOAP response: " + reply);
            }
        }

        public class SimpleEndpointBehavior : IEndpointBehavior
        {
            public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
            {
                // No implementation necessary  
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                clientRuntime.ClientMessageInspectors.Add(new SimpleMessageInspector());
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            {
                // No implementation necessary  
            }

            public void Validate(ServiceEndpoint endpoint)
            {
                // No implementation necessary  
            }
        }  
#endif
    }
}