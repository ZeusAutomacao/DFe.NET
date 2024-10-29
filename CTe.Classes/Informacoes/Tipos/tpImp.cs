using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Formato de impressão do DACTE (1-DANFe Retrato; 2-DANFe Paisagem)
    /// </summary>
    public enum tpImp
    {
        [XmlEnum("1")]
        Retrado = 1,
        [XmlEnum("2")]
        Paisagem = 2
    }
}