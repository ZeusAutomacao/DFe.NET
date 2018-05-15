using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Autorizacao.SVAN
{

    public class NfeRetAutorizacao4SVAN : NFeRetAutorizacao4SoapClient, INfeServico
    {

        public NfeRetAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeRetAutorizacaoLoteAsync(nfeDadosMsg).Result;
            return result.nfeRetAutorizacaoLoteResult;
        }

    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacaoSVANLoteRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeRetAutorizacaoSVANLoteRequest()
        {
        }

        public nfeRetAutorizacaoSVANLoteRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacaoSVANLoteResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeRetAutorizacaoLoteResult;

        public nfeRetAutorizacaoSVANLoteResponse()
        {
        }

        public nfeRetAutorizacaoSVANLoteResponse(System.Xml.XmlNode nfeRetAutorizacaoLoteResult)
        {
            this.nfeRetAutorizacaoLoteResult = nfeRetAutorizacaoLoteResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", ConfigurationName = "NFeRetAutorizacao4Soap")]
    public interface NFeRetAutorizacao4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacaoLote", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeRetAutorizacaoSVANLoteResponse> nfeRetAutorizacaoLoteAsync(nfeRetAutorizacaoSVANLoteRequest request);
    }

    public partial class NFeRetAutorizacao4SoapClient : SoapBindingClient<NFeRetAutorizacao4Soap>
    {
        public NFeRetAutorizacao4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeRetAutorizacaoSVANLoteResponse> nfeRetAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeRetAutorizacaoSVANLoteRequest inValue = new nfeRetAutorizacaoSVANLoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRetAutorizacaoLoteAsync(inValue);
        }

    }

}
