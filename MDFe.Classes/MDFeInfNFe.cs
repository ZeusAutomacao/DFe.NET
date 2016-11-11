using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfNFe
    {
        [XmlElement(ElementName = "chNFe")]
        public string ChNFe { get; set; }

        [XmlElement(ElementName = "SegCodBarra")]
        public string SegCodBarra { get; set; }

        [XmlElement(ElementName = "infUnidTransp")]
        public List<MDFeInfUnidTransp> InfUnidTransps { get; set; }
    }
}