using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Informacoes.Pagamento
{
    public class detPag
    {
        private decimal _vPag;

        /// <summary>
        ///     YA01b - Indicador da Forma de Pagamento
        /// </summary>
        public IndicadorPagamentoDetalhePagamento? indPag { get; set; }

        public bool indPagSpecified { get { return indPag.HasValue; } }

        /// <summary>
        ///     YA02 - Meio de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        /// <summary>
        ///     YA02a - Descrição do Meio de Pagamento
        /// </summary>
        public string xPag { get; set; }

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
    }
}