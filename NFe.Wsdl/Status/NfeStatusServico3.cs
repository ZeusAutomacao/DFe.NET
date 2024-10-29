using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Status
{
    [WebServiceBinding(Name = "NfeStatusServicoSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico3")]
    public class NfeStatusServico3 : SoapHttpClientProtocol, INfeServico
    {
        public NfeStatusServico3(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico3")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico3/nfeStatusServicoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeStatusServicoNF")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico3")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico3")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeStatusServicoNF", new object[] {nfeDadosMsg});
            return (XmlNode) (results[0]);
        }
    }
}