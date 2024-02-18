using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;
using CTe.CTeOSDocumento.Soap;
using DFe.Http.Ext;

namespace DFe.Wsdl.Common
{
    public class RequestSefazDefault : IRequestSefaz
    {
        /// <summary>
        /// Serializa a estrutura do envelope contida no objeto para um XmlDocument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <returns></returns>
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            return ConfiguracaoServicoWSDL.RequestSefaz.SerealizeDocument(soapEnvelope);
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
            return await ConfiguracaoServicoWSDL.RequestSefaz.SendRequestAsync(xmlEnvelop, certificadoDigital, url, timeOut,
                tipoEvento, actionUrn);
        }

        public string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            return ConfiguracaoServicoWSDL.RequestSefaz.SendRequest(xmlEnvelop, certificadoDigital, url, timeOut,
                tipoEvento, actionUrn);
        }
    }
}