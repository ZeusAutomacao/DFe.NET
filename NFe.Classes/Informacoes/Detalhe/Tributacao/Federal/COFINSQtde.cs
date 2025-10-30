using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class COFINSQtde : COFINSBasico
    {
        private decimal _qBcProd;
        private decimal _vAliqProd;
        private decimal _vCofins;

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public CSTCOFINS CST { get; set; }

        /// <summary>
        ///     S09 - Quantidade Vendida
        /// </summary>
        /// 
        [XmlElement(Order = 2)]
        public decimal qBCProd
        {
            get { return _qBcProd; }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     S10 - Alíquota da COFINS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal vAliqProd
        {
            get { return _vAliqProd; }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     S11 - Valor da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal vCOFINS
        {
            get { return _vCofins; }
            set { _vCofins = value.Arredondar(2); }
        }
    }
}