using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.Http.Ext;

namespace DFe.Http
{
    public class RequestWS
    {
        public string EnviaSefaz(DFeEnvelope envelope, string url, string metodo)
        {
            try
            {
                string XMLRetorno = string.Empty;
                string xmlSoap = new Envelope().Construir(envelope);

                Uri uri = new Uri(url);

                WebRequest webRequest = WebRequest.Create(uri);
                HttpWebRequest httpWR = (HttpWebRequest)webRequest;
                // todo httpWR.Timeout

                httpWR.ContentLength = Encoding.ASCII.GetBytes(xmlSoap).Length;

                httpWR.ClientCertificates.Add(new X509Certificate2(@"certificado", "senha do mesmo"));

                httpWR.ComposeContentType("application/soap+xml", Encoding.UTF8, metodo);

                httpWR.Method = "POST";

                Stream reqStream = httpWR.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(reqStream);
                streamWriter.Write(xmlSoap, 0, Encoding.ASCII.GetBytes(xmlSoap).Length);
                streamWriter.Close();

                WebResponse webResponse = httpWR.GetResponse();
                Stream respStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(respStream);

                XMLRetorno = streamReader.ReadToEnd();

                return XMLRetorno;
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                throw;
            }
        }
    }
}