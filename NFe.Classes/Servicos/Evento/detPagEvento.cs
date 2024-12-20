using System;
using DFe.Utils;
using System.Xml.Serialization;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Servicos.Evento
{
    public sealed class detPagEvento
    {
        private decimal _vPag;

        /// <summary>
        ///     P22 - Indicador da Forma de Pagamento
        /// </summary>
        public IndicadorPagamentoDetalhePagamento? indPag { get; set; }

        public bool indPagSpecified { get { return indPag.HasValue; } }

        /// <summary>
        ///     P23 - Meio de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        /// <summary>
        ///     P24 - Descrição do Meio de Pagamento
        /// </summary>
        public string xPag { get; set; }

        /// <summary>
        ///     P25 - Valor do Pagamento
        /// </summary>
        public decimal vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        /// <summary>
        ///     P26 - Data do Pagamento
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
        ///     P28 - CNPJ do estabelecimento onde o pagamento foi processado/transacionado/recebido 
        ///           quando a emissão do documento fiscal ocorrer em estabelecimento distinto
        /// </summary>
        public string CNPJPag { get; set; }

        /// <summary>
        ///     P29 - UF do CNPJ do estabelecimento onde o pagamento foi processado/transacionado/recebido
        /// </summary>
        public string UFPag { get; set; }

        /// <summary>
        ///     P30 - CNPJ da instituição financeira, de pagamento, adquirente ou subadquirente
        /// </summary>
        public string CNPJIF { get; set; }

        /// <summary>
        ///     P31 - Bandeira da operadora de cartão de crédito e/ou débito
        /// </summary>
        public BandeiraCartao? tBand { get; set; }

        public bool ShouldSerializetBand()
        {
            return tBand.HasValue;
        }

        /// <summary>
        ///     P32 - Número de autorização da operação cartão de crédito e/ou débito
        /// </summary>
        public string cAut { get; set; }

        /// <summary>
        ///     P34 - CNPJ do estabelecimento beneficiário do pagamento
        /// </summary>
        public string CNPJReceb { get; set; }

        /// <summary>
        ///     P35 - UF do estabelecimento beneficiário do pagamento
        /// </summary>
        public string UFReceb { get; set; }

        public bool ShouldSerializedPag()
        {
            return dPag.HasValue;
        }
    }
}
