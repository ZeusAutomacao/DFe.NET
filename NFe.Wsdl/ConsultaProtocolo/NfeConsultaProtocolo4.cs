using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaProtocolo
{
    [WebServiceBinding(Name = "NFeConsultaProtocolo4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4")]
    public class NfeConsultaProtocolo4 : SoapHttpClientProtocol, INfeServico
    {
        public NfeConsultaProtocolo4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeConsultaNF")]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeConsultaNF", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }
    }
}