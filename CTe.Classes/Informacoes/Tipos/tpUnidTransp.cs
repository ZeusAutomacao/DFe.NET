using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpUnidTransp
    {
        [XmlEnum("1")]
        RodoviarioTracao = 1,
        [XmlEnum("2")]
        RodoviarioReboque = 2,
        [XmlEnum("3")]
        Navio = 3,
        [XmlEnum("4")]
        Balsa = 4,
        [XmlEnum("5")]
        Aeronave = 5,
        [XmlEnum("6")]
        Vagao = 6,
        [XmlEnum("7")]
        Outros = 7
    }
}