using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Status
{
    [WebServiceBinding(Name = "NfeStatusServicoSoap", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico")]
    public class NfeStatusServico : SoapHttpClientProtocol, INfeServico
    {
        public NfeStatusServico(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap11;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico/nfeStatusServicoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeStatusServicoNF")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeStatusServicoNF", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}