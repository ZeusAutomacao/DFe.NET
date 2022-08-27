using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Autorizacao
{
    public class NfeRetAutorizacao4 : NFeRetAutorizacao4Soap12Client, INfeServico
    {
        public NfeRetAutorizacao4(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            //Timeout = timeOut;
            ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeRetAutorizacaoLoteAsync(nfeDadosMsg);
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRetAutorizacao4LoteRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeRetAutorizacao4LoteRequest() { }

        public nfeRetAutorizacao4LoteRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeRetAutorizacao4LoteResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeRetAutorizacao4LoteResponse() { }

        public nfeRetAutorizacao4LoteResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4",
        ConfigurationName = "NFeRetAutorizacao4Soap12")]
    public interface NFeRetAutorizacao4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacaoLote",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeRetAutorizacao4LoteResponse> nfeRetAutorizacaoLoteAsync(nfeRetAutorizacao4LoteRequest request);
    }

    public class NFeRetAutorizacao4Soap12Client : SoapBindingClient<NFeRetAutorizacao4Soap12>
    {
        public NFeRetAutorizacao4Soap12Client(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeRetAutorizacao4LoteResponse> nfeRetAutorizacaoLoteAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeRetAutorizacao4LoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return Channel.nfeRetAutorizacaoLoteAsync(inValue);
        }
    }
}