using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Evento
{

    public class RecepcaoEvento4 : NFeRecepcaoEvento4Soap12Client, INfeServico
    {
        public RecepcaoEvento4(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeRecepcaoEventoAsync(nfeDadosMsg).Result;
            return result.nfeResultMsg;
        }

    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRecepcaoEvento4Request
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeRecepcaoEvento4Request()
        {
        }

        public nfeRecepcaoEvento4Request(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRecepcaoEvento4Response
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeRecepcaoEvento4Response()
        {
        }

        public nfeRecepcaoEvento4Response(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", ConfigurationName = "NFeRecepcaoEvento4Soap12")]
    public interface NFeRecepcaoEvento4Soap12 : IChannel
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(nfeRecepcaoEvento4Request request);
    }

    public partial class NFeRecepcaoEvento4Soap12Client : SoapBindingClient<NFeRecepcaoEvento4Soap12>
    {

        public NFeRecepcaoEvento4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeRecepcaoEvento4Request inValue = new nfeRecepcaoEvento4Request();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRecepcaoEventoAsync(inValue);
        }

    }

}