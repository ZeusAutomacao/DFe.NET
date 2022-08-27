using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeRetAutorizacaoLoteAsync(nfeDadosMsg);
            return result.nfeRetAutorizacaoLoteResult;
        }
    }


    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeRetAutorizacaoSVANLoteRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeRetAutorizacaoSVANLoteRequest()
        {
        }

        public nfeRetAutorizacaoSVANLoteRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeRetAutorizacaoSVANLoteResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public XmlNode nfeRetAutorizacaoLoteResult;

        public nfeRetAutorizacaoSVANLoteResponse()
        {
        }

        public nfeRetAutorizacaoSVANLoteResponse(XmlNode nfeRetAutorizacaoLoteResult)
        {
            this.nfeRetAutorizacaoLoteResult = nfeRetAutorizacaoLoteResult;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", ConfigurationName = "NFeRetAutorizacao4Soap")]
    public interface NFeRetAutorizacao4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacaoLote", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeRetAutorizacaoSVANLoteResponse> nfeRetAutorizacaoLoteAsync(nfeRetAutorizacaoSVANLoteRequest request);
    }

    public partial class NFeRetAutorizacao4SoapClient : SoapBindingClient<NFeRetAutorizacao4Soap>
    {
        public NFeRetAutorizacao4SoapClient(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeRetAutorizacaoSVANLoteResponse> nfeRetAutorizacaoLoteAsync(XmlNode nfeDadosMsg)
        {
            nfeRetAutorizacaoSVANLoteRequest inValue = new nfeRetAutorizacaoSVANLoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRetAutorizacaoLoteAsync(inValue);
        }

    }

}
