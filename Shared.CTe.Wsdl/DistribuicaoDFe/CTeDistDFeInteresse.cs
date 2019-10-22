using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;

namespace CTe.Wsdl.DistribuicaoDFe
{
    public class CTeDistDFeInteresse
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public CTeDistDFeInteresse(WsdlConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException();

            this.configuracao = configuracao;
            soapEnvelope = new SoapEnvelope();
        }

        /// <summary>
        /// Encapsula os dados da requisição no envelope por meio da serialização das partes e realiza a requisção ao Web Service.
        /// </summary>
        /// <param name="cteDadosMsg"></param>
        /// <returns>XmlNode</returns>
        public XmlNode Execute(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody
            {
                cteDistDFeInteresse = new cteDistDFeInteresse
                {
                    cteDadosMsg = cteDadosMsg
                }
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, TipoEvento.CTeDistribuicaoDFe, "retDistDFeInt");
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody
            {
                cteDistDFeInteresse = new cteDistDFeInteresse
                {
                    cteDadosMsg = cteDadosMsg
                }
            };
            return await RequestBuilderAndSender.ExecuteAsync(soapEnvelope, configuracao, TipoEvento.CTeDistribuicaoDFe, "retDistDFeInt");
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
        public ResponseBody body { get; set; }
    }

    /// <summary>
    /// Classe para o cabeçalho do Envelope SOAP
    /// </summary>
    public class ResponseHead<T> : CommonResponseHead
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")]
        public T cteCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")]
        public cteDistDFeInteresse cteDistDFeInteresse { get; set; }
    }

    public class cteDistDFeInteresse
    {
        public XmlNode cteDadosMsg { get; set; }
    }
}
