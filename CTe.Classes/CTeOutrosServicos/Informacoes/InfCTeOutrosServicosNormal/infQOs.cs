using DFe.Classes;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class infQOs
    {
        private decimal? _qCarga;

        public decimal? qCarga
        {
            get { return _qCarga.Arredondar(4); }
            set { _qCarga = value.Arredondar(4); }
        }

        public bool qCargaSpecified { get { return _qCarga.HasValue; } }
    }
}