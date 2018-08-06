using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Evento.AN
{
    [WebServiceBinding(Name = "NFeRecepcaoEvento4Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")]
    public class RecepcaoEventoManifestacaoDestinatario4AN : SoapHttpClientProtocol, INfeServico
    {
        public RecepcaoEventoManifestacaoDestinatario4AN(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEventoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeRecepcaoEventoNF")]
        [return: XmlElement("nfeRecepcaoEventoNFResult", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeRecepcaoEventoNF", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }
    }
}