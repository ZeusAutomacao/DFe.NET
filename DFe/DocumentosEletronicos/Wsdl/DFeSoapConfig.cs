using System.Security.Cryptography.X509Certificates;
using System.Xml;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl
{
    public class DFeSoapConfig
    {
        public DFeCabecalho DFeCabecalho { get; set; }

        public DFeCorpo DFeCorpo { get; set; }

        public string Url { get; set; }

        public int TimeOut { get; set; }

        public string Metodo { get; set; }

        public X509Certificate2 Certificado { get; set; }
    }
}