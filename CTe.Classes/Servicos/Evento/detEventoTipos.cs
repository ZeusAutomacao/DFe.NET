using System.Xml.Serialization;

namespace CTeDLL.Classes.Servicos.Evento
{
    /// <summary>
    ///     Informar "1=Empresa Emitente" para este evento.
    ///     Nota:
    ///     1=Empresa Emitente;
    ///     2=Empresa Destinatária;
    ///     3=Empresa;
    ///     5=Fisco;
    ///     6=RFB;
    ///     9=Outros Órgãos.
    /// </summary>
    public enum TipoAutor
    {
        [XmlEnum("1")] taEmpresaEmitente = 1,

        [XmlEnum("2")] taEmpresaDestinataria = 2,

        [XmlEnum("3")] taEmpresa = 3,

        [XmlEnum("5")] taFisco = 5,

        [XmlEnum("6")] taRFB = 6,

        [XmlEnum("9")] taOutrosOrgaos = 9
    }
}