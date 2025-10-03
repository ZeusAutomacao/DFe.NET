﻿/********************************************************************************/
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
using System.Runtime.ConstrainedExecution;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Evento
{
    /// <summary>
    ///     Informar "1 - Empresa Emitente" para este evento.
    ///     Nota:
    ///     1 - Empresa Emitente;
    ///     2 - Empresa Destinatária;
    ///     3 - Empresa;
    ///     5 - Fisco;
    ///     6 - RFB;
    ///     9 - Outros Órgãos.
    /// </summary>
    public enum TipoAutor
    {
        /// <summary>
        /// 1 - Empresa Emitente
        /// </summary>
        [Description("Empresa Emitente")]
        [XmlEnum("1")]
        taEmpresaEmitente = 1,

        /// <summary>
        /// 2 - Empresa Destinatária
        /// </summary>
        [Description("Empresa Destinatária")]
        [XmlEnum("2")]
        taEmpresaDestinataria = 2,

        /// <summary>
        /// 3 - Empresa
        /// </summary>
        [Description("Empresa")]
        [XmlEnum("3")]
        taEmpresa = 3,

        /// <summary>
        /// 5 - Fisco
        /// </summary>
        [Description("Fisco")]
        [XmlEnum("5")]
        taFisco = 5,

        /// <summary>
        /// 6 - RFB
        /// </summary>
        [Description("RFB")]
        [XmlEnum("6")]
        taRFB = 6,
        
        /// <summary>
        /// 8 - Empresa sucessora
        /// </summary>
        [Description("Empresa sucessora")]
        [XmlEnum("8")]
        taEmpresaSucessora = 8,

        /// <summary>
        /// 9 - Outros Órgãos
        /// </summary>
        [Description("Outros Órgãos")]
        [XmlEnum("9")]
        taOutrosOrgaos = 9
    }

    /// <summary>
    ///     Motivo de Insucesso.
    ///     Nota:
    ///     1 - Recebedor não encontrado;
    ///     2 - Recusa do recebedor;
    ///     3 - Endereço inexistente;
    ///     4 - Outros (exige informar justificativa);
    /// </summary>
    public enum MotivoInsucesso
    {
        /// <summary>
        /// 1 - Recebedor não encontrado 
        /// </summary>
        [Description("Recebedor não encontrado")]
        [XmlEnum("1")]
        RecebedorNaoEncontrado = 1,

        /// <summary>
        /// 2 - Recusa do recebedor
        /// </summary>
        [Description("Recusa do recebedor")]
        [XmlEnum("2")]
        RecusaRecebedor = 2,

        /// <summary>
        /// 3 - Endereço inexistente
        /// </summary>
        [Description("Endereço inexistente")]
        [XmlEnum("3")]
        EnderecoInexistente = 3,

        /// <summary>
        /// 4 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("4")]
        Outros = 4
    }

    /// <summary>
    ///  0 – Não permite;
    ///  1 – Permite o transportador autorizado pelo
    ///  emitente ou destinatário autorizar outros
    ///  transportadores para ter acesso ao download da
    ///  NF-e
    /// </summary>
    public enum TipoAutorizacao
    {
        /// <summary>
        /// 0 – Não permite
        /// </summary>
        [Description("Não permite")]
        [XmlEnum("0")]
        NaoPermite = 0,

        /// <summary>
        ///  1 – Permite o transportador autorizado pelo
        ///  emitente ou destinatário autorizar outros
        ///  transportadores para ter acesso ao download da
        ///  NF-e
        /// </summary>
        [Description("Permite")]
        [XmlEnum("1")]
        Permite = 1
    }
    
    /// <summary>
    ///     Indicador de efetiva quitação do pagamento integral referente a NFe referenciada.
    /// </summary>
    public enum IndicadorDeQuitacaoDoPagamento
    {
        /// <summary>
        ///     1 – Quitado
        ///     Observação: Outros valores ainda não foram publicados. Revisado: 17/07/2025. Nota técnica base: NT_2025.002_v1.10_RTC_NF-e_IBS_CBS_IS
        /// </summary>
        [Description("Quitado")]
        [XmlEnum("1")]
        Quitado = 1
    }

    /// <summary>
    ///     Indicador de concordância com o valor da nota de crédito que lançaram IBS e CBS na apuração assistida.
    ///     0 - Não aceite
    ///     1 - Aceite
    /// </summary>
    public enum IndicadorAceitacao
    {
        [Description("Não aceite")]
        [XmlEnum("0")]
        NaoAceite = 0,
        
        [Description("Aceite")]
        [XmlEnum("1")]
        Aceite = 1
    }
    
    /// <summary>
    ///     Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada.
    ///     0 - Não aceite
    ///     1 - Aceite
    /// </summary>
    public enum IndicadorDeferimento
    {
        [Description("Não aceite")]
        [XmlEnum("0")]
        NaoAceite = 0,
        
        [Description("Aceite")]
        [XmlEnum("1")]
        Aceite = 1
    }

    public enum MotivoDeferimento
    {
        [Description("Falta de manifestação de todas as sucessoras")]
        [XmlEnum("1")]
        FaltaDeManifestacaoDeTodasSucessoras,
        
        [Description("Outros.")]
        [XmlEnum("2")]
        Outros
    }
}