using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.MDFe.Classes.Flags;

namespace DFe.MDFe.Classes.Informacoes.Seguro
{
    [Serializable]
    public class seg
    {
        [XmlElement(ElementName = "infResp")]
        public infResp infResp { get; set; }

        [XmlElement(ElementName = "infSeg")]
        public infSeg infSeg { get; set; }

        [XmlElement(ElementName = "nApol")]
        public string nApol { get; set; }

        [XmlElement(ElementName = "nAver")]
        public List<string> nAver { get; set; }
    }
}