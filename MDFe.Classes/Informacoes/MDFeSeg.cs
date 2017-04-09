using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeSeg
    {
        [XmlElement(ElementName = "infResp")]
        public MDFeInfResp InfResp { get; set; }

        [XmlElement(ElementName = "infSeg")]
        public MDFeInfSeg InfSeg { get; set; }

        [XmlElement(ElementName = "nApol")]
        public string NApol { get; set; }

        [XmlElement(ElementName = "nAver")]
        public List<string> NAver { get; set; }
    }

    [Serializable]
    public class MDFeInfSeg
    {
        [XmlElement(ElementName = "xSeg")]
        public string XSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
    }

    [Serializable]
    public class MDFeInfResp
    {
        [XmlElement(ElementName = "respSeg")]
        public MDFeRespSeg RespSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}