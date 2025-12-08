using DFe.Classes;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal.cobrancas
{
    public class fat
    {
        private decimal? _vOrig;
        private decimal? _vDesc;
        private decimal? _vLiq;
        public string nFat { get; set; }

        public decimal? vOrig
        {
            get { return _vOrig.Arredondar(2); }
            set { _vOrig = value.Arredondar(2); }
        }

        public decimal? vDesc
        {
            get { return _vDesc.Arredondar(2); }
            set { _vDesc = value.Arredondar(2); }
        }

        public decimal? vLiq
        {
            get { return _vLiq.Arredondar(2); }
            set { _vLiq = value.Arredondar(2); }
        }


        public bool vOrigSpecified { get { return vOrig.HasValue; } }
        public bool vDescSpecified { get { return vDesc.HasValue; } }
        public bool vLiqSpecified { get { return vLiq.HasValue; } }
    }
}