using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal.docAnteriores
{
    public class idDocAnt
    {
        [XmlElement(ElementName = "idDocAntPap")]
        public List<idDocAntPap> idDocAntPap;

        [XmlElement(ElementName = "idDocAntEle")]
        public List<idDocAntEle> idDocAntEle;
    }
}