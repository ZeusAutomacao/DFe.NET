using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfEmbComb
    {
        [XmlElement(ElementName = "cEmbComb")]
        public string CEmbComb { get; set; }
    }
}