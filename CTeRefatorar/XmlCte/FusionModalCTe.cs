using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionModalCTe
    {
        [XmlEnum("01")]
        Rodoviario = 01,

        [XmlEnum("02")]
        Aereo = 02,

        [XmlEnum("03")]
        Aquaviario = 03,

        [XmlEnum("04")]
        Ferroviario = 04,

        [XmlEnum("05")]
        Dutoviario = 05,

        [XmlEnum("06")]
        Multimodal = 06
    }
}