using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeTipoEmissao
    {
        [XmlEnum("1")]
        Normal = 1,
        [XmlEnum("2")]
        Contingencia = 2
    }
}