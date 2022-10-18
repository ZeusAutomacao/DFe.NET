using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaGtin
{
    [WebServiceBinding(Name = "ccgConsGTIN", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin")]
    public class ConsultaGTINApi : SoapHttpClientProtocol, INfeServico
    {
        public ConsultaGTINApi(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin/ccgConsGTIN",
            Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "ccgConsGTIN")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("ccgConsGTIN", new object[] { nfeDadosMsg });
            return (XmlNode)(results[0]);
        }
    }
}