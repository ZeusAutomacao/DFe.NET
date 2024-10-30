using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum respSeg
    {
        [XmlEnum("0")]
        Remetente = 0,
        [XmlEnum("1")]
        Expedidor = 1,
        [XmlEnum("2")]
        Recebedor = 2,
        [XmlEnum("3")]
        Destinatario = 3,
        [XmlEnum("4")]
        EmitenteDoCTe = 4,
        [XmlEnum("5")]
        TomadorDoServico = 5
    }
}