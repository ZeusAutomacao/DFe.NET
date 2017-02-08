using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.Status
{
    [WebServiceBinding(Name = "CteStatusServicoSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
    public class CteStatusServico : SoapHttpClientProtocol, ICteServico
    {
        public CteStatusServico(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico/cteStatusServicoCT", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteStatusServicoCT")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteStatusServicoCT", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}