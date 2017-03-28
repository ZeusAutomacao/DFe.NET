using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionSeguroCTe
    {
        [XmlElement(ElementName = "respSeg")]
        public FusionResponsavelSeguroCTe ResponsavelSeguro { get; set; }

        [XmlElement(ElementName = "xSeg")]
        public string NomeSeguradora { get; set; }

        [XmlElement(ElementName = "nApol")]
        public string NumeroApolice { get; set; }

        [XmlElement(ElementName = "nAver")]
        public string NumeroAverbacao { get; set; }

        [XmlElement(ElementName = "vCarga")]
        public decimal? ValorCarga { get; set; }
    }
}