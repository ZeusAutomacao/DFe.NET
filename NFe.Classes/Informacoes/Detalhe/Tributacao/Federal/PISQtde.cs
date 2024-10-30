using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class PISQtde : PISBasico
    {
        private decimal _qBcProd;
        private decimal _vAliqProd;
        private decimal _vPis;

        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS
        /// </summary>
        /// 

        [XmlElement(Order = 1)]
        public CSTPIS CST { get; set; }

        /// <summary>
        ///     Q10 - Quantidade Vendida
        /// </summary>
        /// 

        [XmlElement(Order = 2)]
        public decimal qBCProd
        {
            get { return _qBcProd; }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     Q11 - Alíquota do PIS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal vAliqProd
        {
            get { return _vAliqProd; }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     Q09 - Valor do PIS
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal vPIS
        {
            get { return _vPis; }
            set { _vPis = value.Arredondar(2); }
        }
    }
}