using System;
using System.Xml.Serialization;

namespace MDFe.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "infMDFe")]
    public class MDFeInfMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; }
    }
}
