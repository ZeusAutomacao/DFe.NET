using System.Xml;
using DFe.DocumentosEletronicos.Wsdl.Corpo;

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

        public string NamespaceBody { get; set; }

        public ITagCorpo TagCorpo { get; set; }

        public string GetXmlBody()
        {
            return TagCorpo.GetTagCorpo(this);
        }

        public string GetXml()
        {
            return Xml.LastChild.OuterXml;
        }
    }
}