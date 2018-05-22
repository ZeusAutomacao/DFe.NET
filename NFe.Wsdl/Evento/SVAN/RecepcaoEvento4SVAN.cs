using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Evento.SVAN
{

    public class RecepcaoEvento4SVAN : NFeRecepcaoEvento4SoapClient, INfeServico
    {
        public RecepcaoEvento4SVAN(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeRecepcaoEventoAsync(nfeDadosMsg).Result;
            return result.nfeRecepcaoEventoResult;
        }

    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRecepcaoEventoSVANRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeRecepcaoEventoSVANRequest()
        {
        }

        public nfeRecepcaoEventoSVANRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRecepcaoEventoSVANResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public System.Xml.XmlNode nfeRecepcaoEventoResult;

        public nfeRecepcaoEventoSVANResponse()
        {
        }

        public nfeRecepcaoEventoSVANResponse(System.Xml.XmlNode nfeRecepcaoEventoResult)
        {
            this.nfeRecepcaoEventoResult = nfeRecepcaoEventoResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", ConfigurationName = "NFeRecepcaoEvento4Soap")]
    public interface NFeRecepcaoEvento4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeRecepcaoEventoSVANResponse> nfeRecepcaoEventoAsync(nfeRecepcaoEventoSVANRequest request);
    }

    public partial class NFeRecepcaoEvento4SoapClient : SoapBindingClient<NFeRecepcaoEvento4Soap>
    {
        public NFeRecepcaoEvento4SoapClient(string endpointAddressUri) :
            base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeRecepcaoEventoSVANResponse> nfeRecepcaoEventoAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeRecepcaoEventoSVANRequest inValue = new nfeRecepcaoEventoSVANRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRecepcaoEventoAsync(inValue);
        }

    }
}

