using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Status
{

    public class NfeStatusServico4 : NFeStatusServico4Soap12Client, INfeServico
    {

        public NfeStatusServico4(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }
        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeStatusServicoNFAsync(nfeDadosMsg).GetAwaiter().GetResult();
            return result.nfeResultMsg;
        }
    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeStatusServico4NFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeStatusServico4NFRequest()
        {
        }

        public nfeStatusServico4NFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeStatusServico4NFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeStatusServico4NFResponse()
        {
        }

        public nfeStatusServico4NFResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", ConfigurationName = "NFeStatusServico4Soap12")]
    public interface NFeStatusServico4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeStatusServico4NFResponse> nfeStatusServicoNFAsync(nfeStatusServico4NFRequest request);
    }

    public partial class NFeStatusServico4Soap12Client : SoapBindingClient<NFeStatusServico4Soap12>
    {
        public NFeStatusServico4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeStatusServico4NFResponse> nfeStatusServicoNFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeStatusServico4NFRequest inValue = new nfeStatusServico4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeStatusServicoNFAsync(inValue);
        }

    }

}