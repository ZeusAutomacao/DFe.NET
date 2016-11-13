using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags
{
    public enum VersaoServico
    {
        [XmlEnum("1.00")]
        Versao100 = 1
    }
}