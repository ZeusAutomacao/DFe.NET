using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeCategCombVeic
    {
        [XmlEnum("02")]
        VeicComerc2Eixos = 2,
        [XmlEnum("04")]
        VeicComerc3Eixos = 4,
        [XmlEnum("06")]
        VeicComerc4Eixos = 6,
        [XmlEnum("07")]
        VeicComerc5Eixos = 7,
        [XmlEnum("08")]
        VeicComerc6Eixos = 8,
        [XmlEnum("10")]
        VeicComerc7Eixos = 10,
        [XmlEnum("11")]
        VeicComerc8Eixos = 11,
        [XmlEnum("12")]
        VeicComerc9Eixos = 12,
        [XmlEnum("13")]
        VeicComerc10Eixos = 13,
        [XmlEnum("14")]
        VeicComerc10MaisEixos = 14,
    }
}
