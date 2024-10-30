using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpRod
    {
        [XmlEnum("00")]
        NaoAplicavel = 00,
        [XmlEnum("01")]
        Truck = 01,
        [XmlEnum("02")]
        Toco = 02,
        [XmlEnum("03")]
        CavaloMecanico = 03,
        [XmlEnum("04")]
        Van = 04,
        [XmlEnum("05")]
        Utilitario = 05,
        [XmlEnum("06")]
        Outros = 06
    }
}