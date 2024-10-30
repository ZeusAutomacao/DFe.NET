using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Download
{
    [WebServiceBinding(Name = "NfeDownloadNFSoap", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeDownloadNF")]
    public class NfeDownloadNF : SoapHttpClientProtocol, INfeServico
    {

        public NfeDownloadNF(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap11;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeDownloadNF")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg")]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeDownloadNF/nfeDownloadNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeDownloadNF")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeDownloadNF")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeDownloadNF")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeDownloadNF", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}