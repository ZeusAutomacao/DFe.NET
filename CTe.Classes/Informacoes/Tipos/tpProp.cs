using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpProp
    {
        [XmlEnum("P")]
        Proprio,
        [XmlEnum("T")]
        Terceiro
    }
}