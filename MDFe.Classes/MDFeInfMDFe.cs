using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        public MDFeInfMDFe()
        {
            MDFeIde = new MDFeIde();
            MDFeEmit = new MDFeEmit();
        }

        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "ide")]
        public MDFeIde MDFeIde { get; set; }

        [XmlElement(ElementName = "emit")]
        public MDFeEmit MDFeEmit { get; set; }

    }
}
