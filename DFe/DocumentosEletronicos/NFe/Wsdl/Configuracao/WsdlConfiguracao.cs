using System.Security.Cryptography.X509Certificates;

namespace DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao
{
    public class WsdlConfiguracao
    {
        public string Url { get; set; }
        public string Versao { get; set; }
        public X509Certificate2 CertificadoDigital { get; set; }
        public int TimeOut { get; set; }
    }
}