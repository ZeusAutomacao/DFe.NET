using System.Security.Cryptography.X509Certificates;
using DFe.Utils.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeConfiguracao
    {
        public static string CaminhoCertificadoDigital { get; set; }
        public static string SenhaCertificadoDigital { get; set; }
        public static string NumeroSerieCertificadoDigital { get; set; }
        public static string CaminhoSchemas { get; set; }

        public static MDFeVersaoWebService VersaoWebService { get; set; } = new MDFeVersaoWebService();

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

    public class MDFeVersaoWebService
    {
        public VersaoServico VersaoMDFeRecepcao { get; set; }
        public VersaoServico VersaoMDFeRetRecepcao { get; set; }
        public VersaoServico VersaoMDFeRecepcaoEvento { get; set; }
        public VersaoServico VersaoMDFeConsulta { get; set; }
        public VersaoServico VersaoMDFeStatusServico { get; set; }
        public VersaoServico VersaoMDFeConsNaoEnc { get; set; }
    }
}