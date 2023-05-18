using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Informacoes.Pagamento
{
    public class detPag
    {
        private decimal _vPag;


        public IndicadorPagamentoDetalhePagamento? indPag { get; set; }

        public bool indPagSpecified { get { return indPag.HasValue; } }

        /// <summary>
        /// YA02 - Forma de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        /// <summary>
        ///     YA02a - Descrição do Meio de Pagamento
        /// </summary>
        public string xPag { get; set; }

        public decimal vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        public card card { get; set; }


    }
}