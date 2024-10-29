using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS53 : ICMSBasico
    {
        private decimal? _qBCMono;
        private decimal? _adRemICMS;
        private decimal? _vICMSMonoOp;
        private decimal? _pDif;
        private decimal? _vICMSMono;
        private decimal? _qBCMonoDif;
        private decimal? _adRemICMSDif;
        private decimal? _vICMSMonoDif;

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
        ///     N37a - Quantidade Tributada
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
        ///     N38 - Alíquota adRem do imposto
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? adRemICMS
        {
            get { return _adRemICMS.Arredondar(4); }
            set { _adRemICMS = value.Arredondar(4); }
        }
        public bool ShouldSerializeadRemICMS()
        {
            return adRemICMS.HasValue;
        }

        /// <summary>
        ///     N41a - Valor do ICMS da operação
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? vICMSMonoOp
        {
            get { return _vICMSMonoOp.Arredondar(2); }
            set { _vICMSMonoOp = value.Arredondar(2); }
        }
        public bool ShouldSerializevICMSMonoOp()
        {
            return vICMSMonoOp.HasValue;
        }

        /// <summary>
        ///     N42 - Percentual do diferimento
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal? pDif
        {
            get { return _pDif.Arredondar(4); }
            set { _pDif = value.Arredondar(4); }
        }
        public bool ShouldSerializepDif()
        {
            return pDif.HasValue;
        }

        /// <summary>
        ///     N43 - Valor do ICMS diferido
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal? vICMSMonoDif
        {
            get { return _vICMSMonoDif.Arredondar(2); }
            set { _vICMSMonoDif = value.Arredondar(2); }
        }
        public bool ShouldSerializevICMSMonoDif()
        {
            return vICMSMonoDif.HasValue;
        }

        /// <summary>
        ///     N39 - Valor do ICMS próprio devido
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal? vICMSMono
        {
            get { return _vICMSMono.Arredondar(2); }
            set { _vICMSMono = value.Arredondar(2); }
        }
        public bool ShouldSerializevICMSMono()
        {
            return vICMSMono.HasValue;
        }

        /// <summary>
        ///     N41a - Quantidade tributada diferida
        ///     Campo removido na NT 2023.001 v1.20 - Informar nulo para novos documentos.
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? qBCMonoDif
        {
            get { return _qBCMonoDif.Arredondar(4); }
            set { _qBCMonoDif = value.Arredondar(4); }
        }
        public bool ShouldSerializeqBCMonoDif()
        {
            return qBCMonoDif.HasValue;
        }

        /// <summary>
        ///     N42 - Alíquota ad rem do imposto diferido
        ///     Campo removido na NT 2023.001 v1.20 - Informar nulo para novos documentos.
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal? adRemICMSDif
        {
            get { return _adRemICMSDif.Arredondar(4); }
            set { _adRemICMSDif = value.Arredondar(4); }
        }
        public bool ShouldSerializeadRemICMSDif()
        {
            return adRemICMSDif.HasValue;
        }

    }
}