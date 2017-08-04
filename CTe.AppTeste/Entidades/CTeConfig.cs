using DFe.Configuracao;
using DFe.Entidades;
using DFe.Flags;


namespace CTe.AppTeste.Entidades
{
    public class CTeConfig : DFeConfig
    {
        public override TipoAmbiente TipoAmbiente { get; set; }
        public override VersaoServico VersaoServico { get; set; }
        public override Estado EstadoUf { get; set; }
        public override string CnpjEmitente { get; set; }
    }
}