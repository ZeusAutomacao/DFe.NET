using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Informacoes.Identificacao
{
    public class gCompraGov
    {
        private decimal _pRedutor;

        // B32
        public TipoEnteGov tpCompraGov { get; set; }

        // B33
        public decimal pRedutor
        {
            get => _pRedutor.Arredondar(4);
            set => _pRedutor = value.Arredondar(4);
        }
    }
}