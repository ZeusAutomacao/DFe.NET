using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;
using NFe.Classes.Servicos.DistribuicaoDFe;
using NFe.Wsdl.Autorizacao;
using NFe.Wsdl.ConsultaProtocolo;

namespace NFe.Wsdl.DistribuicaoDFe
{
    public class NfeDistDFeInteresse : INfeServico
    {
        //Envelope SOAP para 
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;


        public NfeDistDFeInteresse(string url, X509Certificate certificado, int timeOut)
        {
            configuracao = new WsdlConfiguracao()
            {
                Url = url,
                CertificadoDigital = new X509Certificate2(certificado),
                TimeOut = timeOut,
            };
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            soapEnvelope = new SoapEnvelope()
            {
            };

            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                nfeDistDFeInteresse = new ResponseDadosMsg<XmlNode>()
                {
                    nfeDadosMsg = nfeDadosMsg
                }
            }; 

            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao,
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe/nfeDistDFeInteresse",
                responseElementName: "nfeDistDFeInteresseResult"
            ).FirstChild;

        }
        /// <summary>
        /// Classe base para a serialização no formato do envelope SOAP.
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
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
            public ResponseDadosMsg<T> nfeDistDFeInteresse { get; set; }
        }
         
        public class ResponseDadosMsg<T> : CommonResponseBody
        {
            public T nfeDadosMsg { get; set; }
        }
    }
}
