using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace CTeDLL.Wsdl.Inutilizacao
{
    [WebServiceBinding(Name = "CteInutilizacaoSoap12", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao")]
    public class CteInutilizacao : SoapHttpClientProtocol, ICteServico
    {
        public CteInutilizacao(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao/cteInutilizacaoCT", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "cteInutilizacaoCT")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao")] XmlNode cteCabecMsg)
        {
            var results = Invoke("cteInutilizacaoCT", new object[] { cteCabecMsg });
            return ((XmlNode)(results[0]));
        }
    }
}
