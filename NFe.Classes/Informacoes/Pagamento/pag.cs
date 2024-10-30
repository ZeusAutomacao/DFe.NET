using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Pagamento
{
    public class pag
    {
        private decimal? _vPag;
        private decimal? _vTroco;

        /// <summary>
        /// YA01a - Grupo Detalhamento da Forma de Pagamento
        /// VERSÃO 4.00
        /// </summary>
        [XmlElement("detPag")]
        public List<detPag> detPag { get; set; }

        /// <summary>
        /// YA09 - Valor do troco
        /// Versão 4.00
        /// </summary>
        public decimal? vTroco
        {
            get { return _vTroco.Arredondar(2); }
            set { _vTroco = value.Arredondar(2); }
        }

        public bool vTrocoSpecified
        {
            get { return _vTroco.HasValue; }
        }

        /// <summary>
        ///     YA02 - Forma de pagamento
        ///     Versão 3.00
        /// </summary>
        public FormaPagamento? tPag { get; set; }

        public bool tPagSpecified
        {
            get { return tPag.HasValue; }
        }

        /// <summary>
        ///     YA03 - Valor do Pagamento
        /// Versão 3.00
        /// </summary>
        public decimal? vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        public bool vPagSpecified
        {
            get { return vPag.HasValue; }
        }

        /// <summary>
        ///     YA04 - Grupo de Cartões
        ///     Versão 3.00
        /// </summary>
        public card card { get; set; }
    }
}