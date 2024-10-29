using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpHor
    {
        [XmlEnum("0")]
        SemHoraDefinida,
        [XmlEnum("1")]
        NoHorario,
        [XmlEnum("2")]
        AteOHorario,
        [XmlEnum("3")]
        ApartirDoHorario,
        [XmlEnum("4")]
        NoIntervaloDeTempo
    }
}