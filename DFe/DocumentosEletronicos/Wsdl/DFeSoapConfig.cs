using System.Security.Cryptography.X509Certificates;
using System.Xml;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl
{
    public class DFeSoapConfig
    {
        public XmlNode Xml { get; set; }

        public DFeCabecalho DFeCabecalho { get; set; }

        public string NamespaceBody { get; set; }

        public string NamespaceHeader { get; set; }

        public string Url { get; set; }

        public int TimeOut { get; set; }

        public string Metodo { get; set; }

        public X509Certificate2 Certificado { get; set; }

        public ITagCabecalho TagTagCabecalho { get; set; }

        public ITagCorpo TagCorpo { get; set; }

        public string GetXmlHeader()
        {
            return TagTagCabecalho.GetTagCabecalho(this);
        }

        public string GetXmlBody()
        {
            return TagCorpo.GetTagCorpo(this);
        }
    }
}