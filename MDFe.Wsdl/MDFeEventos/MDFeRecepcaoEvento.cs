using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace MDFe.Wsdl.MDFeEventos
{
    public class MDFeRecepcaoEvento
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao WsdlConfiguracao;

        public MDFeRecepcaoEvento(WsdlConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException();

            WsdlConfiguracao = configuracao;
            soapEnvelope = new SoapEnvelope
            {
                head = new ResponseHead<CTe.CTeOSDocumento.Common.mdfeCabecMsg>
                {
                    mdfeCabecMsg = new CTe.CTeOSDocumento.Common.mdfeCabecMsg
                    {
                        versaoDados = configuracao.Versao,
                        cUF = configuracao.CodigoIbgeEstado
                    }
                }
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Encapsula os dados da requisição no envelope por meio da serialização das partes e realiza a requisção ao Web Service.
        /// </summary>
        /// <param name="mdfeDadosMsg"></param>
        /// <returns>XmlNode</returns>
        public async Task<XmlNode> mdfeRecepcaoEventoAsync(XmlNode mdfeDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                mdfeDadosMsg = mdfeDadosMsg
            };
            return await RequestBuilderAndSender.ExecuteAsync(soapEnvelope, WsdlConfiguracao, TipoEvento.MDFeEvento, "retEventoMDFe");
        }

        public XmlNode mdfeRecepcaoEvento(XmlNode mdfeDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                mdfeDadosMsg = mdfeDadosMsg
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, WsdlConfiguracao, TipoEvento.MDFeEvento, "retEventoMDFe");
        }

    }

    /// <summary>
    /// Classe base para a serialização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelope : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseHead<CTe.CTeOSDocumento.Common.mdfeCabecMsg> head { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }
    }

    public class ResponseHead<T> : CommonResponseHead
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento")]
        public T mdfeCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento")]
        public T mdfeDadosMsg { get; set; }
    }
}
