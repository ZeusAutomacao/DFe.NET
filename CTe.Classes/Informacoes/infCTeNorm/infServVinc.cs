using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infServVinc
    {
        [XmlElement(ElementName = "infCTeMultimodal")]
        public List<infCTeMultimodal> infCTeMultimodal { get; set; }
    }
}