using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeVersaoWebService
    {
        public int TimeOut { get; set; }
        public Estado UfEmitente { get; set; }
        public TipoAmbiente TipoAmbiente { get; set; }
        public VersaoServico VersaoLayout { get; set; }
    }
}
