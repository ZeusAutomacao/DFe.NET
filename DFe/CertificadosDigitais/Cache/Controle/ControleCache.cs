using DFe.Configuracao;

namespace DFe.CertificadosDigitais.Cache.Controle
{
    public class ControleCache
    {
        private DFeConfig Config { get; }
        private CertificadoDigital CertificadoDigital { get; }

        public ControleCache(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            Config = config;
            CertificadoDigital = certificadoDigital;
        }

        public CertificadoDigital ExecutaCache()
        {
            var certificadoDigital =
                Config.ProxyCacheCertificadoDigital.BuscarPorCnpjEmitente(Config.CnpjEmitente);

            if (certificadoDigital != null)
            {
                return certificadoDigital;
            }

            Config.ProxyCacheCertificadoDigital.Adicionar(Config.CnpjEmitente, CertificadoDigital);

            return CertificadoDigital;
        }
    }
}