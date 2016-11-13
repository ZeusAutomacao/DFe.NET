using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    public class MDFeInfCTe
    {
        [XmlElement(ElementName = "chCTe")]
        public string ChCTe { get; set; }

        [XmlElement(ElementName = "SegCodBarra")]
        public string SegCodBarra { get; set; }

        [XmlElement(ElementName = "infUnidTransp")]
        public List<MDFeInfUnidTransp> InfUnidTransps { get; set; }

        [XmlElement(ElementName = "infNFe")]
        public List<MDFeInfNFe> InfNFe { get; set; }

        [XmlElement(ElementName = "infMDFeTransp")]
        public List<MDFeInfMDFeTransp> InfMdFeTransps { get; set; }
    }
}