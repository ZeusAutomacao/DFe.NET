using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.Evento
{
    [WebServiceBinding(Name = "CteRecepcaoEventoSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento")]
    public class RecepcaoEvento : SoapHttpClientProtocol, ICteServico
    {
        public RecepcaoEvento(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento/cteRecepcaoEvento", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteRecepcaoEvento")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteRecepcaoEvento", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}