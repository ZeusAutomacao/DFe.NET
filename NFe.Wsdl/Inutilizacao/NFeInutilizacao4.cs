using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Inutilizacao
{
    [WebServiceBinding(Name = "NFeInutilizacao4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
    public class NFeInutilizacao4 : SoapHttpClientProtocol, INfeServico {

        public NFeInutilizacao4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
        [WebMethod(MessageName = "nfeInutilizacaoNF")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeInutilizacaoNF", new object[] { nfeDadosMsg });
            return (XmlNode)(results[0]);
        }
    }
}