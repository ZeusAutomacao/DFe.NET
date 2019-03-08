using System;
using System.Xml;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using NFe.Utils;

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
            return XmlZip == false ? Xml.LastChild.OuterXml : Convert.ToBase64String(Compressao.Zip(Xml.LastChild.OuterXml));
        }
    }
}