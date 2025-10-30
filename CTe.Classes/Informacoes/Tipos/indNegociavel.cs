using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum indNegociavel
    {
        [XmlEnum("0")]
        NaoNegociavel = 0,
        [XmlEnum("1")]
        Negociavel = 1
    }
}