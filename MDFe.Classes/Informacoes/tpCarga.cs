using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum tpCarga
    {
        [XmlEnum("01")]
        GranelSolido = 01,

        [XmlEnum("02")]
        GranelLiquido = 02,

        [XmlEnum("03")]
        Frigorificada = 03,

        [XmlEnum("04")]
        Conteinerizada = 04,

        [XmlEnum("05")]
        CargaGeral = 05,

        [XmlEnum("06")]
        Neogranel = 06,

        [XmlEnum("07")]
        PerigosaGranelSolido = 07,

        [XmlEnum("08")]
        PerigosaGranelLiquido = 08,

        [XmlEnum("09")]
        PerigosaCargaFrigorificada = 09,

        [XmlEnum("10")]
        PerigosaCargaConteinerizada = 10,

        [XmlEnum("11")]
        PerigosaCargaGeral = 11,
    }
}