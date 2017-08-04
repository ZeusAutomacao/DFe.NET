using DFe.CertificadosDigitais;
using DFe.CertificadosDigitais.Cache.Controle;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Facade;

namespace DFe.Facade
{
    public class FacadeBase
    {
        protected DFeConfig DfeConfig { get; set; }
        protected CertificadoDigital CertificadoDigital { get; private set; }

        protected FacadeBase(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            DfeConfig = dfeConfig;
            CertificadoDigital = certificadoDigital;

            DefineConfiguracaoCertificadoDigital(dfeConfig, certificadoDigital);
        }

        private void DefineConfiguracaoCertificadoDigital(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            if (VerificaSeTemCache(dfeConfig, certificadoDigital)) return;

            CertificadoDigital = certificadoDigital;
        }

        private bool VerificaSeTemCache(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            if (!dfeConfig.IsEfetuarCacheCertificadoDigital) return false;

            CertificadoDigital = new ControleCache(dfeConfig, certificadoDigital).ExecutaCache();

            return true;
        }
    }
}