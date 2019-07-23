using DFe.DocumentosEletronicos.Common;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Status
{
    public class NfeStatusServico4 : INfeServico
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        public NfeStatusServico4(string url, X509Certificate certificado, int timeOut)
        {
            this.configuracao = new WsdlConfiguracao()
            {
                 Url = url,
                 CertificadoDigital = new X509Certificate2(certificado),
                 TimeOut = timeOut
            };

            soapEnvelope = new SoapEnvelope()
            {
            };
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                 nfeDadosMsg = nfeDadosMsg
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, "nfeResultMsg", actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF");
        }
    }

    /// <summary>
    /// Classe base para a serealização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelope : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
        public T nfeDadosMsg { get; set; }
    }
}
