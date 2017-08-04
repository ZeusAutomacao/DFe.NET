using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.CTe.Classes.Flags
{
    public enum versao
    {
        [XmlEnum("2.00")] ve200,
        [XmlEnum("3.00")] ve300
    }
}