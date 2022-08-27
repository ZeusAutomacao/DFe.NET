using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Autorizacao.SVAN
{
    public class NFeAutorizacao4SVAN : NFeAutorizacao4SoapClient, INfeServicoAutorizacao
    {
        public NFeAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeAutorizacaoLoteAsync(nfeDadosMsg);
            return result.nfeAutorizacaoLoteResult;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            return ExecuteZipAsync(nfeDadosMsgZip).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteZipAsync(string nfeDadosMsgZip)
        {
            var result = await nfeAutorizacaoLoteZipAsync(nfeDadosMsgZip);
            return result.nfeAutorizacaoLoteZipResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeAutorizacaoSVANLoteRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeAutorizacaoSVANLoteRequest() { }

        public nfeAutorizacaoSVANLoteRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeAutorizacaoSVANLoteResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public XmlNode nfeAutorizacaoLoteResult;

        public nfeAutorizacaoSVANLoteResponse() { }

        public nfeAutorizacaoSVANLoteResponse(XmlNode nfeAutorizacaoLoteResult)
        {
            this.nfeAutorizacaoLoteResult = nfeAutorizacaoLoteResult;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeAutorizacaoSVANLoteZipRequest
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public string nfeDadosMsgZip;

        public nfeAutorizacaoSVANLoteZipRequest() { }

        public nfeAutorizacaoSVANLoteZipRequest(string nfeDadosMsgZip)
        {
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public class nfeAutorizacaoSVANLoteZipResponse
    {
        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public XmlNode nfeAutorizacaoLoteZipResult;

        public nfeAutorizacaoSVANLoteZipResponse() { }

        public nfeAutorizacaoSVANLoteZipResponse(XmlNode nfeAutorizacaoLoteZipResult)
        {
            this.nfeAutorizacaoLoteZipResult = nfeAutorizacaoLoteZipResult;
        }
    }

    public interface NFeAutorizacao4Soap : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeAutorizacaoSVANLoteResponse> nfeAutorizacaoLoteAsync(nfeAutorizacaoSVANLoteRequest request);

        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZip",
            ReplyAction = "*")]
        [XmlSerializerFormat]
        Task<nfeAutorizacaoSVANLoteZipResponse> nfeAutorizacaoLoteZipAsync(nfeAutorizacaoSVANLoteZipRequest request);
    }

    public class NFeAutorizacao4SoapClient : SoapBindingClient<NFeAutorizacao4Soap>
    {
        public NFeAutorizacao4SoapClient(string endpointAddressUri) : base(endpointAddressUri) { }

        public Task<nfeAutorizacaoSVANLoteResponse> nfeAutorizacaoLoteAsync(XmlNode nfeDadosMsg)
        {
            var inValue = new nfeAutorizacaoSVANLoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return Channel.nfeAutorizacaoLoteAsync(inValue);
        }

        public Task<nfeAutorizacaoSVANLoteZipResponse> nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip)
        {
            var inValue = new nfeAutorizacaoSVANLoteZipRequest();
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            return Channel.nfeAutorizacaoLoteZipAsync(inValue);
        }
    }
}