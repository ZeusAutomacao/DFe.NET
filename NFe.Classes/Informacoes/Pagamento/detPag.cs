namespace NFe.Classes.Informacoes.Pagamento
{
    public class detPag
    {
        private decimal _vPag;

        /// <summary>
        ///     YA02 - Forma de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        /// <summary>
        ///     YA03 - Valor do Pagamento
        /// </summary>
        public decimal vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        /// <summary>
        ///     YA04 - Grupo de Cartões
        /// </summary>
        public card card { get; set; }

        private decimal? _vTroco;

        /// <summary>
        /// YA09 - Valor do troco
        /// </summary>
        public decimal? vTroco
        {
            get { return _vTroco.Arredondar(2); }
            set { _vTroco = value.Arredondar(2); }
        }

        public bool ShouldSerializevTroco()
        {
            return vTroco.HasValue;
        }
    }
}