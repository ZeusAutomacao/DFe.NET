using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfTermDescarreg
    {
        [XmlElement(ElementName = "cTermDescarreg")]
        public string CTermDescarreg { get; set; }

        [XmlElement(ElementName = "xTermDescarreg")]
        public string XTermDescarreg { get; set; }
    }
}