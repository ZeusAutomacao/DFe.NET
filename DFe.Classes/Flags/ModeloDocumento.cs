using System.Xml.Serialization;

namespace DFe.Classes.Flags
{
    /// <summary>
    ///     Código do modelo do Documento Fiscal. 55 = NF-e; 58 = MDFe; 65 = NFC-e.
    /// </summary>
    public enum ModeloDocumento
    {
        [XmlEnum("55")]
        NFe = 55,
        [XmlEnum("58")]
        MDFe = 58,
        [XmlEnum("65")]
        NFCe = 65,
        [XmlEnum("57")]
        CTe = 57,
        [XmlEnum("67")]
        CTeOS = 67
    }
}