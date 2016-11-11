using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMunDescarga
    {
        [XmlElement(ElementName = "cMunDescarga")]
        public string CMunDescarga { get; set; }

        [XmlElement(ElementName = "xMunDescarga")]
        public string XMunDescarga { get; set; }
    }
}