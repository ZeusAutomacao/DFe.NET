using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Evento
{

    public class RecepcaoEvento4 : NFeRecepcaoEvento4Soap12Client, INfeServico
    {
        public RecepcaoEvento4(string url, X509Certificate certificado, int timeOut) : base(url)
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
            var result = await nfeRecepcaoEventoAsync(nfeDadosMsg);
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeRecepcaoEvento4Request
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeRecepcaoEvento4Request()
        {
        }

        public nfeRecepcaoEvento4Request(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeRecepcaoEvento4Response
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeRecepcaoEvento4Response()
        {
        }

        public nfeRecepcaoEvento4Response(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", ConfigurationName = "NFeRecepcaoEvento4Soap12")]
    public interface NFeRecepcaoEvento4Soap12 : IChannel
    {

        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(nfeRecepcaoEvento4Request request);
    }

    public partial class NFeRecepcaoEvento4Soap12Client : SoapBindingClient<NFeRecepcaoEvento4Soap12>
    {

        public NFeRecepcaoEvento4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeRecepcaoEvento4Response> nfeRecepcaoEventoAsync(XmlNode nfeDadosMsg)
        {
            nfeRecepcaoEvento4Request inValue = new nfeRecepcaoEvento4Request();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRecepcaoEventoAsync(inValue);
        }

    }

}