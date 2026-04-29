using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gDevTrib
    {
        private decimal _vDevTrib { get; set; }

        public decimal vDevTrib
        {
            get => _vDevTrib.Arredondar(2);
            set => _vDevTrib = value.Arredondar(2);
        }
    }
}
