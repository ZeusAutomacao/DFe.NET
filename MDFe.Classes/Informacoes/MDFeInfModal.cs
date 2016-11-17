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

        [XmlElement("rodo", typeof(MDFeRodo), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("aereo", typeof(MDFeAereo), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("aquav", typeof(MDFeAquav), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("ferrov", typeof(MDFeFerrov), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public MDFeModalContainer Modal { get; set; }
    }
}