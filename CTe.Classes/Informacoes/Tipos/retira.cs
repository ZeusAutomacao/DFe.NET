using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum retira
    {
        [XmlEnum("0")]
        Sim = 0,
        [XmlEnum("1")]
        Nao = 1
    }
}