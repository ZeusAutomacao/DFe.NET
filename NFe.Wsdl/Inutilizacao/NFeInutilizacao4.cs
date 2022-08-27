using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeInutilizacao4NFAsync(nfeDadosMsg);
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacao4NFRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeInutilizacao4NFRequest()
        {
        }

        public nfeInutilizacao4NFRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeInutilizacao4NFResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeInutilizacao4NFResponse()
        {
        }

        public nfeInutilizacao4NFResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", ConfigurationName = "NFeInutilizacao4Soap12")]
    public interface NFeInutilizacao4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeInutilizacao4NFResponse> nfeInutilizacao4NFAsync(nfeInutilizacao4NFRequest request);
    }

    public partial class NFeInutilizacao4Soap12Client : SoapBindingClient<NFeInutilizacao4Soap12>
    {
        public NFeInutilizacao4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeInutilizacao4NFResponse> nfeInutilizacao4NFAsync(XmlNode nfeDadosMsg)
        {
            nfeInutilizacao4NFRequest inValue = new nfeInutilizacao4NFRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeInutilizacao4NFAsync(inValue);
        }

    }

}