using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeTpUnidCargaVazia
    {
        [XmlEnum("1")]
        Container = 1,
        [XmlEnum("2")]
        Carreta = 2
    }
}