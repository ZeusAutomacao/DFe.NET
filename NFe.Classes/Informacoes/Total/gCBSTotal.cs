namespace NFe.Classes.Informacoes.Total
{
    public class gCBSTotal
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vCBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // W53
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }

        // W54
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }

        // W56
        public decimal vCBS
        {
            get => _vCBS;
            set => _vCBS = value.Arredondar(2);
        }

        // W56a
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        // W56b
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}