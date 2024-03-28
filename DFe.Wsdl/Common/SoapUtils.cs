using CTe.CTeOSDocumento.Common;
using DFe.Http.Ext;
using DFe.Wsdl.Common;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CTe.CTeOSDocumento.Soap
{
    /// <summary>
    /// Classe utilitária resposável pela serialização das classes em um envelope do tipo
    /// SOAP e envio das requisições para os respectivos Web Services.
    /// </summary>
    public class SoapUtils
    {
        private IRequestSefaz _requestSefaz;

        public SoapUtils()
        {
            _requestSefaz = ConfiguracaoServicoWSDL.GetRequestSefaz();
        }

        /// <summary>
        /// Serializa a estrutura do envelope contida no objeto para um XmlDocument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <returns></returns>
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            return _requestSefaz.SerealizeDocument(soapEnvelope);
        }

        /// <summary>
        /// Cria e envia a requisição HttpClient, retornando a resposta obtida do WebService.
        /// </summary>
        /// <param name="xmlEnvelop"></param>
        /// <param name="certificadoDigital"></param>
        /// <param name="url"></param>
        /// <param name="timeOut"></param>
        /// <param name="tipoEvento"></param>
        /// <returns></returns>
        public async Task<string> SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            return await _requestSefaz.SendRequestAsync(xmlEnvelop, certificadoDigital, url, timeOut, tipoEvento, actionUrn);
        }

        public string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            return _requestSefaz.SendRequest(xmlEnvelop, certificadoDigital, url, timeOut, tipoEvento, actionUrn);
        }
    }

}
