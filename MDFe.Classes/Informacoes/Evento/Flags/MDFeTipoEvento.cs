using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento.Flags
{
    public enum MDFeTipoEvento
    {
        [XmlEnum("110111")]
        Cancelamento = 110111,
        [XmlEnum("110112")]
        Encerramento = 110112,
        [XmlEnum("110114")]
        InclusaoDeCondutor = 110114,
        [XmlEnum("110115")]
        InclusaoDFe = 110115,
        [XmlEnum("310620")]
        RegistroDePassagem = 310620,
        [XmlEnum("510620")]
        RegistroDePassagemAutomatico = 510620,
        [XmlEnum("110116")]
        PagamentoOperacaoMDFe = 110116,
    }
}