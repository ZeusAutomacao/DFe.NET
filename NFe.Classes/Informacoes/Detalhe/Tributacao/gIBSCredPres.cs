namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCredPres
    {
        private decimal _pCredPres;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // UB74
        public TipocCredPres cCredPres { get; set; }

        // UB75
        public decimal pCredPres
        {
            get => _pCredPres.Arredondar(4);
            set => _pCredPres = value.Arredondar(4);
        }

        // UB76
        public decimal vCredPres
        {
            get => _vCredPres.Arredondar(2);
            set => _vCredPres = value.Arredondar(2);
        }

        // UB77
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus.Arredondar(2);
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}