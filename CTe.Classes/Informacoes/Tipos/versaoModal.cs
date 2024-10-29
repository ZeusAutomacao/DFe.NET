using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum versaoModal
    {
        [XmlEnum("2.00")]
        veM200,
        [XmlEnum("3.00")]
        veM300,
        [XmlEnum("4.00")]
        veM400
    }
}