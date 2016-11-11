using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfModal
    {
        [XmlAttribute(AttributeName = "versaoModal")]
        public MDFeVersaoModal VersaoModal { get; set; }

        [XmlElement("rodo", typeof(MDFeRodo))]
        [XmlElement("aereo", typeof(MDFeAereo))]
        [XmlElement("aquav", typeof(MDFeAquav))]
        [XmlElement("ferrov", typeof(MDFeFerrov))]
        public IMDFeModal Modal { get; set; }
    }
}