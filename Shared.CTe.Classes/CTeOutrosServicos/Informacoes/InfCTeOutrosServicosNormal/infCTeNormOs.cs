using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class infCTeNormOs
    {
        public infServico infServico { get; set; }

        [XmlElement("infDocRef")]
        public List<infDocRef> infDocRef { get; set; }

        [XmlElement("seg")]
        public List<segOs> seg { get; set; }

        [XmlElement("infModal")]
        public infModalOs infModal { get; set; }

        [XmlElement("infCteSub")]
        public infCteSubOs infCteSub { get; set; }
    }
}