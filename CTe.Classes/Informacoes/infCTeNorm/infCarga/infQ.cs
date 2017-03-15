using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
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
