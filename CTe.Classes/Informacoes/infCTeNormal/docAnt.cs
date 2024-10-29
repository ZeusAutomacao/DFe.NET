using CTe.Classes.Informacoes.infCTeNormal.docAnteriores;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class docAnt
    {
        [XmlElement(ElementName = "emiDocAnt")]
        public List<emiDocAnt> emiDocAnt;
    }
}