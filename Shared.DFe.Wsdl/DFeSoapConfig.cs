using System.Security.Cryptography.X509Certificates;
using System.Xml;
using CTe.CTeOSDocumento.Wsdl.Corpo;
using DFe.Wsdl;

namespace CTe.CTeOSDocumento.Wsdl
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