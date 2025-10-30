using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS61 : ICMSBasico
    {
        private decimal? _qBCMonoRet;
        private decimal? _adRemICMSRet;
        private decimal? _vICMSMonoRet;

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
        ///     N43a - Quantidade tributada retida anteriormente
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal? qBCMonoRet
        {
            get { return _qBCMonoRet.Arredondar(4); }
            set { _qBCMonoRet = value.Arredondar(4); }
        }

        public bool ShouldSerializeqBCMonoRet()
        {
            return qBCMonoRet.HasValue;
        }

        /// <summary>
        ///     N44 - Alíquota ad rem do imposto retido anteriormente
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? adRemICMSRet
        {
            get { return _adRemICMSRet.Arredondar(4); }
            set { _adRemICMSRet = value.Arredondar(4); }
        }

        /// <summary>
        ///     N45 - Valor do ICMS retido anteriormente
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? vICMSMonoRet
        {
            get { return _vICMSMonoRet.Arredondar(2); }
            set { _vICMSMonoRet = value.Arredondar(2); }
        }

    }
}