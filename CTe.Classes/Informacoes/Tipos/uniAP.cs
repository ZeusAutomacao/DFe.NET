using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum uniAP
    {
        [XmlEnum("1")]
        KG = 1,
        [XmlEnum("2")]
        KGG = 2,
        [XmlEnum("3")]
        LITROS = 3,
        [XmlEnum("4")]
        TI = 4,
        [XmlEnum("5")]
        Unidades = 5
    }
}