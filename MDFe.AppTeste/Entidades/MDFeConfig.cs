using DFe.Configuracao;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;

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