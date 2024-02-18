using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CTe.CTeOSDocumento.Common;
using CTe.CTeOSDocumento.Soap;

namespace DFe.Wsdl.Common
{
    public class RequestSefazHttpClientHandler : IRequestSefaz
    {
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

        public async Task<string> SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital,
            string url, int timeOut,
            TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            if (!tipoEvento.HasValue && string.IsNullOrWhiteSpace(actionUrn))
            {
                throw new ArgumentNullException(
                    "Pelo menos uma das propriedades tipoEvento ou actionUrl devem ser definidos para executar a action na requisição soap");
            }

            if (tipoEvento.HasValue)
            {
                actionUrn = new SoapUrls().GetSoapUrl(tipoEvento.Value);
            }

            string xmlSoap = xmlEnvelop.InnerXml;

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                handler.ClientCertificates.Add(certificadoDigital);

                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromMilliseconds(timeOut == 0 ? 2000 : timeOut);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent(xmlSoap, Encoding.UTF8, "application/soap+xml");
                    request.Headers.Add("SOAPAction", actionUrn);

                    HttpResponseMessage response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }

        }

        public string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut,
            TipoEvento? tipoEvento = null, string actionUrn = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            if (!tipoEvento.HasValue && string.IsNullOrWhiteSpace(actionUrn))
            {
                throw new ArgumentNullException(
                    "Pelo menos uma das propriedades tipoEvento ou actionUrl devem ser definidos para executar a action na requisição soap");
            }

            if (tipoEvento.HasValue)
            {
                actionUrn = new SoapUrls().GetSoapUrl(tipoEvento.Value);
            }

            string xmlSoap = xmlEnvelop.InnerXml;

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                handler.ClientCertificates.Add(certificadoDigital);

                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromMilliseconds(timeOut == 0 ? 2000 : timeOut);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent(xmlSoap, Encoding.UTF8, "application/soap+xml");
                    request.Headers.Add("SOAPAction", actionUrn);

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsStringAsync().Result;
                }
            }

        }
    }
}