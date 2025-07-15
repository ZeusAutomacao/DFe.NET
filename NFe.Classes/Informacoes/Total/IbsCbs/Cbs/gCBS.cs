namespace NFe.Classes.Informacoes.Total.IbsCbs.Cbs
{
    public class gCBS
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vCBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;
        
        /// <summary>
        ///     W53 - Valor total do diferimento
        /// </summary>
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W54 - Valor total de devolução de tributos
        /// </summary>
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W56 - Valor total do CBS
        /// </summary>
        public decimal vCBS
        {
            get => _vCBS;
            set => _vCBS = value.Arredondar(2);
        }

        /// <summary>
        ///     W56a - Valor total do crédito presumido
        /// </summary>
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        /// <summary>
        ///     W56b - Valor total do crédito presumido em condição suspensiva
        /// </summary>
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}