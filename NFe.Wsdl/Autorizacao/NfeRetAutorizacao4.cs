using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    [WebServiceBinding(Name = "NFeRetAutorizacao4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4")]
    public class NfeRetAutorizacao4 : SoapHttpClientProtocol, INfeServico
    {
        public NfeRetAutorizacao4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4/nfeRetAutorizacaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRetAutorizacaoLote")]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRetAutorizacao4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRetAutorizacaoLote", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }
    }
}