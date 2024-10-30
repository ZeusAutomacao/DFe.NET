using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpPropProp
    {
        [XmlEnum("0")]
        TACAgregado = 0,
        [XmlEnum("1")]
        TACIndependente = 1,
        [XmlEnum("2")]
        Outros = 2
    }
}