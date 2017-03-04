using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCarga
    {
        public decimal? vCarga
        {
            get { return _vCarga.Arredondar(2); }
            set { _vCarga = value.Arredondar(2); }
        }

        public bool vCargaSpecified => vCarga.HasValue;

        public string proPred { get; set; }
        public string xOutCat { get; set; }

        [XmlElement("infQ")]
        public List<infQ> infQ;

        private decimal? _vCarga;
        private decimal? _vCargaAverb;

        /// <summary>
        /// Versão 3.0 - Opcional
        /// </summary>
        public decimal? vCargaAverb
        {
            get { return _vCargaAverb.Arredondar(2); }
            set { _vCargaAverb = value.Arredondar(2); }
        }

        public bool vCargaAverbSpecified => vCargaAverb.HasValue;

        public infDoc infDoc { get; set; }
        public docAnt docAnt { get; set; }
    }
}
