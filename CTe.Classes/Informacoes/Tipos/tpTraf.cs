using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpTraf
    {
        [XmlEnum("0")]
        Proprio = 0,
        [XmlEnum("1")]
        Mutuo = 1,
        [XmlEnum("2")]
        Rodoferroviario = 2,
        [XmlEnum("3")]
        Rodoviario = 3
    }
}