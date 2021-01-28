using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
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
            var result = base.nfeConsultaNFAsync(nfeDadosMsg).GetAwaiter().GetResult();
            return result.nfeResultMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsulta4NFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeConsulta4NFRequest()
        {
        }

        public nfeConsulta4NFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsulta4NFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeConsulta4NFResponse()
        {
        }

        public nfeConsulta4NFResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4", ConfigurationName = "NFeConsultaProtocolo4Soap12")]
    public interface NFeConsultaProtocolo4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeConsulta4NFResponse> nfeConsultaNFAsync(nfeConsulta4NFRequest request);
    }

    public partial class NFeConsultaProtocolo4Soap12Client : SoapBindingClient<NFeConsultaProtocolo4Soap12>
    {
        public NFeConsultaProtocolo4Soap12Client(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public System.Threading.Tasks.Task<nfeConsulta4NFResponse> nfeConsultaNFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeConsulta4NFRequest inValue = new nfeConsulta4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeConsultaNFAsync(inValue);
        }
    }

}