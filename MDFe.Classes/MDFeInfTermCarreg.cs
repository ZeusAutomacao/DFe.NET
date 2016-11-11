using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfTermCarreg
    {
        [XmlElement(ElementName = "cTermCarreg")]
        public string CTermCarreg { get; set; }

        [XmlElement(ElementName = "xTermCarreg")]
        public string XTermCarreg { get; set; }
    }
}