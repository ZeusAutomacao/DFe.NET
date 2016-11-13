using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeLacre
    {
        [XmlElement(ElementName = "nLacre")]
        public string NLacre { get; set; }
    }
}