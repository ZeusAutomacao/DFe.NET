using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.InfCTeComplementar;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCTeNorm
    {
        public infCarga infCarga { get; set; }
        public infDoc infDoc { get; set; }
        public docAnt docAnt { get; set; }

        /// <summary>
        /// Versao 2.0
        /// </summary>
        public seg seg { get; set; }

        public infModal infModal { get; set; }

        /// <summary>
        /// Versao 2.0
        /// </summary>
        [XmlElement(ElementName = "peri")]
        public List<peri> peri { get; set; }

        [XmlElement(ElementName = "veicNovos")]
        public List<veicNovos> veicNovos { get; set; }
        public cobr cobr { get; set; }
        public infCteSub infCteSub { get; set; }

        /// <summary>
        /// Versao 3.0
        /// </summary>
        public infGlobalizado infGlobalizado { get; set; }

        /// <summary>
        /// Versao 3.0
        /// </summary>
        public infServVinc infServVinc { get; set; }
    }
}
