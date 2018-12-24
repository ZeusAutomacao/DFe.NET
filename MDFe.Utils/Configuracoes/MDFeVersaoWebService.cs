using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using MDFe.Utils.Flags;

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