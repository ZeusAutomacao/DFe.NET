using System.Xml.Serialization;

namespace DFe.Classes.Flags
{
    public enum VersaoServico
    {
        [XmlEnum("1.00")]
        Versao100 = 100,

        [XmlEnum("2.00")]
        Versao200 = 200,

        [XmlEnum("3.00")]
        Versao300 = 300,

        [XmlEnum("3.10")]
        Versao310 = 310,

        [XmlEnum("4.00")]
        Versao400 = 400
    }
}