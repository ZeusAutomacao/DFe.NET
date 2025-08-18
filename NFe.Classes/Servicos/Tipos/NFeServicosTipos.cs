/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

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
        ///     serviço destinado à recepção de mensagem do Evento de Ator Interessado da NF-e
        /// </summary>
        RecepcaoEventoAtorInteressado,

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

        ConsultaGtin,
        
        /// <summary>
        ///     Serviço destinado a permitir que o emitente da NFe informe o efetivo pagamento integral a fim de liberar crédito presumido do adquirente
        /// </summary>
        RecepcaoEventoInformacaoDeEfetivoPagamentoIntegralParaLiberarCreditoPresumidoDoAdquirente,
        
        /// <summary>
        ///     Serviço destinado a evento a ser gerado pelo adquirente em relação às notas fiscais de aquisição de emissão de terceiros e que lhe gerem o direito à apropriação de crédito presumido
        /// </summary>
        RecepcaoEventoSolicitacaoDeApropriacaoDeCreditoPresumido,
        
        /// <summary>
        ///     Serviço para permitir ao adquirente informar quando uma aquisição for destinada para o consumo de pessoa física, hipótese em que não haverá direito à 
        ///     apropriação de crédito. Evento a ser registrado após a emissão da nota de bens destinados para uso e consumo pessoal. 
        ///     Uma mesma NFe de aquisição pode receber vários Eventos desse tipo, com nSeqEvento diferentes (eventos cumulativos).
        /// </summary>
        RecepcaoEventoDestinacaoDeItemParaConsumoPessoal,
        
        /// <summary>
        ///     Serviço para Permitir ao destinatário informar que concorda com os valores constantes em nota de crédito emitida pelo fornecedor ou pelo adquirente que
        ///     serão lançados a débito na apuração assistida de IBS e CBS
        /// </summary>
        RecepcaoEventoAceiteDeDebitoNaApuracaoPorEmissaoDeNotaDeCredito,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pelo adquirente de bem, quando este for integrado ao seu ativo imobilizado, a fim de viabilizar a adequada identificação, 
        ///     pelos sistemas da administração tributária, de prazo-limite para apreciação de eventuais pedidos de ressarcimento do respectivo crédito, nos termos
        ///     do art. 40, I da LC 214/2025.
        /// </summary>
        RecepcaoEventoImobilizacaoDeItem,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pelo adquirente de combustível listado no art. 172 da LC 214/2025 e que pertença à cadeia produtiva desses combustíveis,
        ///     para solicitar a apropriação de crédito referente à parcela que for consumida em sua atividade comercial.
        /// </summary>
        RecepcaoEventoSolicitacaoDeApropriacaoDeCreditoDeCombustivel,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pelo adquirente para apropriação de crédito de bens e serviços que dependam da sua atividade
        /// </summary>
        RecepcaoEventoSolicitacaoDeApropriacaoDeCreditoParaBensEServicosQueDependemDeAtividadeDoAdquirente,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pela sucessora em relação às notas fiscais de transferência de crédito de outra sucessora da mesma empresa sucedida para informar aceite da transferência de crédito de IBS.
        /// </summary>
        RecepcaoEventoManifestacaoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pela sucessora em relação às notas fiscais de transferência de crédito de outra sucessora da mesma empresa sucedida para informar aceite da transferência de crédito de CBS.
        /// </summary>
        RecepcaoEventoManifestacaoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pelo fisco em relação às notas fiscais de transferência de crédito para informar aceite ou não aceite da transferência de crédito de IBS.
        /// </summary>
        RecepcaoEventoManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao,
        
        /// <summary>
        ///     Serviço para permitir ser gerado pelo fisco em relação às notas fiscais de transferência de crédito para informar aceite ou não aceite da transferência de crédito de CBS.
        /// </summary>
        RecepcaoEventoManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao,
        
        /// <summary>
        ///     Serviço para permitir ser gerado para permitir que o autor de um Evento já autorizado possa proceder o seu cancelamento.
        /// </summary>
        RecepcaoEventoCancelamentoDeEvento,
        
        /// <summary>
        ///     Serviço para permitir  que o adquirente das regiões incentivadas (ALC/ZFM) informe que a tributação na importação não se converteu em isenção de um
        ///     determinado item por não atender as condições da legislação
        /// </summary>
        RecepcaoEventoImportacaoEmAlcZfmNaoConvertidaEmIsencao,
        
        /// <summary>
        ///     Serviço para permitir ao adquirente informar quando uma aquisição for objeto de roubo, perda, furto ou perecimento.
        ///     Observação: O evento atual está relacionado aos bens que foram objeto de perecimento, perda, roubo ou furto em trânsito, em fornecimentos com frete FOB.
        /// </summary>
        RecepcaoEventoPerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloAdquirente,
        
        /// <summary>
        ///     Serviço para permitir ao fornecedor informar quando um bem for objeto de roubo, perda, furto ou perecimento antes da entrega, durante o transporte contratado pelo fornecedor.
        ///     Observação: O evento atual está relacionado aos bens móveis materiais que foram objeto de perecimento, perda, roubo ou furto em trânsito, em fornecimentos com frete CIF.
        /// </summary>
        RecepcaoEventoPerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloFornecedor,
        
        /// <summary>
        ///     Serviço para permitir ao fornecedor informar que um pagamento antecipado não teve o respectivo fornecimento realizado
        /// </summary>
        RecepcaoEventoFornecimentoNaoRealizadoComPagamentoAntecipado
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
    ///     110001 -  Cancelamento de Evento
    ///     110110 - Carta de Correção
    ///     110140 - EPEC
    ///     110111 - Cancelamento
    ///     110112 - Cancelamento por substituição
    ///     110130 - Comprovante de Entrega da NF-e
    ///     110131 - Cancelamento Comprovante de Entrega da NF-e
    ///     110150 - Ator Interessado na NF-e
    ///     110192 - Insucesso na Entrega da NF-e
    ///     110193 - Cancelamento Insucesso na Entrega da NF-e
    ///     110750 - Conciliação Financeira da NF-e
    ///     110751 - Cancelamento Conciliação Financeira da NF-e
    ///     112110 - Informação de efetivo pagamento integral para liberar crédito presumido do adquirente
    ///     112120 - Importação em ALC/ZFM não convertida em isenção
    ///     112140 - Fornecimento não realizado com pagamento antecipado
    ///     210200 – Confirmação da Operação
    ///     210210 – Ciência da Emissão
    ///     210220 – Desconhecimento da Operação
    ///     210240 – Operação não Realizada
    ///     211110 - Solicitação de Apropriação de crédito presumido
    ///     211120 - Destinação de item para consumo pessoal
    ///     211124 - Perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente
    ///     112130 - Perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor
    ///     211128 - Aceite de débito na apuração por emissão de nota de crédito
    ///     211130 -  Imobilização de Item
    ///     211140 -  Solicitação de Apropriação de Crédito de Combustível
    ///     211150 -  Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente
    ///     212110 -  Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
    ///     212120 -  Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
    ///     412120 -  Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
    ///     412130 -  Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
    /// </summary>
    public enum NFeTipoEvento
    {
        /// <summary>
        /// 110001 - Cancelamento de Evento
        /// </summary>
        [Description("Cancelamento de Evento")]
        [XmlEnum("110001")]
        TeNfeCancelamentoDeEvento = 110001,
        
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
        /// 110150 - Ator interessado na NF-e
        /// </summary>
        [Description("Ator interessado na NF-e")]
        [XmlEnum("110150")]
        TeNfeAtorInteressadoNFe = 110150,
        
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
        /// 112110 - Informação de efetivo pagamento integral para liberar crédito presumido do adquirente
        /// </summary>
        [Description("Informação de efetivo pagamento integral para liberar crédito presumido do adquirente")]
        [XmlEnum("112110")]
        TeNfeInformacaoDeEfetivoPagamentoIntegralParaLiberarCreditoPresumidoDoAdquirente = 112110,
        
        /// <summary>
        /// 112120 - Importação em ALC/ZFM não convertida em isenção
        /// </summary>
        [Description("Importação em ALC/ZFM não convertida em isenção")]
        [XmlEnum("112120")]
        TeNfeImportacaoEmAlcZfmNaoConvertidaEmIsencao = 112120,
        
        /// <summary>
        /// 112140 - Fornecimento não realizado com pagamento antecipado
        /// </summary>
        [Description("Fornecimento não realizado com pagamento antecipado")]
        [XmlEnum("112140")]
        TeNfeFornecimentoNaoRealizadoComPagamentoAntecipado = 112140,
        
        /// <summary>
        /// 211110 - Solicitação de Apropriação de crédito presumido
        /// </summary>
        [Description("Solicitação de Apropriação de crédito presumido")]
        [XmlEnum("211110")]
        TeNfeSolicitacaoDeApropriacaoDeCreditoPresumido = 211110,
        
        /// <summary>
        /// 211120 - Destinação de item para consumo pessoal
        /// </summary>
        [Description("Destinação de item para consumo pessoal")]
        [XmlEnum("211120")]
        TeNfeDestinacaoDeItemParaConsumoPessoal = 211120,
        
        /// <summary>
        /// 211124 - Perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente
        /// </summary>
        [Description("Perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente")]
        [XmlEnum("211124")]
        TeNfePerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloAdquirente = 211124,
        
        /// <summary>
        /// 112130 - Perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor
        /// </summary>
        [Description("Perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor")]
        [XmlEnum("112130")]
        TeNfePerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloFornecedor = 112130,
        
        /// <summary>
        /// 211128 - Aceite de débito na apuração por emissão de nota de crédito
        /// </summary>
        [Description("Aceite de débito na apuração por emissão de nota de crédito")]
        [XmlEnum("211128")]
        TeNfeAceiteDeDebitoNaApuracaoPorEmissaoDeNotaDeCredito = 211128,

        /// <summary>
        /// 211130 -  Imobilização de Item
        /// </summary>
        [Description("Imobilização de Item")]
        [XmlEnum("211130")]
        TeNfeImobilizacaoDeItem = 211130,
        
        /// <summary>
        /// 211140 -  Solicitação de Apropriação de Crédito de Combustível
        /// </summary>
        [Description("Solicitação de Apropriação de Crédito de Combustível")]
        [XmlEnum("211140")]
        TeNfeSolicitacaoApropriacaoCreditoCombustivel = 211140,
        
        /// <summary>
        /// 211150 -  Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente
        /// </summary>
        [Description("Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente")]
        [XmlEnum("211150")]
        TeNfeSolicitacaoDeApropriacaoDeCreditoParaBensEServicosQueDependemDeAtividadeDoAdquirente = 211150,
        
        /// <summary>
        /// 212110 -  Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
        /// </summary>
        [Description("Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão")]
        [XmlEnum("212110")]
        TeNfeManifestacaoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao = 212110,
        
        /// <summary>
        /// 212120 -  Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
        /// </summary>
        [Description("Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão")]
        [XmlEnum("212120")]
        TeNfeManifestacaoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao = 212120,
        
        /// <summary>
        /// 412120 -  Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
        /// </summary>
        [Description("Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão")]
        [XmlEnum("412120")]
        TeNfeManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao = 412120,
        
        /// <summary>
        /// 412130 -  Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
        /// </summary>
        [Description("Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão")]
        [XmlEnum("412130")]
        TeNfeManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao = 412130,
        
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
