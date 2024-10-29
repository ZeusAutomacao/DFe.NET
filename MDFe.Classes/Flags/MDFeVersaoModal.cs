using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeVersaoModal
    {
        [XmlEnum("1.00")]
        Versao100 = 100,

        [XmlEnum("3.00")]
        Versao300 = 300
    }
}