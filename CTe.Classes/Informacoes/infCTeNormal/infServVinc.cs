using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infServVinc
    {
        [XmlElement(ElementName = "infCTeMultimodal")]
        public List<infCTeMultimodal> infCTeMultimodal { get; set; }
    }
}