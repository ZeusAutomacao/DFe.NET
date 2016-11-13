using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfAdic
    {
        [XmlElement(ElementName = "infAdFisco")]
        public string InfAdFisco { get; set; }

        [XmlElement(ElementName = "infCpl")]
        public string InfCpl { get; set; }
    }
}