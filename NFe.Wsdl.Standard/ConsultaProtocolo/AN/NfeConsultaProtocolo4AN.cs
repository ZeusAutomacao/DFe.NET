﻿using CTe.CTeOSDocumento.Common;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.ConsultaProtocolo.AN
{
    public class NfeConsultaProtocolo4AN : INfeServico
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private readonly WsdlConfiguracao configuracao;

        public NfeConsultaProtocolo4AN(string url, X509Certificate certificado, int timeOut)
        {
            configuracao = new WsdlConfiguracao()
            {
                Url = url,
                CertificadoDigital = new X509Certificate2(certificado),
                TimeOut = timeOut
            };
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            soapEnvelope = new SoapEnvelope()
            {
            };

            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                nfeDadosMsg = nfeDadosMsg
            };

            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao,
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF",
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
            public ResponseBody<XmlNode> body { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP
        /// </summary>
        public class ResponseBody<T> : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4")]
            public T nfeDadosMsg { get; set; }
        }
    }
}
