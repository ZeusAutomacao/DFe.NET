using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace NFe.Wsdl.Autorizacao
{
    public class NFeAutorizacao4 : NFeAutorizacao4Soap12Client, INfeServicoAutorizacao
    {
        public NFeAutorizacao4(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            //Timeout = timeOut;
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await nfeAutorizacaoLoteAsync(nfeDadosMsg);
            return result.nfeResultMsg;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            return this.ExecuteZipAsync(nfeDadosMsgZip).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteZipAsync(string nfeDadosMsgZip)
        {
            var result = await base.nfeAutorizacaoLoteZipAsync(nfeDadosMsgZip);
            return result.nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public nfeAutorizacao4LoteRequest()
        {
        }

        public nfeAutorizacao4LoteRequest(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public XmlNode nfeResultMsg;

        public nfeAutorizacao4LoteResponse()
        {
        }

        public nfeAutorizacao4LoteResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteZipRequest
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public string nfeDadosMsgZip;

        public nfeAutorizacao4LoteZipRequest()
        {
        }

        public nfeAutorizacao4LoteZipRequest(string nfeDadosMsgZip)
        {
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteZipResponse
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
        public XmlNode nfeResultMsg;

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
        public XmlNode nfeResultMsgZip;

        public nfeAutorizacao4LoteZipResponse()
        {
        }

        public nfeAutorizacao4LoteZipResponse(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
            this.nfeResultMsgZip = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", ConfigurationName = "NFeAutorizacao4Soap12")]
    public interface NFeAutorizacao4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeAutorizacao4LoteResponse> nfeAutorizacaoLoteAsync(nfeAutorizacao4LoteRequest request);

        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZip", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<nfeAutorizacao4LoteZipResponse> nfeAutorizacaoLoteZipAsync(nfeAutorizacao4LoteZipRequest request);
    }

    public partial class NFeAutorizacao4Soap12Client : SoapBindingClient<NFeAutorizacao4Soap12>
    {

        public NFeAutorizacao4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<nfeAutorizacao4LoteResponse> nfeAutorizacaoLoteAsync(XmlNode nfeDadosMsg)
        {
            nfeAutorizacao4LoteRequest inValue = new nfeAutorizacao4LoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeAutorizacaoLoteAsync(inValue);
        }

        public async Task<nfeAutorizacao4LoteZipResponse> nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip)
        {
            nfeAutorizacao4LoteZipRequest inValue = new nfeAutorizacao4LoteZipRequest();
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            var result = await this.Channel.nfeAutorizacaoLoteZipAsync(inValue);
            inValue = null;

            if (result.nfeResultMsg == null)
                result.nfeResultMsg = result.nfeResultMsgZip;

            return result;
        }
    }
}