using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMunCarrega
    {
        [XmlElement(ElementName = "cMunCarrega")]
        public string CMunCarrega { get; set; }

        [XmlElement(ElementName = "xMunCarrega")]
        public string XMunCarrega { get; set; }
    }
}