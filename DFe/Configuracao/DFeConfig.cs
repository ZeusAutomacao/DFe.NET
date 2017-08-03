using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.MDFe.Classes.Flags;

namespace DFe.Configuracao
{
    public abstract class DFeConfig
    {
        public DFeConfig()
        {
            
        }

        public bool IsSalvarXml { get; set; }
        public string CaminhoSchemas { get; set; }
        public string CaminhoSalvarXml { get; set; }
        public string TimeOut { get; set; }

        public abstract TipoAmbiente TipoAmbiente { get; set; }
        public abstract VersaoServico VersaoServico { get; set; }
        public abstract Estado EstadoUf { get; set; }

        protected bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }
    }
}