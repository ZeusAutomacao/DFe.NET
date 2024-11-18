using NFe.Classes.Informacoes.Identificacao.Tipos;
using System;

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

        /// <summary>
        ///     YA03a - Data do Pagamento
        /// </summary>
        public DateTimeOffset? dPag { get; set; }

        public card card { get; set; }

        /// <summary>
        ///     YA03a - Data do Pagamento
        /// </summary>
        public string CNPJPag { get; set; }

        /// <summary>
        ///     YA03a - Data do Pagamento
        /// </summary>
        public string UFPag { get; set; }

        public bool ShouldSerializedPag()
        {
            return dPag.HasValue;
        }
    }
}