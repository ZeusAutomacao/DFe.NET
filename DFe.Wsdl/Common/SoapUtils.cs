using CTe.CTeOSDocumento.Common;
using DFe.Http.Ext;
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
        /// <summary>
        /// Serializa a estrutura do envelope contida no objeto para um XmlDocument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <returns></returns>
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            // instancia do objeto responsável pela serialização
            XmlSerializer soapserializer = new XmlSerializer(typeof(T));

            // Armazena os dados em memória para manipulação
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            //Serializa o objeto de acordo com o formato
            soapserializer.Serialize(xmlTextWriter, soapEnvelope);
            xmlTextWriter.Formatting = Formatting.None;

            XmlDocument xmlDocument = new XmlDocument();

            //Remove o caractere especial BOM (byte order mark)
            string output = Encoding.UTF8.GetString(memoryStream.ToArray());
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
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
        public async Task<string> SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            //verifica se pelo menos uma das 2 propriedades obrigatorias estão definidas
            if (!tipoEvento.HasValue && string.IsNullOrWhiteSpace(actionUrn))
            {
                throw new ArgumentNullException("Pelo menos uma das propriedades tipoEvento ou actionUrl devem ser definidos para executar a action na requisição soap");
            }

            //caso o tipoevento esteja definido, pega a url do evento
            if (tipoEvento.HasValue)
            {
                actionUrn = new SoapUrls().GetSoapUrl(tipoEvento.Value);
            }

            string xmlSoap = xmlEnvelop.InnerXml;
            HttpWebRequest httpWr = (HttpWebRequest)WebRequest.Create(new Uri(url));

            httpWr.Timeout = timeOut == 0 ? 2000 : timeOut;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, actionUrn);
            httpWr.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWr.GetRequestStream());

            streamWriter.Write(xmlSoap, 0, Encoding.UTF8.GetBytes(xmlSoap).Length);
            streamWriter.Close();

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWr.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string xmlRetorno = streamReader.ReadToEnd();
                return await Task.FromResult(xmlRetorno);
            }
        }

        public string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            //verifica se pelo menos uma das 2 propriedades obrigatorias estão definidas
            if (!tipoEvento.HasValue && string.IsNullOrWhiteSpace(actionUrn))
            {
                throw new ArgumentNullException("Pelo menos uma das propriedades tipoEvento ou actionUrl devem ser definidos para executar a action na requisição soap");
            }

            //caso o tipoevento esteja definido, pega a url do evento
            if (tipoEvento.HasValue)
            {
                actionUrn = new SoapUrls().GetSoapUrl(tipoEvento.Value);
            }

            string xmlSoap = xmlEnvelop.InnerXml;
            HttpWebRequest httpWr = (HttpWebRequest)WebRequest.Create(new Uri(url));

            httpWr.Timeout = timeOut == 0 ? 2000 : timeOut;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, actionUrn);
            httpWr.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWr.GetRequestStream());

            streamWriter.Write(xmlSoap, 0, Encoding.UTF8.GetBytes(xmlSoap).Length);
            streamWriter.Close();

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpWr.GetResponse())
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string xmlRetorno = streamReader.ReadToEnd();
                return xmlRetorno;
            }
        }
    }

}
