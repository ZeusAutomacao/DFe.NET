using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeTpRod
    {
        [XmlEnum("01")]
        Truck = 01,
        [XmlEnum("02")]
        Toco = 02,
        [XmlEnum("03")]
        CavaloMecanico = 03,
        [XmlEnum("04")]
        VAN = 04,
        [XmlEnum("05")]
        Utilitario = 05,
        [XmlEnum("06")]
        Outros = 06
    }
}