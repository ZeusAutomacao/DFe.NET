using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaProtocolo
{
    [WebServiceBinding(Name = "NfeConsultaSoap", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")]
    public class NfeConsultaProtocolo : SoapHttpClientProtocol, INfeServico
    {
        public NfeConsultaProtocolo(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap11;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta/nfeConsultaNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeConsultaNF")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeConsultaNF", new object[] {nfeDadosMsg});
            return ((XmlNode) (results[0]));
        }
    }
}