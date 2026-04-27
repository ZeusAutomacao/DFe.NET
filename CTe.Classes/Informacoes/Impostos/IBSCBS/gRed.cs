using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gRed
    {
        private decimal _pRedAliq;
        private decimal _pAliqEfet;

        public decimal pRedAliq
        {
            get => _pRedAliq.Arredondar(4);
            set => _pRedAliq = value.Arredondar(4);
        }

        public decimal pAliqEfet
        {
            get => _pAliqEfet.Arredondar(4);
            set => _pAliqEfet = value.Arredondar(4);
        }
    }
}
