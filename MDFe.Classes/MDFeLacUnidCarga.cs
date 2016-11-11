using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeLacUnidCarga
    {
        [XmlElement(ElementName = "nLacre")]
        public string NLacre { get; set; }
    }
}