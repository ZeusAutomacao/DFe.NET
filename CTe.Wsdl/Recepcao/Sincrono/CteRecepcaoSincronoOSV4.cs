using CTe.CTeOSDocumento.Common;
using DFe.Utils;
using System;
using System.Net;
using System.Xml;

namespace CTe.Wsdl.Recepcao.Sincrono
{
    public class CteRecepcaoSincronoOSV4
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private WsdlConfiguracao configuracao;

        /// <summary>
        /// Cria o cabeçalho do envelope a ser enviado e atribui as configurações do WSDL.
        /// </summary>
        /// <param name="configuracao"></param>
        public CteRecepcaoSincronoOSV4(WsdlConfiguracao configuracao)
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
        /// <param name="cteCabecMsg"></param>
        /// <returns>XmlNode</returns>
        public XmlNode CTeRecepcaoSincV4(XmlNode cteDadosMsg)
        {
            soapEnvelope.body = new ResponseBody<string>
            {
                cteDadosMsg = Convert.ToBase64String(Compressao.Zip(cteDadosMsg.OuterXml))
            };
            return RequestBuilderAndSender.Execute(soapEnvelope, configuracao, TipoEvento.CTeRecepcaoOSV4, "retCTeOS");
        }
    }
}
