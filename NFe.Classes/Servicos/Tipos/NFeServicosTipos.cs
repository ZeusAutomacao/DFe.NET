using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Tipos
{
    public enum ServicoNFe
    {
        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento de Cancelamento da NF-e
        /// </summary>
        RecepcaoEventoCancelmento,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento de Carta de Correção da NF-e
        /// </summary>
        RecepcaoEventoCartaCorrecao,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento EPEC da NF-e
        /// </summary>
        RecepcaoEventoEpec,
        
        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Insucesso na Entrega da NFe
        /// </summary>
        RecepcaoEventoInsucessoEntregaNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Cancelamento Insucesso na Entrega da NFe
        /// </summary>
        RecepcaoEventoCancInsucessoEntregaNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Comprovante de Entrega da NFe
        /// </summary>
        RecepcaoEventoComprovanteEntregaNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Cancelamento do Comprovante de Entrega da NFe
        /// </summary>
        RecepcaoEventoCancComprovanteEntregaNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Conciliação Financeira da NFe
        /// </summary>
        RecepcaoEventoConciliacaoFinanceiraNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento Cancelamento de Conciliação Financeira da NFe
        /// </summary>
        RecepcaoEventoCancConciliacaoFinanceiraNFe,

        /// <summary>
        ///     serviço destinado à recepção de mensagem do Evento de Manifestação do destinatário da NF-e
        /// </summary>
        RecepcaoEventoManifestacaoDestinatario,

        /// <summary>
        ///     serviço destinado à recepção de mensagens de lote de NF-e versão 2.0
        /// </summary>
        NfeRecepcao,

        /// <summary>
        ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 2.0
        /// </summary>
        NfeRetRecepcao,

        /// <summary>
        ///     Serviço para consultar o cadastro de contribuintes do ICMS da unidade federada
        /// </summary>
        NfeConsultaCadastro,

        /// <summary>
        ///     serviço destinado ao atendimento de solicitações de inutilização de numeração
        /// </summary>
        NfeInutilizacao,

        /// <summary>
        ///     serviço destinado ao atendimento de solicitações de consulta da situação atual da NF-e
        ///     na Base de Dados do Portal da Secretaria de Fazenda Estadual
        /// </summary>
        NfeConsultaProtocolo,

        /// <summary>
        ///     serviço destinado à consulta do status do serviço prestado pelo Portal da Secretaria de Fazenda Estadual
        /// </summary>
        NfeStatusServico,

        /// <summary>
        ///     serviço destinado à recepção de mensagens de lote de NF-e versão 3.10
        /// </summary>
        NFeAutorizacao,

        /// <summary>
        ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 3.10
        /// </summary>
        NFeRetAutorizacao,

        /// <summary>
        ///     Distribui documentos e informações de interesse do ator da NF-e
        /// </summary>
        NFeDistribuicaoDFe,

        /// <summary>
        ///     “Serviço de Consulta da Relação de Documentos Destinados” para um determinado CNPJ
        ///     de destinatário informado na NF-e.
        /// </summary>
        NfeConsultaDest,

        /// <summary>
        ///     Serviço destinado ao atendimento de solicitações de download de Notas Fiscais Eletrônicas por seus destinatários
        /// </summary>
        NfeDownloadNF,

        /// <summary>
        ///     Serviço destinado a administração do CSC.
        /// </summary>
        NfceAdministracaoCSC,

        ConsultaGtin
    }

    /// <summary>
    ///     Indicador de Sincronização:
    ///     <para>0 - Assíncrono. A resposta deve ser obtida consultando o serviço NFeRetAutorizacao, com o nº do recibo</para>
    ///     <para>1 - Síncrono. Empresa solicita processamento síncrono do Lote de NF-e (sem a geração de Recibo para consulta futura);</para>
    /// </summary>
    public enum IndicadorSincronizacao
    {
        /// <summary>
        /// 0 - Assíncrono. A resposta deve ser obtida consultando o serviço NFeRetAutorizacao, com o nº do recibo
        /// </summary>
        [Description("Assíncrono. A resposta deve ser obtida consultando o serviço NFeRetAutorizacao, com o nº do recibo")]
        [XmlEnum("0")]
        Assincrono = 0,

        /// <summary>
        /// 1 - Síncrono. Empresa solicita processamento síncrono do Lote de NF-e (sem a geração de Recibo para consulta futura)
        /// </summary>
        [Description("Síncrono. Empresa solicita processamento síncrono do Lote de NF-e (sem a geração de Recibo para consulta futura)")]
        [XmlEnum("1")]
        Sincrono = 1
    }

    /// <summary>
    ///     Código do Tipo do Evento.
    ///     110110 - Carta de Correção
    ///     110140 - EPEC
    ///     110111 - Cancelamento
    ///     110112 - Cancelamento por substituição
    ///     110130 - Comprovante de Entrega da NF-e
    ///     110131 - Cancelamento Comprovante de Entrega da NF-e
    ///     110192 - Insucesso na Entrega da NF-e
    ///     110193 - Cancelamento Insucesso na Entrega da NF-e
    ///     110750 - Conciliação Financeira da NF-e
    ///     110751 - Cancelamento Conciliação Financeira da NF-e
    ///     210200 – Confirmação da Operação
    ///     210210 – Ciência da Emissão
    ///     210220 – Desconhecimento da Operação
    ///     210240 – Operação não Realizada
    /// </summary>
    public enum NFeTipoEvento
    {
        /// <summary>
        /// 110110 - Carta de Correção
        /// </summary>
        [Description("Carta de Correção")]
        [XmlEnum("110110")]
        TeNfeCartaCorrecao = 110110,

        /// <summary>
        /// 110140 - EPEC
        /// </summary>
        [Description("EPEC")]
        [XmlEnum("110140")]
        TeNfceEpec = 110140,

        /// <summary>
        /// 110111 - Cancelamento
        /// </summary>
        [Description("Cancelamento")]
        [XmlEnum("110111")]
        TeNfeCancelamento = 110111,

        /// <summary>
        /// 110112 - Cancelamento por substituição
        /// </summary>
        [Description("Cancelamento por substituicao")]
        [XmlEnum("110112")]
        TeNfeCancelamentoSubstituicao = 110112,
        
        /// <summary>
        /// 110192 - Insucesso na Entrega da NF-e
        /// </summary>
        [Description("Insucesso na Entrega da NF-e")]
        [XmlEnum("110192")]
        TeNfeInsucessoNaEntregadaNFe = 110192,

        /// <summary>
        /// 110193 - Cancelamento Insucesso na Entrega da NF-e
        /// </summary>
        [Description("Cancelamento Insucesso na Entrega da NF-e")]
        [XmlEnum("110193")]
        TeNfeCancInsucessoNaEntregadaNFe = 110193,

        /// <summary>
        /// 110130 - Comprovante de Entrega da NF-e
        /// </summary>
        [Description("Comprovante de Entrega da NF-e")]
        [XmlEnum("110130")]
        TeNfeComprovanteDeEntregadaNFe = 110130,

        /// <summary>
        /// 110131 - Cancelamento Comprovante de Entrega da NF-e
        /// </summary>
        [Description("Cancelamento Comprovante de Entrega da NF-e")]
        [XmlEnum("110131")]
        TeNfeCancComprovanteDeEntregadaNFe = 110131,

        /// <summary>
        /// 110750 - Conciliação Financeira da NF-e
        /// </summary>
        [Description("ECONF")]
        [XmlEnum("110750")]
        TeNfeConciliacaoFinanceiraNFe = 110750,

        /// <summary>
        /// 110751 - Cancelamento Conciliação Financeira da NF-e
        /// </summary>
        [Description("Cancelamento Conciliação Financeira")]
        [XmlEnum("110751")]
        TeNfeCancConciliacaoFinanceiraNFe = 110751,

        /// <summary>
        /// 210200 – Confirmação da Operação
        /// </summary>
        [Description("Confirmacao da Operacao")]
        [XmlEnum("210200")]
        TeMdConfirmacaoDaOperacao = 210200,

        /// <summary>
        /// 210210 – Ciência da Operação
        /// </summary>
        [Description("Ciencia da Operacao")]
        [XmlEnum("210210")]
        TeMdCienciaDaOperacao = 210210,

        /// <summary>
        /// 210220 – Desconhecimento da Operação
        /// </summary>
        [Description("Desconhecimento da Operacao")]
        [XmlEnum("210220")]
        TeMdDesconhecimentoDaOperacao = 210220,

        /// <summary>
        /// 210240 – Operação não Realizada
        /// </summary>
        [Description("Operacao nao Realizada")]
        [XmlEnum("210240")]
        TeMdOperacaoNaoRealizada = 210240,

        /// <summary>
        /// 610130 – Comprovante de entrega CTe
        /// </summary>
        [Description("Comprovante de entrega CTe")]
        [XmlEnum("610130")]
        ComprovanteEntregaCTe = 610130,

        /// <summary>
        /// 610131 – Cancelamento de Comprovante de entrega CTe
        /// </summary>
        [Description("Cancelamento de entrega CTe")]
        [XmlEnum("610131")]
        CancelamentoComprovanteEntregaCTe = 610131,

        /// <summary>
        /// 790700 – Averbação para Exportação
        /// </summary>
        [Description("Averbação para Exportação")]
        [XmlEnum("790700")]
        TeMdAverbacaoparaExportacao = 790700
    }

    /// <summary>
    /// Classe com métodos para tratamento do enum <see cref="NFeTipoEvento"/>
    /// </summary>
    public class NFeTipoEventoUtils
    {
        /// <summary>
        /// Devolve a lista de enums do tipo <see cref="NFeTipoEvento"/> que são utilizados para manifestação do destinatário/>
        /// </summary>
        public static ISet<NFeTipoEvento> NFeTipoEventoManifestacaoDestinatario = new HashSet<NFeTipoEvento>()
        {
            NFeTipoEvento.TeMdConfirmacaoDaOperacao, NFeTipoEvento.TeMdCienciaDaOperacao,
            NFeTipoEvento.TeMdDesconhecimentoDaOperacao, NFeTipoEvento.TeMdOperacaoNaoRealizada
        };

        /// <summary>
        /// Devolve a lista de enums do tipo <see cref="NFeTipoEvento"/> que são utilizados para cancelamento/>
        /// </summary>
        public static ISet<NFeTipoEvento> NFeTipoEventoCancelamento = new HashSet<NFeTipoEvento>()
        {
            NFeTipoEvento.TeNfeCancelamento, NFeTipoEvento.TeNfeCancelamentoSubstituicao
        };
    }
}