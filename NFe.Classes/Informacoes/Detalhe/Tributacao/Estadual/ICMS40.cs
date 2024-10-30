using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS40 : ICMSBasico
    {
        private decimal? _vIcmsDeson;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        [XmlElement(Order = 2)]
        public Csticms CST { get; set; }

        /// <summary>
        ///     N27a - Valor do ICMS desonerado
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal? vICMSDeson
        {
            get { return _vIcmsDeson.Arredondar(2); }
            set { _vIcmsDeson = value.Arredondar(2); }
        }

        /// <summary>
        ///     N28 - Motivo da desoneração do ICMS
        /// </summary>
        [XmlElement(Order = 4)]
        public MotivoDesoneracaoIcms? motDesICMS { get; set; }
        
        /// <summary>
        /// N28b - Indica se o valor do ICMS desonerado (vICMSDeson) deduz 
        /// do valor do item (vProd). (NT 2023.004) 
        /// </summary>
        [XmlElement(Order = 5)]
        public DeduzDesoneracaoNoProduto? indDeduzDeson { get; set; }

        public bool ShouldSerializevICMSDeson()
        {
            return vICMSDeson.HasValue;
        }

        public bool ShouldSerializemotDesICMS()
        {
            return motDesICMS.HasValue;
        }

        public bool ShouldSerializeindDeduzDeson()
        {
            return indDeduzDeson.HasValue;
        }
    }
}