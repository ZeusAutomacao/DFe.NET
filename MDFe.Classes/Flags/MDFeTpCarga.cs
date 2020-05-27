using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeTpCarga
    {   [XmlEnum("01")]
        GranelSolido = 1,
        [XmlEnum("02")]
        GranelLiquido = 2,
        [XmlEnum("03")]
        Frigorificada = 3,
        [XmlEnum("04")]
        Conteinerizada = 4,
        [XmlEnum("05")]
        CargaGeral = 5,
        [XmlEnum("06")]
        Neogranel = 6,
        [XmlEnum("07")]
        PerigosaGranelSolido = 7,
        [XmlEnum("08")]
        PerigosaGranelLiquido = 8,
        [XmlEnum("09")]
        PerigosaCargaFrigorificada = 9,
        [XmlEnum("10")]
        PerigosaConteinerizada = 10,
        [XmlEnum("11")]
        PerigosaCargaGeral = 11
    }
}
