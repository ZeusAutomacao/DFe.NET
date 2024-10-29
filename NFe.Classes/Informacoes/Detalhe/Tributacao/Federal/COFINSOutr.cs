using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class COFINSOutr : COFINSBasico
    {
        private decimal? _vBc;
        private decimal? _pCofins;
        private decimal? _qBcProd;
        private decimal? _vAliqProd;
        private decimal? _vCofins;

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS
        /// </summary>
        /// 

        [XmlElement(Order = 1)]
        public CSTCOFINS CST { get; set; }

        /// <summary>
        ///     S07 - Valor da Base de Cálculo da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 2)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     S08 - Alíquota da COFINS (em percentual)
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal? pCOFINS
        {
            get { return _pCofins.Arredondar(4); }
            set { _pCofins = value.Arredondar(4); }
        }

        /// <summary>
        ///     S09 - Quantidade Vendida
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal? qBCProd
        {
            get { return _qBcProd.Arredondar(4); }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     S10 - Alíquota da COFINS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 5)]
        public decimal? vAliqProd
        {
            get { return _vAliqProd.Arredondar(4); }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     S11 - Valor da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 6)]
        public decimal? vCOFINS
        {
            get { return _vCofins.Arredondar(2); }
            set { _vCofins = value.Arredondar(2); }
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepCOFINS()
        {
            return pCOFINS.HasValue;
        }

        public bool ShouldSerializeqBCProd()
        {
            return qBCProd.HasValue;
        }

        public bool ShouldSerializevAliqProd()
        {
            return vAliqProd.HasValue;
        }

        public bool ShouldSerializevCOFINS()
        {
            return vCOFINS.HasValue;
        }
    }
}