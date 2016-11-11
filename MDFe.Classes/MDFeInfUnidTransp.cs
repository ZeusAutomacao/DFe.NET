using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfUnidTransp
    {
        [XmlElement(ElementName = "tpUnidTransp")]
        public MDFeTpUnidTransp TpUnidTransp { get; set; }

        [XmlElement(ElementName = "idUnidTransp")]
        public string IdUnidTransp { get; set; }

        [XmlElement(ElementName = "lacUnidTransp")]
        public List<MDFeLacUnidTransp> LacUnidTransps { get; set; }

        [XmlElement(ElementName = "infUnidCarga")]
        public List<MDFeInfUnidCarga> InfUnidCargas { get; set; }

        [XmlElement(ElementName = "qtdRat")]
        public decimal? QtdRat { get; set; }
    }
}