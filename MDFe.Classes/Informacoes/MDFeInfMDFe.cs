using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        public MDFeInfMDFe()
        {
            Ide = new MDFeIde();
            Emit = new MDFeEmit();
            InfModal = new MDFeInfModal();
            InfDoc = new MDFeInfDoc();
            Tot = new MDFeTot();
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoLayout Versao { get; set; } = VersaoLayout.Versao100;

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "ide")]
        public MDFeIde Ide { get; set; }

        [XmlElement(ElementName = "emit")]
        public MDFeEmit Emit { get; set; }

        [XmlElement(ElementName = "infModal")]
        public MDFeInfModal InfModal { get; set; }

        [XmlElement(ElementName = "infDoc")]
        public MDFeInfDoc InfDoc { get; set; }

        [XmlElement(ElementName = "tot")]
        public MDFeTot Tot { get; set; }

        [XmlElement(ElementName = "lacres")]
        public List<MDFeLacre> Lacres { get; set; }

        [XmlElement(ElementName = "autXML")]
        public List<MDFeAutXML> AutXml { get; set; }

        [XmlElement(ElementName = "infAdic")]
        public MDFeInfAdic InfAdic { get; set; }
    }
}
