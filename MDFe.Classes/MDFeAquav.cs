using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeAquav : IMDFeModal
    {
        [XmlElement(ElementName = "CNPJAgeNav")]
        public string CNPJAgeNav { get; set; }

        [XmlElement(ElementName = "tpEmb")]
        public byte TpEmb { get; set; }

        [XmlElement(ElementName = "cEmbar")]
        public string CEmbar { get; set; }

        [XmlElement(ElementName = "xEmbar")]
        public string XEmbar { get; set; }

        [XmlElement(ElementName = "nViag")]
        public string NViag { get; set; }

        [XmlElement(ElementName = "cPrtEmb")]
        public string CPrtEmb { get; set; }

        [XmlElement(ElementName = "cPrtDest")]
        public string CPrtDest { get; set; }

        [XmlElement(ElementName = "infTermCarreg")]
        public List<MDFeInfTermCarreg> InfTermCarregs { get; set; }

        [XmlElement(ElementName = "infTermDescarreg")]
        public List<MDFeInfTermDescarreg> InfTermDescarregs { get; set; }

        [XmlElement(ElementName = "infEmbComb")]
        public List<MDFeInfEmbComb> InfEmbCombs { get; set; }

        [XmlElement(ElementName = "infUnidCargaVazia")]
        public List<MDFeInfUnidCargaVazia> InfUnidCargaVazias { get; set; }
    }
}