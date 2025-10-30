using System.Xml.Serialization;

namespace MDFe.Utils.Flags
{
    public enum VersaoServico
    {
        [XmlEnum("1.00")]
        Versao100 = 100,

        [XmlEnum("3.00")]
        Versao300 = 300
    }
}