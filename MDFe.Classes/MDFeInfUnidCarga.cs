using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfUnidCarga
    {
        [XmlElement(ElementName = "tpUnidCarga")]
        public MDFeTpUnidCarga TpUnidCarga { get; set; }

        [XmlElement(ElementName = "idUnidCarga")]
        public string IdUnidCarga { get; set; }

        [XmlElement(ElementName = "lacUnidCarga")]
        public List<MDFeLacUnidCarga> LacUnidCargas { get; set; }

        [XmlElement(ElementName = "qtdRat")]
        public decimal? QtdRat { get; set; }
    }
}