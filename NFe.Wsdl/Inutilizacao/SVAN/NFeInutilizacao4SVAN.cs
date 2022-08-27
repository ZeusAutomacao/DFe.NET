using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Inutilizacao.SVAN
{

    public class NFeInutilizacao4SVAN : NFeInutilizacao4SoapClient, INfeServico
    {
        public NFeInutilizacao4SVAN(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeInutilizacaoNFAsync(nfeDadosMsg);
            return result.nfeInutilizacaoNFResult;
        }
    }

    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacaoSVANNFRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeInutilizacaoSVANNFRequest()
        {
        }

        public nfeInutilizacaoSVANNFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacaoSVANNFResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public XmlNode nfeInutilizacaoNFResult;

        public nfeInutilizacaoSVANNFResponse()
        {
        }

        public nfeInutilizacaoSVANNFResponse(XmlNode nfeInutilizacaoNFResult)
        {
            this.nfeInutilizacaoNFResult = nfeInutilizacaoNFResult;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", ConfigurationName = "NFeInutilizacao4Soap")]
    public interface NFeInutilizacao4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeInutilizacaoSVANNFResponse> nfeInutilizacaoNFAsync(nfeInutilizacaoSVANNFRequest request);
    }

    public partial class NFeInutilizacao4SoapClient : SoapBindingClient<NFeInutilizacao4Soap>
    {
        public NFeInutilizacao4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeInutilizacaoSVANNFResponse> nfeInutilizacaoNFAsync(XmlNode nfeDadosMsg)
        {
            nfeInutilizacaoSVANNFRequest inValue = new nfeInutilizacaoSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeInutilizacaoNFAsync(inValue);
        }
    }

    }