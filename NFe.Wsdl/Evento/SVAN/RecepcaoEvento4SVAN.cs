using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Evento.SVAN
{
    public class RecepcaoEvento4SVAN : NFeRecepcaoEvento4SoapClient, INfeServico
    {
        public RecepcaoEvento4SVAN(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeRecepcaoEventoAsync(nfeDadosMsg);
            return result.nfeRecepcaoEventoResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRecepcaoEventoSVANRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeRecepcaoEventoSVANRequest() { }

        public nfeRecepcaoEventoSVANRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRecepcaoEventoSVANResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public XmlNode nfeRecepcaoEventoResult;

        public nfeRecepcaoEventoSVANResponse() { }

        public nfeRecepcaoEventoSVANResponse(XmlNode nfeRecepcaoEventoResult)
        {
            this.nfeRecepcaoEventoResult = nfeRecepcaoEventoResult;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4",
        ConfigurationName = "NFeRecepcaoEvento4Soap")]
    public interface NFeRecepcaoEvento4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeRecepcaoEventoSVANResponse> nfeRecepcaoEventoAsync(nfeRecepcaoEventoSVANRequest request);
    }

    public class NFeRecepcaoEvento4SoapClient : SoapBindingClient<NFeRecepcaoEvento4Soap>
    {
        public NFeRecepcaoEvento4SoapClient(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeRecepcaoEventoSVANResponse> nfeRecepcaoEventoAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeRecepcaoEventoSVANRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return Channel.nfeRecepcaoEventoAsync(inValue);
        }
    }
}