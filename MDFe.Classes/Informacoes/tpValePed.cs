using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum tpValePed
    {
        [XmlEnum("01")]
        Tag = 01,

        [XmlEnum("02")]
        Cupom = 02,

        [XmlEnum("03")]
        Cartao = 03
    }
}