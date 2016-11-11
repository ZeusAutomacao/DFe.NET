using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Flags
{
    public enum MDFeTpProp
    {
        [XmlEnum("0")]
        TacAgregado = 0,
        [XmlEnum("1")]
        TacIndependente = 1,
        [XmlEnum("2")]
        Outros = 2
    }
}