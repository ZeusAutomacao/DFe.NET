using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCarga
    {
        public decimal vCarga { get; set; }
        public string proPred { get; set; }
        public string xOutCat { get; set; }

        [XmlElement("infQ")]
        public List<infQ> infQ;

        /// <summary>
        /// Versão 3.0 - Opcional
        /// </summary>
        public decimal? vCargaAverb { get; set; }

        public bool vCargaAverbSpecified => vCargaAverb.HasValue;

        public infDoc infDoc { get; set; }
        public docAnt docAnt { get; set; }
    }
}
