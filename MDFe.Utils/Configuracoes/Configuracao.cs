using System.Security.Cryptography.X509Certificates;
using DFe.Utils.Assinatura;

namespace MDFe.Utils.Configuracoes
{
    public class Configuracao
    {
        public static string CaminhoCertificadoDigital { get; set; }
        public static string SenhaCertificadoDigital { get; set; }
        public static string NumeroSerieCertificadoDigital { get; set; }
        public static string CaminhoSchemas { get; set; }


        public static X509Certificate2 X509Certificate2 => ObterCertificado();

        private static X509Certificate2 ObterCertificado()
        {
            if (!string.IsNullOrEmpty(CaminhoCertificadoDigital) && !string.IsNullOrEmpty(SenhaCertificadoDigital))
            {
                return CertificadoDigital.ObterDeArquivo(CaminhoCertificadoDigital, SenhaCertificadoDigital);
            }

            return CertificadoDigital.ObterDoRepositorio(NumeroSerieCertificadoDigital, SenhaCertificadoDigital);
        }
    }
}