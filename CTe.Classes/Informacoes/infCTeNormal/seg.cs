using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class seg
    {
        private decimal? _vCarga;
        public respSeg respSeg { get; set; }

        public string xSeg { get; set; }

        public string nApol { get; set; }

        public string nAver { get; set; }

        public decimal? vCarga
        {
            get { return _vCarga.Arredondar(2); }
            set { _vCarga = value.Arredondar(2); }
        }

        public bool vCargaSpecified { get { return vCarga.HasValue; } }
    }
}