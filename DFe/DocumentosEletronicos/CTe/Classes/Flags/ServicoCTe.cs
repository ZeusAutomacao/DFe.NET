namespace DFe.DocumentosEletronicos.CTe.Classes.Flags
{
    public enum ServicoCTe
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
        ///     serviço destinado à recepção de mensagem do Evento de Manifestação do destinatário da NF-e
        /// </summary>
        RecepcaoEventoManifestacaoDestinatario,

        /// <summary>
        ///     serviço destinado à recepção de mensagens de lote de NF-e versão 2.0
        /// </summary>
        CteRecepcao,

        /// <summary>
        ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 2.0
        /// </summary>
        CteRetRecepcao,

        /// <summary>
        ///     Serviço para consultar o cadastro de contribuintes do ICMS da unidade federada
        /// </summary>
        CteConsultaCadastro,

        /// <summary>
        ///     serviço destinado ao atendimento de solicitações de inutilização de numeração
        /// </summary>
        CteInutilizacao,

        /// <summary>
        ///     serviço destinado ao atendimento de solicitações de consulta da situação atual da NF-e
        ///     na Base de Dados do Portal da Secretaria de Fazenda Estadual
        /// </summary>
        CteConsultaProtocolo,

        /// <summary>
        ///     serviço destinado à consulta do status do serviço prestado pelo Portal da Secretaria de Fazenda Estadual
        /// </summary>
        CteStatusServico,

        /// <summary>
        ///     serviço destinado à recepção de mensagens de lote de NF-e versão 3.10
        /// </summary>
        CteAutorizacao,

        /// <summary>
        ///     serviço destinado a retornar o resultado do processamento do lote de NF-e versão 3.10
        /// </summary>
        CteRetAutorizacao,

        /// <summary>
        ///     Distribui documentos e informações de interesse do ator da NF-e
        /// </summary>
        CteDistribuicaoDFe,

        /// <summary>
        ///     “Serviço de Consulta da Relação de Documentos Destinados” para um determinado CNPJ
        ///     de destinatário informado na NF-e.
        /// </summary>
        CteConsultaDest,

        /// <summary>
        ///     Serviço destinado ao atendimento de solicitações de download de Notas Fiscais Eletrônicas por seus destinatários
        /// </summary>
        CteDownloadNF
    }
}