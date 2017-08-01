using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.MDFe.Classes.Flags;
using DFe.Utils;

namespace DFe.Configuracao
{
    public abstract class DFeConfig
    {
        public DFeConfig()
        {
            ConfiguracaoCertificado = new ConfiguracaoCertificado();
        }

        public ConfiguracaoCertificado ConfiguracaoCertificado { get; }

        public bool IsSalvarXml { get; set; }
        public string CaminhoSchemas { get; set; }
        public string CaminhoSalvarXml { get; set; }
        public string TimeOut { get; set; }

        public abstract TipoAmbiente TipoAmbiente { get; set; }
        public abstract VersaoServico VersaoServico { get; set; }
        public abstract Estado EstadoUf { get; set; }
    }
}