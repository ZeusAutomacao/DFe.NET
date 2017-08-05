using DFe.CertificadosDigitais.Cache;
using DFe.CertificadosDigitais.Cache.Implementacoes;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;

namespace DFe.Configuracao
{
    public abstract class DFeConfig
    {
        public DFeConfig()
        {
            ProxyCacheCertificadoDigital = new CacheCertificadoDigital();
        }

        public bool IsSalvarXml { get; set; }
        public string CaminhoSchemas { get; set; }
        public string CaminhoSalvarXml { get; set; }
        public int TimeOut { get; set; }
        public bool IsEfetuarCacheCertificadoDigital { get; set; }
        public IProxyCacheCertificadoDigital ProxyCacheCertificadoDigital { get; set; } 

        public abstract TipoAmbiente TipoAmbiente { get; set; }
        public abstract VersaoServico VersaoServico { get; set; }
        public abstract Estado EstadoUf { get; set; }
        public abstract string CnpjEmitente { get; set; }

        public bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }
    }
}