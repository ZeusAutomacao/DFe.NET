using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpVeic
    {
        [XmlEnum("0")]
        Tracao = 0,
        [XmlEnum("1")]
        Reboque = 1
    }
}