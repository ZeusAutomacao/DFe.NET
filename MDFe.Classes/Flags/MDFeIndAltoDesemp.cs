using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeIndAltoDesemp
    {
        [XmlEnum("0")]
        NaoAltoDesempenho = 0,
        [XmlEnum("1")]
        AltoDesempenho = 1
    }
}
