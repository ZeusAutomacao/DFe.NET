using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    [WebServiceBinding(Name = "NfeAutorizacaoSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
    public class NfeAutorizacao : SoapHttpClientProtocol, INfeServicoAutorizacao
    {
        public NfeAutorizacao(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeAutorizacaoLote")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeAutorizacaoLote", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLoteZip", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeAutorizacaoLoteZip")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public XmlNode ExecuteZip([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")] string nfeDadosMsgZip)
        {
            var results = Invoke("nfeAutorizacaoLoteZip", new object[] {nfeDadosMsgZip});
            return ((XmlNode) (results[0]));
        }
    }
}