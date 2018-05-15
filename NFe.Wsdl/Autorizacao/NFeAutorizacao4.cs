using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
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
            var result = base.nfeAutorizacaoLoteAsync(nfeDadosMsg).Result;
            return result.nfeResultMsg;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            var result = base.nfeAutorizacaoLoteZipAsync(nfeDadosMsgZip).Result;
            return result.nfeResultMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeAutorizacao4LoteRequest()
        {
        }

        public nfeAutorizacao4LoteRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeAutorizacao4LoteResponse()
        {
        }

        public nfeAutorizacao4LoteResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteZipRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public string nfeDadosMsgZip;

        public nfeAutorizacao4LoteZipRequest()
        {
        }

        public nfeAutorizacao4LoteZipRequest(string nfeDadosMsgZip)
        {
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeAutorizacao4LoteZipResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeAutorizacao4LoteZipResponse()
        {
        }

        public nfeAutorizacao4LoteZipResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", ConfigurationName = "NFeAutorizacao4Soap12")]
    public interface NFeAutorizacao4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeAutorizacao4LoteResponse> nfeAutorizacaoLoteAsync(nfeAutorizacao4LoteRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZip", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeAutorizacao4LoteZipResponse> nfeAutorizacaoLoteZipAsync(nfeAutorizacao4LoteZipRequest request);
    }

    public partial class NFeAutorizacao4Soap12Client : SoapBindingClient<NFeAutorizacao4Soap12>
    {

        public NFeAutorizacao4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeAutorizacao4LoteResponse> nfeAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeAutorizacao4LoteRequest inValue = new nfeAutorizacao4LoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeAutorizacaoLoteAsync(inValue);
        }

        public System.Threading.Tasks.Task<nfeAutorizacao4LoteZipResponse> nfeAutorizacaoLoteZipAsync(string nfeDadosMsgZip)
        {
            nfeAutorizacao4LoteZipRequest inValue = new nfeAutorizacao4LoteZipRequest();
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            return this.Channel.nfeAutorizacaoLoteZipAsync(inValue);
        }
    }
}