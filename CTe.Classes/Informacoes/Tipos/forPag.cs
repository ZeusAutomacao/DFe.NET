using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum forPag
    {
        [XmlEnum("0")]
        Pago,
        [XmlEnum("1")]
        Apagar,
        [XmlEnum("2")]
        Outros
    }
}