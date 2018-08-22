using System.Xml.Serialization;

namespace SMDFe.Classes.Flags
{

    public enum MDFeTpTransp
    {
        [XmlEnum("1")]
        ETC = 1,
        [XmlEnum("2")]
        TAC = 2,
        [XmlEnum("3")]
        CTC = 3
    }
}