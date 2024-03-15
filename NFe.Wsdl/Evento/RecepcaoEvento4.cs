using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Evento
{
    public class RecepcaoEvento4 : NFeRecepcaoEvento4Soap12Client, INfeServico
    {
        public RecepcaoEvento4(string url, X509Certificate certificado, int timeOut) : base(url)
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
            nfeRecepcaoEvento4Response result = await nfeRecepcaoEventoAsync(nfeDadosMsg);
            return result.nfeResultMsg ?? result.nfeRecepcaoEventoNFResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRecepcaoEvento4Request
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public XmlNode nfeDadosMsg;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRecepcaoEvento4Response
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")]
        public XmlNode nfeResultMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")]
        public XmlNode nfeRecepcaoEventoNFResult;
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4",
        ConfigurationName = "NFeRecepcaoEvento4Soap12")]
    public interface NFeRecepcaoEvento4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(nfeRecepcaoEvento4Request request);
    }

    public class NFeRecepcaoEvento4Soap12Client : SoapBindingClient<NFeRecepcaoEvento4Soap12>
    {
        public NFeRecepcaoEvento4Soap12Client(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeRecepcaoEvento4Request { nfeDadosMsg = nfeDadosMsg };
            return Channel.nfeRecepcaoEventoAsync(inValue);
        }
    }
}