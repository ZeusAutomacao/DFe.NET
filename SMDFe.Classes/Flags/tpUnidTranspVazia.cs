using System.Xml.Serialization;

namespace SMDFe.Classes.Flags
{
    public enum tpUnidTranspVazia
    {
        [XmlEnum("1")]
        RodoviarioTracaoCaminhao = 1,
        [XmlEnum("2")]
        RodoviarioReboqueCarrega = 2
    }
}