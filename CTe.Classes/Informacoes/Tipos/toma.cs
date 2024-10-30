using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum toma
    {
        [XmlEnum("0")]
        Remetente,
        [XmlEnum("1")]
        Expedidor,
        [XmlEnum("2")]
        Recebedor,
        [XmlEnum("3")]
        Destinatario,
        [XmlEnum("4")]
        Outros
    }
}