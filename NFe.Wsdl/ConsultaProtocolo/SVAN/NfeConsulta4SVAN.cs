using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeConsultaNFAsync(nfeDadosMsg);
            return result.nfeConsultaNFResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeConsultaSVANNFRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeConsultaSVANNFRequest()
        {
        }

        public nfeConsultaSVANNFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeConsultaSVANNFResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public XmlNode nfeConsultaNFResult;

        public nfeConsultaSVANNFResponse()
        {
        }

        public nfeConsultaSVANNFResponse(XmlNode nfeConsultaNFResult)
        {
            this.nfeConsultaNFResult = nfeConsultaNFResult;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", ConfigurationName = "NFeConsultaProtocolo4Soap")]
    public interface NFeConsultaProtocolo4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeConsultaSVANNFResponse> nfeConsultaNFAsync(nfeConsultaSVANNFRequest request);
    }

    public partial class NFeConsultaProtocolo4SoapClient : SoapBindingClient<NFeConsultaProtocolo4Soap>
    {
        public NFeConsultaProtocolo4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeConsultaSVANNFResponse> nfeConsultaNFAsync(XmlNode nfeDadosMsg)
        {
            nfeConsultaSVANNFRequest inValue = new nfeConsultaSVANNFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeConsultaNFAsync(inValue);
        }

    }

}