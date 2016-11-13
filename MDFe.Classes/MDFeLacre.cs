using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeLacre
    {
        [XmlElement(ElementName = "nLacre")]
        public string NLacre { get; set; }
    }
}