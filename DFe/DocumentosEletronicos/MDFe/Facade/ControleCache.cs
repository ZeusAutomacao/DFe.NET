using DFe.CertificadosDigitais;
using DFe.Configuracao;

namespace DFe.DocumentosEletronicos.MDFe.Facade
{
    public class ControleCache
    {
        private DFeConfig Config { get; }
        public CertificadoDigital CertificadoDigital { get; }

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