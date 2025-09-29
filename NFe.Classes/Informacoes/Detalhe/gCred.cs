namespace NFe.Classes.Informacoes.Detalhe
{
    public class gCred
    {
        private decimal? _pCredPresumido;
        private decimal? _vCredPresumido;

        /// <summary>
        /// I05h - Código de Benefício Fiscal de Crédito Presumido na UF aplicado ao item
        /// </summary>
        public string cCredPresumido { get; set; }

        /// <summary>
        /// I05i - Percentual do Crédito Presumido
        /// </summary>
        public decimal? pCredPresumido
        {
            get { return _pCredPresumido; }
            set { _pCredPresumido = value.Arredondar(4); }
        }

        /// <summary>
        /// I05j - Valor do Crédito Presumido
        /// </summary>
        public decimal? vCredPresumido
        {
            get { return _vCredPresumido; }
            set { _vCredPresumido = value.Arredondar(2); }
        }

        public bool ShouldSerializepCredPresumido()
        {
            return _pCredPresumido.HasValue;
        }

        public bool ShouldSerializevCredPresumido()
        {
            return _vCredPresumido.HasValue;
        }
    }
}
