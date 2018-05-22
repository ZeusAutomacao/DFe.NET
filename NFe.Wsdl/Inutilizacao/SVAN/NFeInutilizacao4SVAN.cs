using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
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
            var result = base.nfeInutilizacaoNFAsync(nfeDadosMsg).Result;
            return result.nfeInutilizacaoNFResult;
        }

    }

    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeInutilizacaoSVANNFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeInutilizacaoSVANNFRequest()
        {
        }

        public nfeInutilizacaoSVANNFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeInutilizacaoSVANNFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public System.Xml.XmlNode nfeInutilizacaoNFResult;

        public nfeInutilizacaoSVANNFResponse()
        {
        }

        public nfeInutilizacaoSVANNFResponse(System.Xml.XmlNode nfeInutilizacaoNFResult)
        {
            this.nfeInutilizacaoNFResult = nfeInutilizacaoNFResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", ConfigurationName = "NFeInutilizacao4Soap")]
    public interface NFeInutilizacao4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeInutilizacaoSVANNFResponse> nfeInutilizacaoNFAsync(nfeInutilizacaoSVANNFRequest request);
    }

    public partial class NFeInutilizacao4SoapClient : SoapBindingClient<NFeInutilizacao4Soap>
    {
        public NFeInutilizacao4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeInutilizacaoSVANNFResponse> nfeInutilizacaoNFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeInutilizacaoSVANNFRequest inValue = new nfeInutilizacaoSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeInutilizacaoNFAsync(inValue);
        }
    }

    }