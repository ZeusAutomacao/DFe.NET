namespace NFe.Classes.Informacoes.Total
{
    public class gIBS
    {
        private decimal _vIBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // W37
        public gIBSUFTotal gIBSUF { get; set; }

        // W42
        public gIBSMunTotal gIBSMun { get; set; }

        // W47
        public decimal vIBS
        {
            get => _vIBS;
            set => _vIBS = value.Arredondar(2);
        }

        // W48
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        // W49
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}