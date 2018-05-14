using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Status.SVAN
{
    public class NfeStatusServico4NFeSVAN : NFeStatusServico4SoapClient, INfeServico
    {
        public NfeStatusServico4NFeSVAN(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeStatusServicoNFAsync(nfeDadosMsg).Result;
            return result.nfeStatusServicoNFResult;
        }
        
    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeStatusServicoSVANNFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeStatusServicoSVANNFRequest()
        {
        }

        public nfeStatusServicoSVANNFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeStatusServicoSVANNFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public System.Xml.XmlNode nfeStatusServicoNFResult;

        public nfeStatusServicoSVANNFResponse()
        {
        }

        public nfeStatusServicoSVANNFResponse(System.Xml.XmlNode nfeStatusServicoNFResult)
        {
            this.nfeStatusServicoNFResult = nfeStatusServicoNFResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", ConfigurationName = "NFeStatusServico4Soap")]
    public interface NFeStatusServico4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeStatusServicoSVANNFResponse> nfeStatusServicoNFAsync(nfeStatusServicoSVANNFRequest request);
    }

    public partial class NFeStatusServico4SoapClient : SoapBindingClient<NFeStatusServico4Soap>
    {

        public NFeStatusServico4SoapClient(string endpointAddressUri) :
            base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeStatusServicoSVANNFResponse> nfeStatusServicoNFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeStatusServicoSVANNFRequest inValue = new nfeStatusServicoSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeStatusServicoNFAsync(inValue);
        }

    }
}