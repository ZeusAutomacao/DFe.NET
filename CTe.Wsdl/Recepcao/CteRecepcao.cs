using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.Recepcao
{
    [WebServiceBinding(Name = "CteRecepcaoSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao")]
    public class CteRecepcao : SoapHttpClientProtocol, ICteServico
    {
        public CteRecepcao(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao/cteRecepcaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteRecepcaoLote")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteRecepcaoLote", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}