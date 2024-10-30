using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum mod
    {
        [XmlEnum("01")]
        NFModelo01A1eAvulsa,
        [XmlEnum("04")]
        NFdeProdutor
    }
}