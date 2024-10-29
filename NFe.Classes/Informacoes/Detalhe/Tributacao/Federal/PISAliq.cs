using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class PISAliq : PISBasico
    {
        private decimal _vBc;
        private decimal _pPis;
        private decimal _vPis;

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
        public decimal vBC
        {
            get { return _vBc; }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     Q08 - Alíquota do PIS (em percentual)
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal pPIS
        {
            get { return _pPis; }
            set { _pPis = value.Arredondar(4); }
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