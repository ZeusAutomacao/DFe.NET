using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        public HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            try
            {
                using (Stream stream = webRequest.GetRequestStream())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(soapEnvelopeXml.OuterXml);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }

                return webRequest;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO na inserção do envelope -> " + e);
                throw;
            }
        }
    }
}
