using CTe.CTeOSDocumento.Common;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Status
{
    public class NfeStatusServico2 : INfeServico
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        public NfeStatusServico2(string url, X509Certificate certificado, int timeOut)
        {
            configuracao = new WsdlConfiguracao()
            {
                Url = url,
                CertificadoDigital = new X509Certificate2(certificado),
                TimeOut = timeOut
            };
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            soapEnvelope = new SoapEnvelope
            {
                head = new ResponseHead<nfeCabecMsg>
                {
                    nfeCabecMsg = nfeCabecMsg
                }
            };

            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                nfeDadosMsg = nfeDadosMsg
            };

            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao,
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2/nfeStatusServicoNF2",
                responseElementName: "nfeStatusServicoNF2Result").FirstChild;
        }

        /// <summary>
        /// Classe base para a serialização no formato do envelope SOAP.
        /// </summary>
        [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public class SoapEnvelope : CommonSoapEnvelope
        {
            [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseHead<nfeCabecMsg> head { get; set; }

            [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseBody<XmlNode> body { get; set; }
        }

        /// <summary>
        /// Classe para o cabeçalho do Envelope SOAP
        /// </summary>
        public class ResponseHead<T> : CommonResponseHead
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")]
            public T nfeCabecMsg { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP
        /// </summary>
        public class ResponseBody<T> : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")]
            public T nfeDadosMsg { get; set; }
        }
    }
}
