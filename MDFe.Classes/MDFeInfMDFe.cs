using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        public MDFeInfMDFe()
        {
            Ide = new MDFeIde();
            Emit = new MDFeEmit();
            InfDoc = new MDFeInfDoc();
        }

        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "ide")]
        public MDFeIde Ide { get; set; }

        [XmlElement(ElementName = "emit")]
        public MDFeEmit Emit { get; set; }

        [XmlElement(ElementName = "infDoc")]
        public MDFeInfDoc InfDoc { get; set; }

    }
}
