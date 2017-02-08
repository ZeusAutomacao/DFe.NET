using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.Recepcao
{
    [WebServiceBinding(Name = "CteRetRecepcaoSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao")]
    public class CteRetRecepcao : SoapHttpClientProtocol, ICteServico
    {
        public CteRetRecepcao(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao/cteRetRecepcao", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteRetRecepcao")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRetRecepcao")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteRetRecepcao", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}