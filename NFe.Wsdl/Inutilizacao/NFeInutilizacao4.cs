using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Inutilizacao
{

    public class NFeInutilizacao4 : NFeInutilizacao4Soap12Client, INfeServico
    {

        public NFeInutilizacao4(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeInutilizacao4NFAsync(nfeDadosMsg).Result;
            return result.nfeResultMsg;
        }

    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeInutilizacao4NFRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeInutilizacao4NFRequest()
        {
        }

        public nfeInutilizacao4NFRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeInutilizacao4NFResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeInutilizacao4NFResponse()
        {
        }

        public nfeInutilizacao4NFResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", ConfigurationName = "NFeInutilizacao4Soap12")]
    public interface NFeInutilizacao4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeInutilizacao4NFResponse> nfeInutilizacao4NFAsync(nfeInutilizacao4NFRequest request);
    }

    public partial class NFeInutilizacao4Soap12Client : SoapBindingClient<NFeInutilizacao4Soap12>
    {
        public NFeInutilizacao4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeInutilizacao4NFResponse> nfeInutilizacao4NFAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeInutilizacao4NFRequest inValue = new nfeInutilizacao4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeInutilizacao4NFAsync(inValue);
        }

    }

}