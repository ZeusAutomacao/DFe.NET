using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpPer
    {
        [XmlEnum("0")]
        SemDataDefinida,
        [XmlEnum("1")]
        NaData,
        [XmlEnum("2")]
        AteAData,
        [XmlEnum("3")]
        ApartirDaData,
        [XmlEnum("4")]
        NoPeriodo
    }
}