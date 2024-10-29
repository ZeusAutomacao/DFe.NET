using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpNav
    {
        [XmlEnum("0")]
        Interior = 0,
        [XmlEnum("1")]
        Cabotagem = 1
    }
}