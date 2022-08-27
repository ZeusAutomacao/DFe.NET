using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Status
{
    public class NfeStatusServico4 : NFeStatusServico4Soap12Client, INfeServico
    {
        public NfeStatusServico4(string url, X509Certificate certificado, int timeOut) : base(url)
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
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeStatusServico4NFRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeStatusServico4NFRequest() { }

        public nfeStatusServico4NFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeStatusServico4NFResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeStatusServico4NFResponse() { }

        public nfeStatusServico4NFResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4",
        ConfigurationName = "NFeStatusServico4Soap12")]
    public interface NFeStatusServico4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeStatusServico4NFResponse> nfeStatusServicoNFAsync(nfeStatusServico4NFRequest request);
    }

    public class NFeStatusServico4Soap12Client : SoapBindingClient<NFeStatusServico4Soap12>
    {
        public NFeStatusServico4Soap12Client(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeStatusServico4NFResponse> nfeStatusServicoNFAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeStatusServico4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return Channel.nfeStatusServicoNFAsync(inValue);
        }
    }
}