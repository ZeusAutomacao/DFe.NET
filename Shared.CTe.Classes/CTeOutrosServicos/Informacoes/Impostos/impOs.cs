using CTe.Classes.Informacoes.Impostos;
using CTe.Classes.Informacoes.Impostos.Tributacao;
using DFe.Classes;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.Impostos
{
    public class impOs
    {
        public ICMS ICMS { get; set; }

        private decimal? _vTotTrib;
        public decimal? vTotTrib
        {
            get { return _vTotTrib.Arredondar(2); }
            set { _vTotTrib = value.Arredondar(2); }
        }

        public bool vTotTribSpecified { get { return vTotTrib.HasValue; } }

        public string infAdFisco { get; set; }

        public ICMSUFFim ICMSUFFim { get; set; }

        public infTribFed infTribFed { get; set; }
    }
}