using System.Xml.Serialization;
using NFe.Classes.Informacoes.Detalhe.Observacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao;

namespace NFe.Classes.Informacoes.Detalhe
{
    public class det
    {
        private decimal? _vItem;

        /// <summary>
        ///     H02 - Número do item do NF
        /// </summary>
        [XmlAttribute]
        public int nItem { get; set; }

        /// <summary>
        ///     I01 - Detalhamento de Produtos e Serviços
        /// </summary>
        public prod prod { get; set; }

        /// <summary>
        ///     M01 - Tributos incidentes no Produto ou Serviço
        /// </summary>
        public imposto imposto { get; set; }

        /// <summary>
        ///     UA01 - Informação do Imposto devolvido
        /// </summary>
        public impostoDevol impostoDevol { get; set; }

        /// <summary>
        ///     V01 - Informações Adicionais do Produto
        /// </summary>
        public string infAdProd { get; set; }        
        
        /// <summary>
        ///     VA01 - Grupo de observações de uso livre (para o item da NF-e)
        /// </summary>
        [XmlElement(nameof(obsItem))]
        public obsItem obsItem { get; set; }

        // VB01
        public decimal? vItem
        {
            get => _vItem;
            set => _vItem = value.Arredondar(2);
        }

        // VC01
        public DFeReferenciado DFeReferenciado { get; set; }

        public bool ShouldSerializevItem()
        {
            return vItem.HasValue;
        }
    }
}