using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum lota
    {
        [XmlEnum("0")]
        Nao = 0,
        [XmlEnum("1")]
        Sim = 1
    }
}