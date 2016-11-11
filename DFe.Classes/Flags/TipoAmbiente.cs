using System.Xml.Serialization;

namespace DFe.Classes.Flags
{
    public enum TipoAmbiente
    {
        [XmlEnum("1")]
        Producao = 1,
        [XmlEnum("2")]
        Homologacao = 2
    }
}