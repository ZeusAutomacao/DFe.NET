using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RegistroEventos
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "retEventoCTe")]
    public class FusionRetornoEventoCancelamentoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento")]
        public FusionInfoEventoCancelamentoCTe FusionInfoEventoCancelamentoCTe { get; set; }
    }

    public class FusionInfoEventoCancelamentoCTe
    {
    }
}