namespace NFe.Classes.Informacoes.Total.IbsCbs.Ibs
{
    public class gIBSUF
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vIBSUF;

        /// <summary>
        ///     W38 - Valor total do diferimento
        /// </summary>
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W39 - Valor total de devolução de tributos
        /// </summary>
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W41 - Valor total do IBS da UF
        /// </summary>
        public decimal vIBSUF
        {
            get => _vIBSUF;
            set => _vIBSUF = value.Arredondar(2);
        }
    }
}