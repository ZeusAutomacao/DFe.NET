using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Configuracao;
using DFe.MDFe.Classes.Flags;

namespace MDFe.AppTeste.Entidades
{
    public class MDFeConfig : DFeConfig
    {
        public override TipoAmbiente TipoAmbiente { get; set; }
        public override VersaoServico VersaoServico { get; set; }
        public override Estado EstadoUf { get; set; }
        public override string CnpjEmitente { get; set; }
    }
}