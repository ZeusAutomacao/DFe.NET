using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Recepcao
{
    [WebServiceBinding(Name = "NfeRecepcao2Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2")]
    public class NfeRecepcao2 : SoapHttpClientProtocol, INfeServico
    {
        public NfeRecepcao2(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2/nfeRecepcaoLote2", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRecepcaoLote2")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRecepcao2")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRecepcaoLote2", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}