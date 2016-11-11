using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeModal
    {
        [XmlEnum("1")]
        Rodoviario = 1,
        [XmlEnum("2")]
        Aereo = 2,
        [XmlEnum("3")]
        Aquaviario = 3,
        [XmlEnum("4")]
        Ferroviari = 4
    }
}