using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace CTe.Wsdl.ConsultaProtocolo
{
    public class CteConsulta
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public CteConsulta(WsdlConfiguracao configuracao)
        {
            if (configuracao == null) throw new ArgumentNullException();

            this.configuracao = configuracao;
            soapEnvelope = new SoapEnvelope
            {
                head = new ResponseHead<cteCabecMsg>
                {
                    cteCabecMsg = new cteCabecMsg
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
        /// <param name="cteCabecMsg"></param>
        /// <returns>XmlNode</returns>
        public XmlNode cteConsultaCT(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                cteDadosMsg = cteDadosMsg
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, TipoEvento.CTeConsulta, "retConsSitCTe");
        }

        public async Task<XmlNode> cteConsultaCTAsync(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                cteDadosMsg = cteDadosMsg
            };
            return await RequestBuilderAndSender.ExecuteAsync(soapEnvelope, configuracao, TipoEvento.CTeConsulta, "retConsSitCTe");
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
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public T cteCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public T cteDadosMsg { get; set; }
    }
}