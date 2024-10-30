using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Cana
{
    public class cana
    {
        private decimal _qTotMes;
        private decimal _qTotAnt;
        private decimal _qTotGer;
        private decimal _vFor;
        private decimal _vTotDed;
        private decimal _vLiqFor;

        /// <summary>
        ///     ZC02 - Identificação da safra
        /// </summary>
        public string safra { get; set; }

        /// <summary>
        ///     ZC03 - Mês e ano de referência
        /// </summary>
        public string @ref { get; set; }

        /// <summary>
        ///     ZC04 - Grupo Fornecimento diário de cana
        /// </summary>
        [XmlElement("forDia")]
        public List<forDia> forDia { get; set; }

        /// <summary>
        ///     ZC07 - Quantidade Total do Mês
        /// </summary>
        public decimal qTotMes
        {
            get { return _qTotMes; }
            set { _qTotMes = value.Arredondar(10); }
        }

        /// <summary>
        ///     ZC08 - Quantidade Total Anterior
        /// </summary>
        public decimal qTotAnt
        {
            get { return _qTotAnt; }
            set { _qTotAnt = value.Arredondar(10); }
        }

        /// <summary>
        ///     ZC09 - Quantidade Total Geral
        /// </summary>
        public decimal qTotGer
        {
            get { return _qTotGer; }
            set { _qTotGer = value.Arredondar(10); }
        }

        /// <summary>
        ///     ZC10 - Grupo Deduções – Taxas e Contribuições
        /// </summary>
        [XmlElement("deduc")]
        public List<deduc> deduc { get; set; }

        /// <summary>
        ///     ZC13 - Valor dos Fornecimentos
        /// </summary>
        public decimal vFor
        {
            get { return _vFor; }
            set { _vFor = value.Arredondar(2); }
        }

        /// <summary>
        ///     ZC14 - Valor Total da Dedução
        /// </summary>
        public decimal vTotDed
        {
            get { return _vTotDed; }
            set { _vTotDed = value.Arredondar(2); }
        }

        /// <summary>
        ///     ZC15 - Valor Líquido dos Fornecimentos
        /// </summary>
        public decimal vLiqFor
        {
            get { return _vLiqFor; }
            set { _vLiqFor = value.Arredondar(2); }
        }
    }
}