using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    public enum tpUnidTranspVazia
    {
        [XmlEnum("1")]
        RodoviarioTracaoCaminhao = 1,
        [XmlEnum("2")]
        RodoviarioReboqueCarrega = 2
    }
}