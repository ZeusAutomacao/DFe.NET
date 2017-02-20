using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Valores
{
    public class vPrest
    {
        public decimal vTPrest { get; set; }

        public decimal vRec { get; set; }

        [XmlElement("Comp")]
        public List<Comp> Comp;
    }
}
