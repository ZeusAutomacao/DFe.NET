using System.Xml;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.Http;

namespace DFe.Wsdl
{
    public abstract class DFeSoapHttpClientProtocol
    {
        protected string Invoke(DFeSoapConfig soapConfig)
        {
            var xmlRetorno = RequestWS.EnviaSefaz(soapConfig);

            XmlDocument doc = new XmlDocument();
            doc.Load(new System.IO.StringReader(xmlRetorno));

            XmlNodeList xmlList = doc.GetElementsByTagName("retCTeOS");

            return xmlList[0].OuterXml;
        }
    }
}