using DFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using System;
using System.Xml.Serialization;

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
        ///     YA03a - Data do Pagamento  (NT 2023.004)
        /// </summary>
        [XmlIgnore]
        public DateTime? dPag { get; set; }

        /// <summary>
        /// Proxy para dPag no formato AAAA-MM-DD
        /// </summary>
        [XmlElement(ElementName = "dPag")]
        public string ProxydPag
        {
            get { return dPag.ParaDataString(); }
            set { dPag = DateTime.Parse(value); }
        }

        /// <summary>
        ///     YA03c - CNPJ transacional do pagamento (NT 2023.004)
        /// </summary>
        public string CNPJPag { get; set; }

        /// <summary>
        ///     YA03d - UF do CNPJ do estabelecimento onde o pagamento foi processado/transacionado/recebido(NT 2023.004)
        /// </summary>
        public string UFPag { get; set; }

        /// <summary>
        ///     YA04 - Grupo de Cartões
        /// </summary>
        public card card { get; set; }

        public bool ShouldSerializedPag()
        {
            return dPag.HasValue;
        }
    }
}