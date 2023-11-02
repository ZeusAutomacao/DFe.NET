using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace CTe.Wsdl.Status
{
    public class CteStatusServico
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;
        private SoapEnvelopeV4 soapEnvelopeV4;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public CteStatusServico(WsdlConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException();

            this.configuracao = configuracao;

            soapEnvelope = new SoapEnvelope();
            soapEnvelopeV4 = new SoapEnvelopeV4();

            if (configuracao.Versao != "4.00")
            {
                soapEnvelope.head = new ResponseHead<cteCabecMsg>
                {
                    cteCabecMsg = new cteCabecMsg
                    {
                        versaoDados = configuracao.Versao,
                        cUF = configuracao.CodigoIbgeEstado
                    }
                };
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Encapsula os dados da requisição no envelope por meio da serialização das partes e realiza a requisção ao Web Service.
        /// </summary>
        /// <param name="cteDadosMsg"></param>
        /// <returns>XmlNode</returns>
        public async Task<XmlNode> cteStatusServicoCTAsync(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                cteDadosMsg = cteDadosMsg
            };
            return await RequestBuilderAndSender.ExecuteAsync(soapEnvelope, configuracao, configuracao.Versao == "4.00" ? TipoEvento.CTeStatusServicoV4 : TipoEvento.CTeStatusServico, "retConsStatServCte");
        }

        public XmlNode cteStatusServicoCT(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                cteDadosMsg = cteDadosMsg
            };

            if (configuracao.Versao == "4.00")
            {
                soapEnvelopeV4.body = new ResponseBodyV4<XmlNode>
                {
                    cteDadosMsg = cteDadosMsg
                };
            }

            if (configuracao.Versao == "4.00")
            {
                return RequestBuilderAndSender.Execute(soapEnvelopeV4, configuracao, TipoEvento.CTeStatusServicoV4,
                    "retConsStatServCTe");
            }

            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao,
                TipoEvento.CTeStatusServico,
                "retConsStatServCte");
        }
    }

    /// <summary>
    /// Classe base para a serialização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelope : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseHead<cteCabecMsg> head { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }
    }

    /// <summary>
    /// Classe para o cabeçalho do Envelope SOAP
    /// </summary>
    public class ResponseHead<T> : CommonResponseHead
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public T cteCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public T cteDadosMsg { get; set; }
    }

    /// <summary>
    /// Classe base para a serialização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelopeV4 : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBodyV4<XmlNode> body { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBodyV4<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeStatusServicoV4")]
        public T cteDadosMsg { get; set; }
    }
}
