using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class PISOutr : PISBasico
    {
        private decimal? _vBc;
        private decimal? _pPis;
        private decimal? _qBcProd;
        private decimal? _vAliqProd;
        private decimal? _vPis;

        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS
        /// </summary>
        /// 

        [XmlElement(Order = 1)]
        public CSTPIS CST { get; set; }

        /// <summary>
        ///     Q07 - Valor da Base de Cálculo do PIS
        /// </summary>
        /// 

        [XmlElement(Order = 2)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     Q08 - Alíquota do PIS (em percentual)
        /// </summary>
        /// 

        [XmlElement(Order = 3)]
        public decimal? pPIS
        {
            get { return _pPis.Arredondar(4); }
            set { _pPis = value.Arredondar(4); }
        }

        /// <summary>
        ///     Q10 - Quantidade Vendida
        /// </summary>
        /// 

        [XmlElement(Order = 4)]
        public decimal? qBCProd
        {
            get { return _qBcProd.Arredondar(4); }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     Q11 - Alíquota do PIS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 5)]
        public decimal? vAliqProd
        {
            get { return _vAliqProd.Arredondar(4); }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     Q09 - Valor do PIS
        /// </summary>
        /// 
        [XmlElement(Order = 6)]
        public decimal? vPIS
        {
            get { return _vPis.Arredondar(2); }
            set { _vPis = value.Arredondar(2); }
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepPIS()
        {
            return pPIS.HasValue;
        }

        public bool ShouldSerializeqBCProd()
        {
            return qBCProd.HasValue;
        }

        public bool ShouldSerializevAliqProd()
        {
            return vAliqProd.HasValue;
        }

        public bool ShouldSerializevPIS()
        {
            return vPIS.HasValue;
        }
    }
}