using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpDoc
    {
        [XmlEnum("00")]
        Declaracao = 00,
        [XmlEnum("10")]
        Dutoviario = 10,
        [XmlEnum("59")]
        CFeSAT = 59,
        [XmlEnum("65")]
        NFCe = 65,
        [XmlEnum("99")]
        Outros = 99
    }
}