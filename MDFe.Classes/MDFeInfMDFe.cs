using System;
using System.Xml.Serialization;

namespace MDFe.Classes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; }
    }
}
