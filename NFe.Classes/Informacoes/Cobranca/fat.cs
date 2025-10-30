namespace NFe.Classes.Informacoes.Cobranca
{
    public class fat
    {
        private decimal? _vOrig;
        private decimal? _vDesc;
        private decimal? _vLiq;

        /// <summary>
        ///     Y03 - Número da Fatura
        /// </summary>
        public string nFat { get; set; }

        /// <summary>
        ///     Y04 - Valor Original da Fatura
        /// </summary>
        public decimal? vOrig
        {
            get { return _vOrig.Arredondar(2); }
            set { _vOrig = value.Arredondar(2); }
        }

        /// <summary>
        ///     Y05 - Valor do desconto
        /// </summary>
        public decimal? vDesc
        {
            get { return _vDesc.Arredondar(2); }
            set { _vDesc = value.Arredondar(2); }
        }

        /// <summary>
        ///     Y06 - Valor Líquido da Fatura
        /// </summary>
        public decimal? vLiq
        {
            get { return _vLiq.Arredondar(2); }
            set { _vLiq = value.Arredondar(2); }
        }

        public bool ShouldSerializevOrig()
        {
            return vOrig.HasValue;
        }

        public bool ShouldSerializevDesc()
        {
            return vDesc.HasValue;
        }

        public bool ShouldSerializevLiq()
        {
            return vLiq.HasValue;
        }
    }
}