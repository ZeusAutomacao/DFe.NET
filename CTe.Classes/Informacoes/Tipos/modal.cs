using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum modal
    {
        [XmlEnum("01")]
        rodoviario = 01,
        [XmlEnum("02")]
        aereo = 02,
        [XmlEnum("03")]
        aquaviario = 03,
        [XmlEnum("04")]
        ferroviario = 04,
        [XmlEnum("05")]
        dutoviario = 05,
        [XmlEnum("06")]
        multimodal = 06        
    }
}