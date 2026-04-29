using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Identificacao
{
    public class gCompraGov
    {
        private decimal _pRedutor;

        public TipoEnteGov tpEnteGov { get; set; }

        public decimal pRedutor
        {
            get => _pRedutor.Arredondar(4);
            set => _pRedutor = value.Arredondar(4);
        }

    }
}
