using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Inutilizacao
{
    [WebServiceBinding(Name = "NfeInutilizacao2Soap", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao2")]
    public class NfeInutilizacao2 : SoapHttpClientProtocol, INfeServico
    {
        public NfeInutilizacao2(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao2")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao2/nfeInutilizacaoNF2", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeInutilizacaoNF2")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao2")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao2")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeInutilizacaoNF2", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}