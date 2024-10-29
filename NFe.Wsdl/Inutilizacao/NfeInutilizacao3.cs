using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Inutilizacao
{
    [WebServiceBinding(Name = "NfeInutilizacaoSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")]
    public class NfeInutilizacao3 : SoapHttpClientProtocol, INfeServico
    {
        public NfeInutilizacao3(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3/nfeInutilizacaoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeInutilizacaoNF")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeInutilizacao3")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeInutilizacaoNF", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}