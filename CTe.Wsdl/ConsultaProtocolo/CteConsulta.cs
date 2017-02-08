using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.ConsultaProtocolo
{
    [WebServiceBinding(Name = "CteConsultaSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
    public class CteConsulta : SoapHttpClientProtocol, ICteServico
    {
        public CteConsulta(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta/cteConsultaCT", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteConsultaCT")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteConsultaCT", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}