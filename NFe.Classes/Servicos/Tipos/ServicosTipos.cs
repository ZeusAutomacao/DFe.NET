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

    public enum VersaoServico
    {
        [XmlEnum("1.00")] ve100,
        [XmlEnum("2.00")] ve200,
        [XmlEnum("3.10")] ve310
    }

    /// <summary>
    ///     Indicador de Sincronização:
    ///     <para>0 = Assíncrono. A resposta deve ser obtida consultando o serviço NFeRetAutorizacao, com o nº do recibo</para>
    ///     <para>
    ///         1 = Síncrono. Empresa solicita processamento síncrono do Lote de NF-e (sem a geração de Recibo para consulta
    ///         futura);
    ///     </para>
    /// </summary>
    public enum IndicadorSincronizacao
    {
        [XmlEnum("0")] Assincrono = 0,
        [XmlEnum("1")] Sincrono = 1
    }

    /// <summary>
    /// Tipo do evento de manifestação do destinatário.
    /// </summary>
    public enum TipoEventoManifestacaoDestinatario
    {
        [Description("Confirmacao da Operacao")]
        TeMdConfirmacaoDaOperacao = 210200,

        [Description("Ciencia da Operacao")]
        TeMdCienciaDaEmissao = 210210,

        [Description("Desconhecimento da Operacao")]
        TeMdDesconhecimentoDaOperacao = 210220,

        [Description("Operacao nao Realizada")]
        TeMdOperacaoNaoRealizada = 210240
    }
}