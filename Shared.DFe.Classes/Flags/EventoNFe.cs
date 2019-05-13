using System.Xml.Serialization;

namespace DFe.Classes.Flags
{
    /// <summary>
    ///     Código de eventos da Nota Fiscal Eletrônica. Ex: Cancelamento e Carta de Correção
    /// </summary>
    public enum EventoNFe
    {
        [XmlEnum("110111")]
        Cancelamento = 110111,
        [XmlEnum("110110")]
        CartaCorrecao = 110110
    }
}
