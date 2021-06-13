using System;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;
using WsdlConfiguracao = MDFe.Wsdl.Configuracao.WsdlConfiguracao;

namespace MDFe.Wsdl.Gerado.MDFeStatusServico
{
    public class MDFeStatusServicoCore
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao WsdlConfiguracao;

        public MDFeStatusServicoCore(WsdlConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException();

            WsdlConfiguracao = configuracao;
            soapEnvelope = new SoapEnvelope
            {
                head = new ResponseHead<mdfeCabecMsg>
                {
                    mdfeCabecMsg = new mdfeCabecMsg
                    {
                        versaoDados = configuracao.Versao,
                        cUF = configuracao.CodigoIbgeEstado
                    }
                }
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }




    }

    /// <summary>
    /// Classe base para a serialização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelope : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
       public ResponseHead<mdfeCabecMsg> head { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }
    }

    public class ResponseHead<T> : CommonResponseHead
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public T mdfeCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico")]
        public T cteDadosMsg { get; set; }
    }
}
