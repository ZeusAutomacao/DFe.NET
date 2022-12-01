using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum categCombVeic
    {
        [XmlEnum("02")]
        VeiculoComercial2Eixos = 02,

        [XmlEnum("04")]
        VeiculoComercial3Eixos = 03,

        [XmlEnum("06")]
        VeiculoComercial4Eixos = 04,

        [XmlEnum("07")]
        VeiculoComercial5Eixos = 05,

        [XmlEnum("08")]
        VeiculoComercial6Eixos = 06,

        [XmlEnum("10")]
        VeiculoComercial7Eixos = 07,

        [XmlEnum("11")]
        VeiculoComercial8Eixos = 08,

        [XmlEnum("12")]
        VeiculoComercial9Eixos = 09,

        [XmlEnum("13")]
        VeiculoComercial10Eixos = 10,

        [XmlEnum("14")]
        VeiculoComercialAcimaDe10Eixos = 14
    }
}