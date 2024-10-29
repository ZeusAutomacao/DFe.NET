using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaCadastro.CE
{
    [WebServiceBinding(Name = "CadConsultaCadastro2Soap12", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro2")]
    public class CadConsultaCadastro2 : SoapHttpClientProtocol, INfeServico
    {
        public CadConsultaCadastro2(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro2")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        [SoapHeader("nfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro2/consultaCadastro2", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare
            )]
        [WebMethod(MessageName = "consultaCadastro2")]
        [return: XmlElement("cadConsultaCadastro2Result", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro2")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro2")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("consultaCadastro2", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }
    }
}