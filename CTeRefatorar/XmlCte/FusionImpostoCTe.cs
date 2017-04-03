using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionImpostoCTe
    {
        private decimal? _totalTributosFederais;

        [XmlElement(ElementName = "ICMS")]
        public FusionIcmsCTe FusionIcmsCTe { get; set; }

        [XmlElement(ElementName = "vTotTrib")]
        public decimal? TotalTributosFederais
        {
            get { return _totalTributosFederais; }
            set { _totalTributosFederais = value; }
        }

        public bool TotalTributosFederaisSpecified => _totalTributosFederais.HasValue;

        public FusionImpostoCTe()
        {
            FusionIcmsCTe = new FusionIcmsCTe();
        }
    }

    [Serializable]
    public class FusionIcmsCTe
    {
        [XmlElement(ElementName = "ICMSSN")]
        public FusionIcmsSimplesNacionalCTe IcmsSimplesNacional { get; set; }

        public FusionIcmsCTe()
        {
            IcmsSimplesNacional = new FusionIcmsSimplesNacionalCTe();
        }
    }

    [Serializable]
    public class FusionIcmsSimplesNacionalCTe
    {
        [XmlElement(ElementName = "indSN")]
        public int IndicadorSimplesNacional { get; set; }

        public FusionIcmsSimplesNacionalCTe()
        {
            IndicadorSimplesNacional = 1;
        }
    }
}