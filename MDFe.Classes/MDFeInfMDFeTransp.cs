using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMDFeTransp
    {
        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        [XmlElement(ElementName = "infUnidTransp")]
        public List<MDFeInfUnidTransp> InfUnidTransp { get; set; }
    }
}