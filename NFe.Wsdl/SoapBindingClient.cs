using NFe.Wsdl.Autorizacao;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace NFe.Wsdl
{
    public class SoapBindingClient<T> : System.ServiceModel.ClientBase<T> where T : class, IChannel
    {
        public SoapBindingClient(string endpointAddressUri) :

            base(
                 new CustomBinding(new TextMessageEncodingBindingElement(MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None), Encoding.UTF8),
                     new HttpsTransportBindingElement { RequireClientCertificate = true }),
                 new EndpointAddress(endpointAddressUri)
                 )
        {
        }

    }
}
