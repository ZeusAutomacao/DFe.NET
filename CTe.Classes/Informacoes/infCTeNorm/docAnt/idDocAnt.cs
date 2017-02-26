using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class idDocAnt
    {
        [XmlElement(ElementName = "idDocAntPap")]
        public List<idDocAntPap> idDocAntPap;

        [XmlElement(ElementName = "idDocAntEle")]
        public List<idDocAntEle> idDocAntEle;
    }
}
