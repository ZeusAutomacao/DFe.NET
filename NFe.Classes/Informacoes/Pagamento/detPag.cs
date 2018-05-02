using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Informacoes.Pagamento
{
    public class detPag
    {
        private decimal _vPag;


        public IndicadorPagamentoDetalhePagamento? indPag { get; set; }

        /// <summary>
        /// YA02 - Forma de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        public decimal vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        public card card { get; set; }


    }
}