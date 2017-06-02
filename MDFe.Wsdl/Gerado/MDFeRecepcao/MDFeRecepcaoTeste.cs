using System.Net;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using MDFe.Wsdl.Configuracao;

namespace MDFe.Wsdl.Gerado.MDFeRecepcao
{
    [WebServiceBinding(Name = "MDFeConsNaoEncSoap12", Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")]
    public class MDFeRecepcaoTeste : SoapHttpClientProtocol
    {
        public MDFeRecepcaoTeste(WsdlConfiguracao wsdlConfiguracao)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = wsdlConfiguracao.Url;
            mdfeCabecMsg = new Teste.mdfeCabecMsg
            {
                cUF = wsdlConfiguracao.CodigoIbgeEstado,
                versaoDados = wsdlConfiguracao.Versao
            };
            Timeout = wsdlConfiguracao.TimeOut;
            ClientCertificates.Add(wsdlConfiguracao.CertificadoDigital);
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")]
        public Teste.mdfeCabecMsg mdfeCabecMsg { get; set; }

        [SoapHeader("mdfeCabecMsg", Direction = SoapHeaderDirection.InOut)]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "mdfeConsNaoEnc")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc")] XmlNode mdfeDadosMsg)
        {
            var results = Invoke("mdfeConsNaoEnc", new object[] { mdfeDadosMsg });
            return ((XmlNode)(results[0]));
        }
    }
}