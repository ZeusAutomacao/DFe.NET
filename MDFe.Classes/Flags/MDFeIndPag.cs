using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum MDFeIndPag
    {
        [XmlEnum("0")]
        PagamentoVista = 0,
        [XmlEnum("1")]
        PagamentoPrazo = 1
    }
}
