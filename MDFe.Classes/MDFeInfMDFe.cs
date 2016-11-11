using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "ide")]
        public MDFeIde MdFeIde { get; set; }

    }
}
