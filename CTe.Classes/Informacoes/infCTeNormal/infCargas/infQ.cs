using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal.infCargas
{
    public class infQ
    {
        private decimal _qCarga;
        public cUnid cUnid { get; set; }

        public string tpMed { get; set; }

        public decimal qCarga
        {
            get { return _qCarga.Arredondar(4); }
            set { _qCarga = value.Arredondar(4); }
        }
    }
}