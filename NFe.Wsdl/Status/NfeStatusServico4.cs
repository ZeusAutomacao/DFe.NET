using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Status
{
    [WebServiceBinding(Name = "NFeStatusServico4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
    public class NfeStatusServico4 : SoapHttpClientProtocol, INfeServico
    {
        public NfeStatusServico4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
        [WebMethod(MessageName = "nfeStatusServicoNF")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeStatusServicoNF", new object[] { nfeDadosMsg });
            return (XmlNode)(results[0]);
        }
    }
}