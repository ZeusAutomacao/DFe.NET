using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infTotAP
    {
        private decimal _qTotProd;

        public decimal qTotProd
        {
            get { return _qTotProd.Arredondar(4); }
            set { _qTotProd = value.Arredondar(4); }
        }

        public uniAP uniAP { get; set; }
    }
}