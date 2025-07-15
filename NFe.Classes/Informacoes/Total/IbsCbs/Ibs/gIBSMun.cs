namespace NFe.Classes.Informacoes.Total.IbsCbs.Ibs
{
    public class gIBSMun
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vIBSMun;

        /// <summary>
        ///     W43 - Valor total do diferimento
        /// </summary>
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }

        /// <summary>
        ///     W44 - Valor total de devolução de tributos
        /// </summary>
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }

        /// <summary>
        ///     W46 - Valor total do IBS do Município
        /// </summary>
        public decimal vIBSMun
        {
            get => _vIBSMun;
            set => _vIBSMun = value.Arredondar(2);
        }
    }
}