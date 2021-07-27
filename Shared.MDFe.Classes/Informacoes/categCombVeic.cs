using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum categCombVeic
    {
        [XmlEnum("02")]
        VeiculoComercial2Eixos = 02,

        [XmlEnum("03")]
        VeiculoComercial3Eixos = 03,

        [XmlEnum("04")]
        VeiculoComercial4Eixos = 04,

        [XmlEnum("05")]
        VeiculoComercial5Eixos = 05,

        [XmlEnum("06")]
        VeiculoComercial6Eixos = 06,

        [XmlEnum("07")]
        VeiculoComercial7Eixos = 07,

        [XmlEnum("08")]
        VeiculoComercial8Eixos = 08,

        [XmlEnum("09")]
        VeiculoComercial9Eixos = 09,

        [XmlEnum("10")]
        VeiculoComercial10Eixos = 10,
    }
}