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
        NfceAdministracaoCSC
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