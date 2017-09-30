using System.Security.Cryptography.X509Certificates;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao
{
    public class WsdlConfiguracao
    {
        public Estado EstadoUF;
        public string Url { get; set; }
        public string Versao { get; set; }
        public X509Certificate2 CertificadoDigital { get; set; }
        public int TimeOut { get; set; }
        public VersaoServico VersaoLayout { get; set; }
    }
}