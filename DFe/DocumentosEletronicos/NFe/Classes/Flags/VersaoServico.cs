using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.NFe.Classes.Flags
{
    public enum VersaoServico
    {
        [XmlEnum("1.00")] ve100,
        [XmlEnum("2.00")] ve200,
        [XmlEnum("3.10")] ve310,
        [XmlEnum("4.00")] ve400
    }
}