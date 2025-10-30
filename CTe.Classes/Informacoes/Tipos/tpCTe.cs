using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Tipo do Documento Fiscal (0 - CT-e Normal; 1 - CT-e de Complemento de Valores; 2 - CT-e de Anulação; 3 - CT-e Substituto; 5 - Simplificado; 6 - Substitudo Simplificado)
    /// </summary>
    public enum tpCTe
    {
        [XmlEnum("0")]
        Normal,
        [XmlEnum("1")]
        Complemento,
        [XmlEnum("2")]
        Anulacao,
        [XmlEnum("3")]
        Substituto,
        [XmlEnum("5")]
        Simplificado,
        [XmlEnum("6")]
        SubstitutoSimplificado
    }
}