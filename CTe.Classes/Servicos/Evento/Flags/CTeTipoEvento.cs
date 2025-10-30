using System.Xml.Serialization;

namespace CTe.Classes.Servicos.Evento.Flags
{
    public enum CTeTipoEvento
    {
        //Tabela de Eventos MOC_CTe_VisaoGeral_v3.00a.pdf, página 33
        // Evento: Empresa Emitente
        [XmlEnum("110110")]
        CartaCorrecao = 110110,
        [XmlEnum("110111")]
        Cancelamento = 110111,
        [XmlEnum("110113")]
        EPEC = 110113,
        [XmlEnum("110160")]
        RegistrosdoMultimodal = 110160,
        [XmlEnum("110170")]
        InformacoesdaGTV = 110170,
        [XmlEnum("110180")]
        ComprovantedeEntrega = 110180,
        [XmlEnum("110181")]
        CancelamentodoComprovantedeEntrega = 110181,
        [XmlEnum("110190")]
        InsucessoNaEntregaDoCte = 110190,
        [XmlEnum("110191")]
        CancelamentodoInsucessoNaEntregaDoCte = 110191,
        //Evento: Fisco
        [XmlEnum("310620")]
        RegistrodePassagem = 310620,
        [XmlEnum("510620")]
        RegistrodePassagemAutomatico = 510620,
        [XmlEnum("510630")]
        RegistrodePassagemAutomatico2 = 510630,
        [XmlEnum("310610")]
        MDFeAutorizado = 310610,
        [XmlEnum("310611")]
        MDFeCancelado = 310611,
        // Evento: Fisco do Emitente
        [XmlEnum("240130")]
        AutorizadoCTecomplementar = 240130,
        [XmlEnum("240131")]
        CanceladoCTecomplementar = 240131,
        [XmlEnum("240140")]
        CTedeSubstituicao = 240140,
        [XmlEnum("240150")]
        CTedeAnulacao = 240150,
        [XmlEnum("240160")]
        LiberacaodeEPEC = 240160,
        [XmlEnum("240170")]
        LiberacaoPrazoCancelamento = 240170,
        // Evento: Ambiente Nacional 
        [XmlEnum("440130")]
        AutorizadoRedespacho = 440130,
        [XmlEnum("440140")]
        AutorizadoRedespachointermediario = 440140,
        [XmlEnum("440150")]
        AutorizadoSubcontratacao = 440150,
        [XmlEnum("440160")]
        AutorizadoServicoVinculadoMultimodal = 440160,
        // Evento: Tomador 
        [XmlEnum("610110")]
        Desacordo = 610110,
        [XmlEnum("610111")]
        CancelamentoPrestacaodoServicoemDesacordo = 610111        
    }
}