using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum ferrEmi
    {
        [XmlEnum("1")]
        FerroviaDeOrigem = 1,
        [XmlEnum("2")]
        FerroviaDeDestino = 2
    }
}