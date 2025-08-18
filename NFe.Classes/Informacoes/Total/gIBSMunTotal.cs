namespace NFe.Classes.Informacoes.Total
{
    public class gIBSMunTotal
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vIBSMun;

        // W43
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }

        // W44
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }

        // W46
        public decimal vIBSMun
        {
            get => _vIBSMun;
            set => _vIBSMun = value.Arredondar(2);
        }
    }
}