using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaCadastro.DEMAIS_UFs
{
    [WebServiceBinding(Name = "CadConsultaCadastro4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4")]
    public class CadConsultaCadastro4 : SoapHttpClientProtocol, INfeServico
    {
        public CadConsultaCadastro4(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4/consultaCadastro", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4")]
        [WebMethod(MessageName = "consultaCadastro")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("consultaCadastro", new object[] { nfeDadosMsg });
            return (XmlNode)(results[0]);
        }
    }
}