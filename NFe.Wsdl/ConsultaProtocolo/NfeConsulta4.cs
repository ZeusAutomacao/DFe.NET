using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.ConsultaProtocolo
{

    public class NfeConsulta4 : NFeConsultaProtocolo4Soap12Client, INfeServico
    {
        public NfeConsulta4(string url, X509Certificate certificado, int timeOut) : base(url)
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
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeConsulta4NFRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeConsulta4NFRequest()
        {
        }

        public nfeConsulta4NFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeConsulta4NFResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeConsulta4NFResponse()
        {
        }

        public nfeConsulta4NFResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", ConfigurationName = "NFeConsultaProtocolo4Soap12")]
    public interface NFeConsultaProtocolo4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeConsulta4NFResponse> nfeConsultaNFAsync(nfeConsulta4NFRequest request);
    }

    public partial class NFeConsultaProtocolo4Soap12Client : SoapBindingClient<NFeConsultaProtocolo4Soap12>
    {
        public NFeConsultaProtocolo4Soap12Client(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public Task<nfeConsulta4NFResponse> nfeConsultaNFAsync(XmlNode nfeDadosMsg)
        {
            nfeConsulta4NFRequest inValue = new nfeConsulta4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeConsultaNFAsync(inValue);
        }
    }

}