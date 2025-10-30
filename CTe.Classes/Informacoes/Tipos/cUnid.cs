using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum cUnid
    {
        [XmlEnum("00")]
        M3 = 00,
        [XmlEnum("01")]
        KG = 01,
        [XmlEnum("02")]
        TON = 02,
        [XmlEnum("03")]
        UNIDADE = 03,
        [XmlEnum("04")]
        LITROS = 04,
        [XmlEnum("05")]
        MMBTU = 05
    }
}