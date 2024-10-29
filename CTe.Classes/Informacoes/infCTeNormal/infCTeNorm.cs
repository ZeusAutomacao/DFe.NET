using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infCTeNorm
    {
        public infServico infServico { get; set; }
        public infQcteOs infQ { get; set; }

        [XmlElement(ElementName = "infDocRef")]
        public List<infDocRef> infDocRef { get; set; }
        public infCarga infCarga { get; set; }
        public infDoc infDoc { get; set; }
        public docAnt docAnt { get; set; }

        /// <summary>
        /// Versao 2.0
        /// </summary>
        [XmlElement(ElementName = "seg")]
        public List<seg> seg { get; set; }

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