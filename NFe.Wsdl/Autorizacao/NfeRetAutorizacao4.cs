using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.Autorizacao
{

    public class NfeRetAutorizacao4 : NFeRetAutorizacao4Soap12Client, INfeServico
    {
        public NfeRetAutorizacao4(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            //Timeout = timeOut;
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }
        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeRetAutorizacaoLoteAsync(nfeDadosMsg).Result;
            return result.nfeResultMsg; ;
        }

    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacao4LoteRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeRetAutorizacao4LoteRequest()
        {
        }

        public nfeRetAutorizacao4LoteRequest(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacao4LoteResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public nfeRetAutorizacao4LoteResponse()
        {
        }

        public nfeRetAutorizacao4LoteResponse(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4", ConfigurationName = "NFeRetAutorizacao4Soap12")]
    public interface NFeRetAutorizacao4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacaoLote", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeRetAutorizacao4LoteResponse> nfeRetAutorizacaoLoteAsync(nfeRetAutorizacao4LoteRequest request);
    }

    public partial class NFeRetAutorizacao4Soap12Client : SoapBindingClient<NFeRetAutorizacao4Soap12>
    {
        public NFeRetAutorizacao4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeRetAutorizacao4LoteResponse> nfeRetAutorizacaoLoteAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            nfeRetAutorizacao4LoteRequest inValue = new nfeRetAutorizacao4LoteRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.nfeRetAutorizacaoLoteAsync(inValue);
        }
    }
}