using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.MDFe.Classes.Flags;

namespace DFe.MDFe.Classes.Informacoes
{
    [Serializable]
    public class seg
    {
        [XmlElement(ElementName = "infResp")]
        public infResp InfResp { get; set; }

        [XmlElement(ElementName = "infSeg")]
        public infSeg InfSeg { get; set; }

        [XmlElement(ElementName = "nApol")]
        public string NApol { get; set; }

        [XmlElement(ElementName = "nAver")]
        public List<string> NAver { get; set; }
    }

    [Serializable]
    public class infSeg
    {
        [XmlElement(ElementName = "xSeg")]
        public string XSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
    }

    [Serializable]
    public class infResp
    {
        [XmlElement(ElementName = "respSeg")]
        public MDFeRespSeg RespSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}