using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Recepcao
{
    [WebServiceBinding(Name = "NfeRetRecepcao2Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetRecepcao2")]
    public class NfeRetRecepcao2 : SoapHttpClientProtocol, INfeServico
    {
        public NfeRetRecepcao2(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetRecepcao2")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetRecepcao2/nfeRetRecepcao2", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRetRecepcao2")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetRecepcao2")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetRecepcao2")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRetRecepcao2", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}