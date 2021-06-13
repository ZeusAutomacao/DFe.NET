using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum tpNav
    {
        [XmlEnum("0")]
        Interior = 0,
        [XmlEnum("1")]
        Cabotagem = 1
    }
}