using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.AdmCsc
{
    [WebServiceBinding(Name = "CscNFCeBinding", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
    public class NfceCsc : SoapHttpClientProtocol, INfeServico
    {
         public NfceCsc(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

         [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
         public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe/admCscNFCe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "admCscNFCe")]
        [return: XmlElementAttribute("cscNFCeResult", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")]
        public XmlNode Execute([XmlElementAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CscNFCe")] XmlNode nfeDadosMsg)
         {
             var results = Invoke("admCscNFCe", new object[] { nfeDadosMsg });
             return ((XmlNode)(results[0]));
         }
    }
}