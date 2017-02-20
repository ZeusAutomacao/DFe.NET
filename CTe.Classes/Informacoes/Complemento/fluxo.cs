using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class fluxo
    {
        public string xOrig { get; set; }

        [XmlElement(ElementName = "pass")]
        public List<pass> pass { get; set; }

        public string xDest { get; set; }

        public string xRota { get; set; }
    }
}
