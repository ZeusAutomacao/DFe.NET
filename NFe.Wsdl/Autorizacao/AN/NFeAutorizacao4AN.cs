using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao.AN
{
    [WebServiceBinding(Name = "NFeAutorizacao4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
    public class NFeAutorizacao4AN : SoapHttpClientProtocol, INfeServicoAutorizacao
    {
        public NFeAutorizacao4AN(string url, X509Certificate certificado, int timeOut)
        {
            SoapVersion = SoapProtocolVersion.Soap12;
            Url = url;
            Timeout = timeOut;
            ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }


        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeAutorizacaoLote")]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")] XmlNode nfeDadosMsg)
        {
            var results = Invoke("nfeAutorizacaoLote", new object[] { nfeDadosMsg });
            return ((XmlNode)(results[0]));
        }


        [SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZIP", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        [WebMethod(MessageName = "nfeAutorizacaoLoteZIP")]
        [return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
        public XmlNode ExecuteZip([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")] string nfeDadosMsgZip)
        {
            var results = Invoke("nfeAutorizacaoLoteZIP", new object[] { nfeDadosMsgZip });
            return ((XmlNode)(results[0]));
        }
    }
}
