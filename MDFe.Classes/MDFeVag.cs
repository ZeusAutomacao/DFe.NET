using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeVag
    {
        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nVag")]
        public long NVag { get; set; }

        [XmlElement(ElementName = "nSeq")]
        public short? NSeq { get; set; }

        [XmlElement(ElementName = "TU")]
        public decimal TU { get; set; }
    }
}