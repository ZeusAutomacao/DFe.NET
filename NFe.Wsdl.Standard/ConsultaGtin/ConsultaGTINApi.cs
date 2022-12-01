using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace NFe.Wsdl.ConsultaGtin
{
    public class ConsultaGTINApi : INfeServico
    {
        public ConsultaGTINApi(string url, X509Certificate certificado, int timeOut)
        {
            configuracao = new WsdlConfiguracao()
            {
                Url = url,
                CertificadoDigital = new X509Certificate2(certificado),
                TimeOut = timeOut,
            };
        }

        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private readonly WsdlConfiguracao configuracao;

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            soapEnvelope = new SoapEnvelope();

            soapEnvelope.body = new ResponseBody
            {
                ccgConsGTIN = new ccgConsGTIN
                {
                    nfeDadosMsg = nfeDadosMsg
                }
            };

            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao,
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin/ccgConsGTIN",
                responseElementName: "nfeResultMsg"
            ).FirstChild;
        }

        /// <summary>
        /// Classe base para a serialização no formato do envelope SOAP.
        /// </summary>
        [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public class SoapEnvelope : CommonSoapEnvelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseBody body { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP
        /// </summary>
        public class ResponseBody : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/ccgConsGtin")]
            public ccgConsGTIN ccgConsGTIN { get; set; }
        }

        public class ccgConsGTIN
        {
            public XmlNode nfeDadosMsg { get; set; }
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }
    }
}