using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Status.SVAN
{
    public class NfeStatusServico4NFeSVAN : NFeStatusServico4SoapClient, INfeServico
    {
        public NfeStatusServico4NFeSVAN(string url, X509Certificate certificado, int timeOut) : base(url)
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
            var result = await nfeStatusServicoNFAsync(nfeDadosMsg);
            return result.nfeStatusServicoNFResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeStatusServicoSVANNFRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeStatusServicoSVANNFRequest() { }

        public nfeStatusServicoSVANNFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeStatusServicoSVANNFResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public XmlNode nfeStatusServicoNFResult;

        public nfeStatusServicoSVANNFResponse() { }

        public nfeStatusServicoSVANNFResponse(XmlNode nfeStatusServicoNFResult)
        {
            this.nfeStatusServicoNFResult = nfeStatusServicoNFResult;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4",
        ConfigurationName = "NFeStatusServico4Soap")]
    public interface NFeStatusServico4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeStatusServicoSVANNFResponse> nfeStatusServicoNFAsync(nfeStatusServicoSVANNFRequest request);
    }

    public class NFeStatusServico4SoapClient : SoapBindingClient<NFeStatusServico4Soap>
    {
        public NFeStatusServico4SoapClient(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeStatusServicoSVANNFResponse> nfeStatusServicoNFAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeStatusServicoSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return Channel.nfeStatusServicoNFAsync(inValue);
        }
    }
}