using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Transporte
{
    public class vol
    {
        private decimal? _pesoL;
        private decimal? _pesoB;

        /// <summary>
        ///     X27 - Quantidade de volumes transportados
        /// </summary>
        public int? qVol { get; set; }

        /// <summary>
        ///     X28 - Espécie dos volumes transportados
        /// </summary>
        public string esp { get; set; }

        /// <summary>
        ///     X29 - Marca dos volumes transportados
        /// </summary>
        public string marca { get; set; }

        /// <summary>
        ///     X30 - Numeração dos volumes transportados
        /// </summary>
        public string nVol { get; set; }

        /// <summary>
        ///     X31 - Peso Líquido (em kg)
        /// </summary>
        public decimal? pesoL
        {
            get { return _pesoL.Arredondar(3); }
            set { _pesoL = value.Arredondar(3); }
        }

        /// <summary>
        ///     X32 - Peso Bruto (em kg)
        /// </summary>
        public decimal? pesoB
        {
            get { return _pesoB.Arredondar(3); }
            set { _pesoB = value.Arredondar(3); }
        }

        /// <summary>
        ///     X33 - Grupo Lacres
        ///     <para>Ocorrência: 0-5000</para>
        /// </summary>
        [XmlElement("lacres")]
        public List<lacres> lacres { get; set; }

        public bool ShouldSerializeqVol()
        {
            return qVol.HasValue;
        }

        public bool ShouldSerializepesoL()
        {
            return pesoL.HasValue;
        }

        public bool ShouldSerializepesoB()
        {
            return pesoB.HasValue;
        }
    }
}