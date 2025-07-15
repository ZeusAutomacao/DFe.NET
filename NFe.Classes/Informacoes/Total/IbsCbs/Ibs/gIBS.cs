namespace NFe.Classes.Informacoes.Total.IbsCbs.Ibs
{
    public class gIBS
    {
        private decimal _vIBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;
        
        /// <summary>
        ///     W37 - Grupo total do IBS da UF
        /// </summary>
        public gIBSUF gIBSUF  { get; set; }
        
        /// <summary>
        ///     W42 - Grupo total do IBS do Município
        /// </summary>
        public gIBSMun gIBSMun  { get; set; }
        
        /// <summary>
        ///     W47 - Valor total do IBS
        /// </summary>
        public decimal vIBS
        {
            get => _vIBS;
            set => _vIBS = value.Arredondar(2);
        }

        /// <summary>
        ///     W48 - Valor total do crédito presumido
        /// </summary>
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        /// <summary>
        ///     W49 - Valor total do crédito presumido em condição suspensiva
        /// </summary>
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}