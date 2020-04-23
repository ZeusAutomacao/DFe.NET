using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    public enum indPag
    {
        [XmlEnum("0")]
        PagamentoAVista = 0,

        [XmlEnum("1")]
        PagamentoAPrazo = 1
    }
}