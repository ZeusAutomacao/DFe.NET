using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.Flags
{
    public enum MDFeTipoEvento
    {
        [XmlEnum("110111")]
        Cancelamento = 110111,
        [XmlEnum("110112")]
        Encerramento = 110112,
        [XmlEnum("110114")]
        InclusaoDeCondutor = 110114,
        [XmlEnum("310620")]
        RegistroDePassagem = 310620
    }
}