using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infQcteOs
    {
        private decimal _qCarga;

        public decimal qCarga
        {
            get { return _qCarga.Arredondar(4); }
            set { _qCarga = value.Arredondar(4); }
        }
    }
}