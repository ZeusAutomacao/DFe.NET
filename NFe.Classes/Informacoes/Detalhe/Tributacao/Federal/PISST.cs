using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class PISST : PISBasico
    {
        private decimal? _vBc;
        private decimal? _pPis;
        private decimal? _qBcProd;
        private decimal? _vAliqProd;
        private decimal? _vPis;

        /// <summary>
        ///     R02 - Valor da Base de Cálculo do PIS
        /// </summary>
        /// 

        [XmlElement(Order = 1)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     R03 - Alíquota do PIS (em percentual)
        /// </summary>
        /// 
        [XmlElement(Order = 2)]
        public decimal? pPIS
        {
            get { return _pPis.Arredondar(4); }
            set { _pPis = value.Arredondar(4); }
        }

        /// <summary>
        ///     R04 - Quantidade Vendida
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal? qBCProd
        {
            get { return _qBcProd.Arredondar(4); }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     R05 - Alíquota do PIS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal? vAliqProd
        {
            get { return _vAliqProd.Arredondar(4); }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     R06 - Valor do PIS
        /// </summary>
        /// 
        [XmlElement(Order = 5)]
        public decimal? vPIS
        {
            get { return _vPis.Arredondar(2); }
            set { _vPis = value.Arredondar(2); }
        }
        
        /// <summary>
        ///     R07 - Indica se o valor do PISST compõe o valor total da NF-e
        /// </summary>
        [XmlElement(Order = 6)]
        public IndSomaPISST? indSomaPISST { get; set; }

        public bool ShouldSerializeindSomaPISST()
        {
            return indSomaPISST.HasValue;
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