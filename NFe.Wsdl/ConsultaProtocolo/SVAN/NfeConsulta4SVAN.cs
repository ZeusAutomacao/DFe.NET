using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.ConsultaProtocolo.SVAN
{

    public class NfeConsulta4SVAN : NFeConsultaProtocolo4SoapClient , INfeServico
    {
        public NfeConsulta4SVAN(string url, X509Certificate certificado, int timeOut)
           : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeConsultaNFAsync(nfeDadosMsg).Result;
            return result.nfeConsultaNFResult;
        }

    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsultaSVANNFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeConsultaSVANNFRequest()
        {
        }

        public nfeConsultaSVANNFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsultaSVANNFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public System.Xml.XmlNode nfeConsultaNFResult;

        public nfeConsultaSVANNFResponse()
        {
        }

        public nfeConsultaSVANNFResponse(System.Xml.XmlNode nfeConsultaNFResult)
        {
            this.nfeConsultaNFResult = nfeConsultaNFResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", ConfigurationName = "NFeConsultaProtocolo4Soap")]
    public interface NFeConsultaProtocolo4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeConsultaSVANNFResponse> nfeConsultaNFAsync(nfeConsultaSVANNFRequest request);
    }

    public partial class NFeConsultaProtocolo4SoapClient : SoapBindingClient<NFeConsultaProtocolo4Soap>
    {
        public NFeConsultaProtocolo4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeConsultaSVANNFResponse> nfeConsultaNFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeConsultaSVANNFRequest inValue = new nfeConsultaSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeConsultaNFAsync(inValue);
        }

    }

}