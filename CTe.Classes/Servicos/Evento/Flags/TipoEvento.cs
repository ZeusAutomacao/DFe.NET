using System.Xml.Serialization;

namespace CTeDLL.Classes.Servicos.Evento.Flags
{
    public enum TipoEvento
    {
        [XmlEnum("110111")]
        Cancelamento = 110111,
        [XmlEnum("110110")]
        CartaCorrecao = 110110,
    }
}