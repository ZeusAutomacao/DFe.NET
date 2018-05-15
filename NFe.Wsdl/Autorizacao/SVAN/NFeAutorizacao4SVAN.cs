using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Autorizacao.SVAN
{

    public class NFeAutorizacao4SVAN : NFeAutorizacao4SoapClient, INfeServicoAutorizacao
    {
        public NFeAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeAutorizacaoLoteAsync(nfeDadosMsg).Result;
            return result.nfeAutorizacaoLoteResult;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            var result = base.nfeAutorizacaoLoteZipAsync(nfeDadosMsgZip).Result;
            return result.nfeAutorizacaoLoteZipResult;
        }

    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacaoSVANLoteRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeAutorizacaoSVANLoteRequest()
        {
        }

        public nfeAutorizacaoSVANLoteRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacaoSVANLoteResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeAutorizacaoLoteResult;

        public nfeAutorizacaoSVANLoteResponse()
        {
        }

        public nfeAutorizacaoSVANLoteResponse(System.Xml.XmlNode nfeAutorizacaoLoteResult)
        {
            this.nfeAutorizacaoLoteResult = nfeAutorizacaoLoteResult;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacaoSVANLoteZipRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public string nfeDadosMsgZip;

        public nfeAutorizacaoSVANLoteZipRequest()
        {
        }

        public nfeAutorizacaoSVANLoteZipRequest(string nfeDadosMsgZip)
        {
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacaoSVANLoteZipResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeAutorizacaoLoteZipResult;

        public nfeAutorizacaoSVANLoteZipResponse()
        {
        }

        public nfeAutorizacaoSVANLoteZipResponse(System.Xml.XmlNode nfeAutorizacaoLoteZipResult)
        {
            this.nfeAutorizacaoLoteZipResult = nfeAutorizacaoLoteZipResult;
        }
    }

    public interface NFeAutorizacao4Soap : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeAutorizacaoSVANLoteResponse> nfeAutorizacaoLoteAsync(nfeAutorizacaoSVANLoteRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZip", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeAutorizacaoSVANLoteZipResponse> nfeAutorizacaoLoteZipAsync(nfeAutorizacaoSVANLoteZipRequest request);
    }

    public partial class NFeAutorizacao4SoapClient : SoapBindingClient<NFeAutorizacao4Soap>
    {
        public NFeAutorizacao4SoapClient(string endpointAddressUri) :
                   base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeAutorizacaoSVANLoteResponse> nfeAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeAutorizacaoSVANLoteRequest inValue = new nfeAutorizacaoSVANLoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeAutorizacaoLoteAsync(inValue);
        }

        public System.Threading.Tasks.Task<nfeAutorizacaoSVANLoteZipResponse> nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip)
        {
            nfeAutorizacaoSVANLoteZipRequest inValue = new nfeAutorizacaoSVANLoteZipRequest();
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            return this.Channel.nfeAutorizacaoLoteZipAsync(inValue);
        }
    }


}