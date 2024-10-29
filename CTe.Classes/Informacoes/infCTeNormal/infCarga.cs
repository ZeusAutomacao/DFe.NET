using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infCargas;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infCarga
    {
        private decimal? _vCarga;
        private decimal? _vCargaAverb;

        [XmlElement(Order = 1)]
        public decimal? vCarga
        {
            get { return _vCarga.Arredondar(2); }
            set { _vCarga = value.Arredondar(2); }
        }

        [XmlElement(Order = 2)]
        public string proPred { get; set; }

        [XmlElement(Order = 3)]
        public string xOutCat { get; set; }

        [XmlElement(ElementName = "infQ", Order = 4)]
        public List<infQ> infQ { get; set; }

        /// <summary>
        /// Versão 3.0 - Opcional
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? vCargaAverb
        {
            get { return _vCargaAverb.Arredondar(2); }
            set { _vCargaAverb = value.Arredondar(2); }
        }

        [XmlElement(Order = 6)]
        public infDoc infDoc { get; set; }

        [XmlElement(Order = 7)]
        public docAnt docAnt { get; set; }

        public bool vCargaSpecified { get { return vCarga.HasValue; } }
        public bool vCargaAverbSpecified { get { return vCargaAverb.HasValue; } }
    }
}