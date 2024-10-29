using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum CL
    {
        [XmlEnum("M")]
        TarifaMinima,
        [XmlEnum("G")]
        TarifaGeral,
        [XmlEnum("E")]
        TarifaEspecifica
    }
}