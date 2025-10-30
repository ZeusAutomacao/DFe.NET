using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS02 : ICMSBasico
    {
        private decimal? _qBCMono;
        private decimal _adRemICMS;
        private decimal _vICMSMono;

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
        ///     N37a - Quantidade tributada
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal? qBCMono
        {
            get { return _qBCMono.Arredondar(4); }
            set { _qBCMono = value.Arredondar(4); }
        }

        public bool ShouldSerializeqBCMono()
        {
            return qBCMono.HasValue;
        }

        /// <summary>
        ///     N38 - Alíquota ad rem do imposto
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal adRemICMS
        {
            get { return _adRemICMS.Arredondar(4); }
            set { _adRemICMS = value.Arredondar(4); }
        }
                
        /// <summary>
        ///     N39 - Valor do ICMS próprio
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal vICMSMono
        {
            get { return _vICMSMono.Arredondar(2); }
            set { _vICMSMono = value.Arredondar(2); }
        }

    }
}