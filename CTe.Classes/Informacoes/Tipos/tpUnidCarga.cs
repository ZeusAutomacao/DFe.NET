using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpUnidCarga
    {
        [XmlEnum("1")]
        Container = 1,
        [XmlEnum("2")]
        ULD = 2,
        [XmlEnum("3")]
        Pallet = 3,
        [XmlEnum("4")]
        Outros = 4
    }
}