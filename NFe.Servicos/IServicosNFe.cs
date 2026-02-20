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

using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.AdmCsc;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Inutilizacao;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos.Retorno;
using System;
using System.Collections.Generic;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoBensServicos;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoCombustivel;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoPresumido;
using NFe.Classes.Servicos.Evento.Informacoes.Imobilizacao;
using NFe.Classes.Servicos.Evento.Informacoes.ItemConsumo;
using NFe.Classes.Servicos.Evento.Informacoes.ItemNaoFornecido;
using NFe.Classes.Servicos.Evento.Informacoes.Perecimento;

namespace NFe.Servicos
{
    public interface IServicosNFe : IDisposable
    {
        /// <summary>
        ///     Consulta o status do Serviço de NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeStatusServico com os dados status do serviço</returns>
        RetornoNfeStatusServico NfeStatusServico(bool exceptionCompleta = false);

        /// <summary>
        ///     Consulta a Situação da NFe
        /// </summary>
        /// <returns>Retorna um objeto da classe RetornoNfeConsultaProtocolo com os dados da Situação da NFe</returns>
        RetornoNfeConsultaProtocolo NfeConsultaProtocolo(string chave);

        /// <summary>
        ///     Inutiliza uma faixa de números
        /// </summary>
        RetornoNfeInutilizacao NfeInutilizacao(string cnpj, int ano, ModeloDocumento modelo, int serie,
            int numeroInicial, int numeroFinal, string justificativa);

        /// <summary>
        ///     Inutilizar uma faixa de números já assinado.
        /// </summary>
        RetornoNfeInutilizacao NfeInutilizacao(inutNFe pedInutilizacao);

        /// <summary>
        ///     Envia um evento do tipo "Cancelamento"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancelamento(int idlote, int sequenciaEvento,
            string protocoloAutorizacao, string chaveNFe, string justificativa, string cpfcnpj, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia eventos do tipo "Cancelamento" já assinado.
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancelamento(int idlote, List<evento> eventos);

        /// <summary>
        ///     Envia um evento do tipo "Cancelamento por substituição"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancelamentoPorSubstituicao(int idlote, int sequenciaEvento,
            string protocoloAutorizacao, string chaveNFe, string justificativa, string cpfcnpj, Estado ufAutor, string versaoAplicativo, string chaveNfeSubstituta, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Processa a recepção do evento "Ator Interessado na NF-e - Transportador"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoAtorInteressado(int idlote, int sequenciaEvento, string cpfCnpjAtorEvento,
            string chaveNFe, string cnpfCnpjAtorInteressado, TipoAutor? tipoAutor = null, TipoAutorizacao? tipoAutorizacao = null,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia um evento do tipo "Carta de Correção"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCartaCorrecao(int idlote, int sequenciaEvento, string chaveNFe,
            string correcao, string cpfcnpj, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia eventos do tipo "Carta de correção" já assinado.
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCartaCorrecao(int idlote, List<evento> eventos);

        /// <summary>
        ///     Envia um evento do tipo "Manifestação do Destinatário"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoDestinatario(int idlote, int sequenciaEvento,
            string chaveNFe, NFeTipoEvento nFeTipoEventoManifestacaoDestinatario, string cpfcnpj,
            string justificativa = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia eventos do tipo "Manifestação do Destinatário" para múltiplas chaves
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoDestinatario(int idlote, int sequenciaEvento,
            string[] chavesNFe, NFeTipoEvento nFeTipoEventoManifestacaoDestinatario, string cpfcnpj,
            string justificativa = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia um evento do tipo "EPEC"
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoEpec(int idlote, int sequenciaEvento, Classes.NFe nfe,
            string veraplic, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Envia eventos do tipo "EPEC" já assinado
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoEpec(int idlote, List<evento> eventos);

        /// <summary>
        ///     Recepção do Evento de Insucesso na Entrega
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoInsucessoEntrega(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, DateTimeOffset dhTentativaEntrega, MotivoInsucesso motivo, string hashTentativaEntrega,
            int? nTentativa = null, DateTimeOffset? dhHashTentativaEntrega = null, decimal? latGps = null, decimal? longGps = null,
            string justificativa = null, Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Serviço para cancelamento insucesso na entrega
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancInsucessoEntrega(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, string nProtEvento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Recepção do Evento de Comprovante de Entrega
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoComprovanteEntrega(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, DateTimeOffset dhEntrega, string nDoc, string xNome, string hashComprovante,
            DateTimeOffset? dhHashComprovante = null, decimal? latGps = null, decimal? longGps = null,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Serviço para cancelamento comprovante de entrega
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancComprovanteEntrega(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, string nProtEvento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Recepção do Evento de Conciliação Financeira
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoConciliacaoFinanceira(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, List<detPagEvento> pagamentos,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Serviço para cancelamento Conciliação Financeira
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancConciliacaoFinanceira(int idlote,
            int sequenciaEvento, string cpfcnpj, string chaveNFe, string nProtEvento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dhEvento = null);

        /// <summary>
        ///     Serviço para evento informação de efetivo pagamento integral para liberar crédito presumido do adquirente
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoInformacaoDeEfetivoPagamentoIntegralParaLiberarCreditoPresumidoDoAdquirente(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorDeQuitacaoDoPagamento indicadorDeQuitacaoDoPagamento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento solicitação de apropriação de crédito presumido
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoSolicitacaoDeApropriacaoDeCreditoPresumido(int idLote,
            int sequenciaEvento, string cpfCnpj, TipoAutor tipoAutor, string chaveNFe,
            List<gCredPres> informacoesDeCreditoPresumidoPorItem,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento destinação de item para consumo pessoal
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoDestinacaoDeItemParaConsumoPessoal(int idLote,
            int sequenciaEvento, string cpfCnpj, TipoAutor tipoAutor, string chaveNFe,
            List<gConsumo> informacoesDeItensParaConsumoPessoal,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento aceite de débito na apuração por emissão de nota de crédito
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoAceiteDeDebitoNaApuracaoPorEmissaoDeNotaDeCredito(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorAceitacao indicadorAceitacao,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de imobilização de item
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoImobilizacaoItem(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gImobilizacao> imobilizacoesItens,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de solicitação de apropriação de crédito de combustível
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoSolicitacaApropriacaoCreditoCombustivel(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gConsumoComb> gConsumoComb,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoSolicitacaoDeApropriacaoDeCreditoParaBensEServicosQueDependemDeAtividadeDoAdquirente(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gCredito> gCreditos,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorAceitacao indicadorAceitacao,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorAceitacao indicadorAceitacao,
            Estado? ufAutor, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorDeferimento indicadorDeferimento,
            MotivoDeferimento motivoDeferimento, string descricaoDeferimento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento de Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, IndicadorDeferimento indicadorDeferimento,
            MotivoDeferimento motivoDeferimento, string descricaoDeferimento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento cancelamento de evento
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoCancelamentoDeEvento(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, string tpEventoAut, string nProtEvento,
            TipoAutor tipoAutor, Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento importação em ALC/ZFM não convertida em isenção
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoImportacaoEmAlcZfmNaoConvertidaEmIsencao(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gConsumo> informacoesPorItemDeImportacao,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento perecimento, perda, roubo ou furto durante o transporte contratado pelo adquirente
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoPerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloAdquirente(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gPerecimento> informacoesPorItemDaNotaDeAquisicao,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoPerecimentoPerdaRouboOuFurtoDuranteOTransporteContratadoPeloFornecedor(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gPerecimento> informacoesPorItemDaNotaDeFornecimento,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Serviço para evento fornecimento não realizado com pagamento antecipado
        /// </summary>
        RetornoRecepcaoEvento RecepcaoEventoFornecimentoNaoRealizadoComPagamentoAntecipado(int idLote,
            int sequenciaEvento, string cpfCnpj, string chaveNFe, List<gItemNaoFornecido> informacoesPorItemDaNotaDePagamentoAntecipado,
            Estado? ufAutor = null, string versaoAplicativo = null, DateTimeOffset? dataHoraEvento = null);

        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        /// </summary>
        RetornoNfeConsultaCadastro NfeConsultaCadastro(string uf, ConsultaCadastroTipoDocumento tipoDocumento, string documento);

        /// <summary>
        ///     Consulta GTIN
        /// </summary>
        RetornoConsultaGtin ConsultaGtin(string gtin);

        /// <summary>
        ///     Serviço destinado à distribuição de informações resumidas e documentos fiscais eletrônicos de interesse de um ator
        /// </summary>
        RetornoNfeDistDFeInt NfeDistDFeInteresse(string ufAutor, string documento, string ultNSU = "0", string nSU = "0", string chNFE = "");

        /// <summary>
        ///     Envia uma ou mais NFe (Recepção)
        /// </summary>
        RetornoNfeRecepcao NfeRecepcao(int idLote, List<Classes.NFe> nFes);

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ (RetRecepcao)
        /// </summary>
        RetornoNfeRetRecepcao NfeRetRecepcao(string recibo);

        /// <summary>
        ///     Envia uma ou mais NFe (Autorização)
        /// </summary>
        RetornoNFeAutorizacao NFeAutorizacao(int idLote, IndicadorSincronizacao indSinc, List<Classes.NFe> nFes, bool compactarMensagem = false);

        /// <summary>
        ///     Recebe o retorno do processamento de uma ou mais NFe's pela SEFAZ (RetAutorização)
        /// </summary>
        RetornoNFeRetAutorizacao NFeRetAutorizacao(string recibo);

        /// <summary>
        ///     Consulta a Situação da NFe - Download
        /// </summary>
        [System.Obsolete("Descontinuado pela Sefaz")]
        RetornoNfeDownload NfeDownloadNf(string cnpj, List<string> chaves, string nomeSaida = "");

        /// <summary>
        ///     Administração do CSC para NFC-e
        /// </summary>
        RetornoAdmCscNFCe AdmCscNFCe(string raizCnpj, IdentificadorOperacaoCsc identificadorOperacaoCsc, string idCscASerRevogado = null, string codigoCscASerRevogado = null);
    }
}
