using CTe.CTeOSDocumento.Common;
using DFe.Classes.Entidades;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    public class NFeAutorizacao4 : INfeServicoAutorizacao
    {
        //Envelope SOAP para 
        private SoapEnvelope soapEnvelope;
        private SoapEnvelopeZip soapEnvelopeZip;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        public NFeAutorizacao4(string url, X509Certificate certificado, int timeOut, bool compactarMensagem, DFe.Classes.Flags.VersaoServico versaoNfeAutorizacao, Estado estado)
        {
            configuracao = new WsdlConfiguracao()
            {
                Url = url,
                CertificadoDigital = new X509Certificate2(certificado),
                TimeOut = timeOut
            };
        }

        [Obsolete("Não utilizar na nfe 4.0. Não tem necessidade mais a partir da nf-e 4.0 o mesmo sera ignorado")]
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
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote",
                responseElementName: "nfeResultMsg"
                ).FirstChild;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            soapEnvelopeZip = new SoapEnvelopeZip()
            {
            };

            soapEnvelopeZip.body = new ResponseBodyZip
            {
                nfeDadosMsgZip = nfeDadosMsgZip
            };

            return RequestBuilderAndSender.Execute(soapEnvelopeZip, configuracao,
                actionUrn: "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZIP",
                responseElementName: "nfeResultMsg"
                ).FirstChild;
        }

        #region Envelope

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
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
            public T nfeDadosMsg { get; set; }
        }

        #endregion

        #region Envelope ZIP

        /// <summary>
        /// Classe base para a serialização no formato do envelope SOAP | zip
        /// </summary>
        [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public class SoapEnvelopeZip : CommonSoapEnvelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseBodyZip body { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP | zip
        /// </summary>
        public class ResponseBodyZip : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4")]
            public string nfeDadosMsgZip { get; set; }
        }

        #endregion
    }
}
