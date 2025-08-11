namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoDif
    {
        private decimal _pDifIbs;
        private decimal _vIbsMonoDif;
        private decimal _pDifCbs;
        private decimal _vCbsMonoDif;

        // UB100
        public decimal pDifIBS
        {
            get => _pDifIbs.Arredondar(4);
            set => _pDifIbs = value.Arredondar(4);
        }

        // UB101
        public decimal vIBSMonoDif
        {
            get => _vIbsMonoDif.Arredondar(2);
            set => _vIbsMonoDif = value.Arredondar(2);
        }

        // UB102
        public decimal pDifCBS
        {
            get => _pDifCbs.Arredondar(4);
            set => _pDifCbs = value.Arredondar(4);
        }

        // UB103
        public decimal vCBSMonoDif
        {
            get => _vCbsMonoDif.Arredondar(2);
            set => _vCbsMonoDif = value.Arredondar(2);
        }
    }
}