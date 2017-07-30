using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using System.Xml;

namespace DFe.NFe.Wsdl.Status
{
    [System.Web.Services.WebServiceBindingAttribute(Name = "NFeStatusServico4Service",
        Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
    public class NFeStatusServico4 : System.Web.Services.Protocols.SoapHttpClientProtocol, INfeServico
    {

        public NFeStatusServico4(string url, X509Certificate2 certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        /// <summary>
        /// Não é utilizado na NF-e 4.0
        /// </summary>
        [Obsolete("Não é mais utilizado na nf-e 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }


        [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
            "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF",
            Use = System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return:
            System.Xml.Serialization.XmlElementAttribute("nfeResultMsg", Namespace =
                "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
        [System.Web.Services.WebMethod(MessageName = "nfeStatusServicoNF")]
        public XmlNode Execute([System.Xml.Serialization.XmlElementAttribute(Namespace =
            "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")] XmlNode nfeDadosMsg)
        {
            var results = this.Invoke("nfeStatusServicoNF", new object[]
            {
                nfeDadosMsg
            });
            return (XmlNode)(results[0]);
        }
    }
}