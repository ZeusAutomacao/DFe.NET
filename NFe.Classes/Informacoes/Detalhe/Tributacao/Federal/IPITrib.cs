using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class IPITrib : IPIBasico
    {
        private decimal? _vBc;
        private decimal? _pIpi;
        private decimal? _qUnid;
        private decimal? _vUnid;
        private decimal? _vIpi;

        /// <summary>
        ///     O09 - Código da Situação Tributária do IPI:
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public CSTIPI CST { get; set; }

        /// <summary>
        ///     O10 - Valor da BC do IPI
        /// </summary>
        /// 
        [XmlElement(Order = 2)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     O13 - Alíquota do IPI
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal? pIPI
        {
            get { return _pIpi.Arredondar(4); }
            set { _pIpi = value.Arredondar(4); }
        }

        /// <summary>
        ///     O11 - Quantidade total na unidade padrão para tributação (somente para os produtos tributados por unidade)
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal? qUnid
        {
            get { return _qUnid.Arredondar(4); }
            set { _qUnid = value.Arredondar(4); }
        }

        /// <summary>
        ///     O12 - Valor por Unidade Tributável
        /// </summary>
        /// 
        [XmlElement(Order = 5)]
        public decimal? vUnid
        {
            get { return _vUnid.Arredondar(4); }
            set { _vUnid = value.Arredondar(4); }
        }

        /// <summary>
        ///     O14 - Valor do IPI
        /// </summary>
        /// 
        [XmlElement(Order = 6)]
        public decimal? vIPI
        {
            get { return _vIpi.Arredondar(2); }
            set { _vIpi = value.Arredondar(2); }
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepIPI()
        {
            return pIPI.HasValue;
        }

        public bool ShouldSerializeqUnid()
        {
            return qUnid.HasValue;
        }

        public bool ShouldSerializevUnid()
        {
            return vUnid.HasValue;
        }

        public bool ShouldSerializevIPI()
        {
            return vIPI.HasValue;
        }
    }
}