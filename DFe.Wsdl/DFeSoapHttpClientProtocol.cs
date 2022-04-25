using System.Xml;
using CTe.CTeOSDocumento.Wsdl;
using DFe.Http;

namespace DFe.Wsdl
{
    public abstract class DFeSoapHttpClientProtocol
    {
        protected string Invoke(DFeSoapConfig soapConfig)
        {
            return RequestWS.EnviaSefaz(soapConfig);
        }

        protected virtual string GetTagConverter(string ret, string tag)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new System.IO.StringReader(ret));
            XmlNodeList xmlList = doc.GetElementsByTagName(tag);
            var xmlConverter = xmlList[0].OuterXml;
            return xmlConverter;
        }
    }
}