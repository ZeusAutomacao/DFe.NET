using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.DistribuicaoDFe
{
    [WebServiceBinding(Name = "NFeDistribuicaoDFeSoap", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
    public class NfeDistDFeInteresse : SoapHttpClientProtocol, INfeServico
    {
        public NfeDistDFeInteresse(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg")]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe/nfeDistDFeInteresse", RequestNamespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe", ResponseNamespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        [WebMethod(MessageName = "nfeDistDFeInteresse")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeDistDFeInteresse", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }

    }
}