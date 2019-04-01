using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using MDFe.Utils.Cabeçalho;
using static MDFe.Utils.Enums.Enums;

namespace MDFe.Utils.Soap
{
    public class SoapUtils
    {
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            var soapserializer = new XmlSerializer(typeof(T));
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            soapserializer.Serialize(xmlTextWriter, soapEnvelope);
            xmlTextWriter.Formatting = Formatting.None;
            var xmlDocument = new XmlDocument();

            var output = Encoding.UTF8.GetString(memoryStream.ToArray());
            var _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (output.StartsWith(_byteOrderMarkUtf8))
            {
                output = output.Remove(0, _byteOrderMarkUtf8.Length);
            }

            xmlDocument.LoadXml(output);

            return xmlDocument;
        }

#if NETSTANDARD
        public async Task<string> MdfeEncHttpClient(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, Tipo consultaTipo)
        {
            var resposta = "";

            try
            {
                var soapUrl = new SoapUrls().GetSoapUrl(consultaTipo);

                var _clientHandler = new HttpClientHandler();
                _clientHandler.ClientCertificates.Add(certificadoDigital);
                _clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;

                using (var client = new HttpClient(_clientHandler))
                {
                    var soapString = xmlEnvelop.InnerXml;
                    client.DefaultRequestHeaders.Add(HttpHeader.ACTION, soapUrl);
                    var content = new StringContent(soapString, Encoding.UTF8, HttpHeader.CONTETTYPE);
                    using (var response = await client.PostAsync(url, content))
                    {
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        {
                            resposta = soapResponse;
                            return resposta;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return resposta;
        }
#endif
    }
}
