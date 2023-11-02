using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace CTe.Wsdl.ConsultaProtocolo.V4
{
    public class CteConsultaV4
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public CteConsultaV4(WsdlConfiguracao configuracao)
        {
            if (configuracao == null) throw new ArgumentNullException();

            this.configuracao = configuracao;
            this.soapEnvelope = new SoapEnvelope();
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
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, TipoEvento.CTeConsultaV4, "retConsSitCTe");
        }

        public async Task<XmlNode> cteConsultaCTAsync(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<XmlNode>
            {
                cteDadosMsg = cteDadosMsg
            };
            return await RequestBuilderAndSender.ExecuteAsync(soapEnvelope, configuracao, TipoEvento.CTeConsultaV4, "retConsSitCTe");
        }
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
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeConsultaV4")]
        public T cteDadosMsg { get; set; }
    }
}