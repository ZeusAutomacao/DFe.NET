using System;
using System.Text;
using System.Xml;
using CTe.CTeOSDocumento.Wsdl.Corpo;
using DFe.Utils;

namespace DFe.Wsdl
{
    public class DFeCorpo
    {
        public DFeCorpo(string namespaceBody, ITagCorpo tagCorpo)
        {
            NamespaceBody = namespaceBody;
            TagCorpo = tagCorpo;
        }

        public XmlNode Xml { get; set; }
        public bool XmlZip { get; set; }

        public string NamespaceBody { get; set; }

        public ITagCorpo TagCorpo { get; set; }

        public string GetXmlBody()
        {
            return TagCorpo.GetTagCorpo(this);
        }

        public string GetXml()
        {
            var xmlDelcaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            var xmlEnvio = XmlZip == false ? Xml.LastChild.OuterXml : CompactarXml(xmlDelcaration);

            return xmlEnvio;
        }

        private string CompactarXml(string xmlDelcaration)
        {
            return Convert.ToBase64String(Compressao.Zip(new StringBuilder(xmlDelcaration).Append(Xml.LastChild.OuterXml).ToString()));
        }
    }
}