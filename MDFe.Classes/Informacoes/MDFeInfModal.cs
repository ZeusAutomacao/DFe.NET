using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfModal
    {
        [XmlAttribute(AttributeName = "versaoModal")]
        public MDFeVersaoModal VersaoModal { get; set; } = MDFeVersaoModal.Versao100;

        [XmlElement("rodo", typeof(MDFeRodo))]
        [XmlElement("aereo", typeof(MDFeAereo))]
        [XmlElement("aquav", typeof(MDFeAquav))]
        [XmlElement("ferrov", typeof(MDFeFerrov))]
        public MDFeModalContainer Modal { get; set; }
    }
}