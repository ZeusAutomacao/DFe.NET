using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    [WebServiceBinding(Name = "NfeRetAutorizacaoSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")]
    public class NfeRetAutorizacao3 : SoapHttpClientProtocol, INfeServico
    {
        public NfeRetAutorizacao3(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3/NfeRetAutorizacaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRetAutorizacao")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRetAutorizacao", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}