using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Flags
{

    public enum tpTransp
    {
        [XmlEnum("1")]
        ETC = 1,
        [XmlEnum("2")]
        TAC = 2,
        [XmlEnum("3")]
        CTC = 3
    }
}