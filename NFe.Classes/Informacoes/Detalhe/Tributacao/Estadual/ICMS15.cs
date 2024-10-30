using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS15 : ICMSBasico
    {
        private decimal? _qBCMono;
        private decimal _adRemICMS;
        private decimal _vICMSMono;
        private decimal? _qBCMonoReten;
        private decimal _adRemICMSReten;
        private decimal _vICMSMonoReten;
        private decimal? _pRedAdRem;

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
        ///     N39 - Valor do ICMS diferido
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal vICMSMono
        {
            get { return _vICMSMono.Arredondar(2); }
            set { _vICMSMono = value.Arredondar(2); }
        }

        /// <summary>
        ///     N39a - Quantidade tributada sujeita a retenção
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal? qBCMonoReten
        {
            get { return _qBCMonoReten.Arredondar(4); }
            set { _qBCMonoReten = value.Arredondar(4); }
        }

        public bool ShouldSerializeqBCMonoReten()
        {
            return qBCMonoReten.HasValue;
        }

        /// <summary>
        ///     N40 - Alíquota ad rem do imposto com retenção
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal adRemICMSReten
        {
            get { return _adRemICMSReten.Arredondar(4); }
            set { _adRemICMSReten = value.Arredondar(4); }
        }

        /// <summary>
        ///     N41 - Alíquota ad rem do imposto com retenção
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vICMSMonoReten
        {
            get { return _vICMSMonoReten.Arredondar(2); }
            set { _vICMSMonoReten = value.Arredondar(2); }
        }

        /// <summary>
        ///     N47 - Percentual de redução do valor da alíquota adrem do ICMS Alíquota ad rem do imposto com retenção
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? pRedAdRem
        {
            get { return _pRedAdRem.Arredondar(2); }
            set { _pRedAdRem = value.Arredondar(2); }
        }

        public bool ShouldSerializepRedAdRem()
        {
            return pRedAdRem.HasValue;
        }

        /// <summary>
        ///     N48 - Motivo da redução do adrem
        /// </summary>
        [XmlElement(Order = 10)]
        public int? motRedAdRem { get; set; }

        public bool ShouldSerializemotRedAdRem()
        {
            return motRedAdRem.HasValue;
        }
    }
}