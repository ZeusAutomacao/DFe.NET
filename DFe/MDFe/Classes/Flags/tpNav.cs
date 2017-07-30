using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Flags
{
    public enum tpNav
    {
        [XmlEnum("0")]
        Interior = 0,
        [XmlEnum("1")]
        Cabotagem = 1
    }
}