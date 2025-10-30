using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Evento
{
    [WebServiceBinding(Name = "RecepcaoEPECSoap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
    public class RecepcaoEPEC : SoapHttpClientProtocol, INfeServico
    {
        public RecepcaoEPEC(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento/nfeRecepcaoEvento", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRecepcaoEvento")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRecepcaoEvento", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}