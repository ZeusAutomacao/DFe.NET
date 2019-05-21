using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using DFe.DocumentosEletronicos.Common;
using DFe.Http.Ext;

namespace DFe.DocumentosEletronicos.Soap
{
    /// <summary>
    /// Classe utilitária resposável pela serialização das classes em um envelope do tipo
    /// SOAP e envio das requisições para os respectivos Web Services.
    /// </summary>
    public class SoapUtils
    {
        /// <summary>
        /// Serializa a estrutura do envelope contida no objeto para um XmlDocument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <returns></returns>
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            // instancia do objeto responsável pela serialização
            var soapserializer = new XmlSerializer(typeof(T));

            // Armazena os dados em memória para manipulação
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            
            //Serializa o objeto de acordo com o formato
            soapserializer.Serialize(xmlTextWriter, soapEnvelope);
            xmlTextWriter.Formatting = Formatting.None;

            var xmlDocument = new XmlDocument();
            
            //Remove o caractere especial BOM (byte order mark)
            var output = Encoding.UTF8.GetString(memoryStream.ToArray());
            var _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (output.StartsWith(_byteOrderMarkUtf8))
            {
                output = output.Remove(0, _byteOrderMarkUtf8.Length);
            }

            //Carrega os dados na instancia do XmlDocument
            xmlDocument.LoadXml(output);

            return xmlDocument;
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
        public async Task<string> SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento tipoEvento)
        {
            var xmlSoap = xmlEnvelop.InnerXml;
            var httpWr = (HttpWebRequest)WebRequest.Create(new Uri(url));
            
            httpWr.Timeout = timeOut == 0 ? 2000 : timeOut;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, new SoapUrls().GetSoapUrl(tipoEvento));
            httpWr.Method = "POST";
            
            var streamWriter = new StreamWriter(httpWr.GetRequestStream());
            
            streamWriter.Write(xmlSoap, 0, Encoding.UTF8.GetBytes(xmlSoap).Length);
            streamWriter.Close();

            var webResponse = httpWr.GetResponse();
            var respStream = webResponse.GetResponseStream();
            var streamReader = new StreamReader(respStream);

            var xmlRetorno = streamReader.ReadToEnd();
            return await Task.FromResult(xmlRetorno);
        }

        public string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento tipoEvento)
        {
            var xmlSoap = xmlEnvelop.InnerXml;
            var httpWr = (HttpWebRequest)WebRequest.Create(new Uri(url));

            httpWr.Timeout = timeOut == 0 ? 2000 : timeOut;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, new SoapUrls().GetSoapUrl(tipoEvento));
            httpWr.Method = "POST";

            var streamWriter = new StreamWriter(httpWr.GetRequestStream());

            streamWriter.Write(xmlSoap, 0, Encoding.UTF8.GetBytes(xmlSoap).Length);
            streamWriter.Close();

            var webResponse = httpWr.GetResponse();
            var respStream = webResponse.GetResponseStream();
            var streamReader = new StreamReader(respStream);

            var xmlRetorno = streamReader.ReadToEnd();
            return xmlRetorno;
        }
    }

}
