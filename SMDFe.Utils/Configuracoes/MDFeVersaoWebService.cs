using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using SMDFe.Utils.Flags;

namespace SMDFe.Utils.Configuracoes
{
    public class MDFeVersaoWebService
    {
        public int TimeOut { get; set; }
        public Estado UfEmitente { get; set; }
        public TipoAmbiente TipoAmbiente { get; set; }
        public VersaoServico VersaoLayout { get; set; }
    }
}