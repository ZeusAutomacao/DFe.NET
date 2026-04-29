using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gDif
    {
        private decimal _pDif;
        private decimal _vDif;

        public decimal pDif
        {
            get => _pDif.Arredondar(4);
            set => _pDif = value.Arredondar(4);
        }

        public decimal vDif
        {
            get => _vDif.Arredondar(2);
            set => _vDif = value.Arredondar(2);
        }
    }
}
