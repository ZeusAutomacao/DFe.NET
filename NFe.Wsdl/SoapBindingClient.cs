using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace NFe.Wsdl
{
    public class SoapBindingClient<T> : ClientBase<T> where T : class, IChannel
    {
        public SoapBindingClient(string endpointAddressUri) : base(
            new CustomBinding(
                new TextMessageEncodingBindingElement(
                    MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None), Encoding.UTF8),
                new HttpsTransportBindingElement
                {
                    RequireClientCertificate = true, MaxReceivedMessageSize = int.MaxValue
                }), new EndpointAddress(endpointAddressUri)) { }

        public SoapBindingClient(string endpointAddressUri, string proxyAddress) : base(GetCustomBidingWithProxy(proxyAddress), new EndpointAddress(endpointAddressUri))
        { }

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
                var proxySplitted = proxyAddress.Split(':');

                httpsTransport.BypassProxyOnLocal = false;
                httpsTransport.ProxyAddress = new Uri($"http://{proxySplitted.GetValue(0)}:{proxySplitted.GetValue(1)}");
                httpsTransport.MaxBufferSize = int.MaxValue;
                httpsTransport.UseDefaultWebProxy = false;
                httpsTransport.ProxyAuthenticationScheme = System.Net.AuthenticationSchemes.Basic;
            }

            return new CustomBinding(textMessage, httpsTransport);
        }
    }
}