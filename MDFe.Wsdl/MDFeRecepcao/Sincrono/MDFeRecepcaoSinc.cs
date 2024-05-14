using CTe.CTeOSDocumento.Common;
using DFe.Utils;
using System;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace MDFe.Wsdl.MDFeRecepcao.Sincrono
{
    public class MDFeRecepcaoSinc
    {
        private readonly SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private readonly WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public MDFeRecepcaoSinc(WsdlConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException();

            this.configuracao = configuracao;
            soapEnvelope = new SoapEnvelope();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Encapsula os dados da requisição no envelope por meio da serialização das partes e realiza a requisção ao Web Service.
        /// </summary>
        /// <param name="mdfeDadosMsg"></param>
        /// <returns>XmlNode</returns>
        public XmlNode mdfeRecepcao(XmlNode mdfeDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<string>
            {
                mdfeDadosMsg = Convert.ToBase64String(Compressao.Zip(mdfeDadosMsg.OuterXml))
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, TipoEvento.MDFeRecepcaoSinc, "retMDFe");
        }

        /// <summary>
        /// Classe base para a serialização no formato do envelope SOAP.
        /// </summary>
        [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public class SoapEnvelope : CommonSoapEnvelope
        {

            [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseBody<string> body { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP
        /// </summary>
        public class ResponseBody<T> : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoSinc")]
            public T mdfeDadosMsg { get; set; }
        }
    }
}
