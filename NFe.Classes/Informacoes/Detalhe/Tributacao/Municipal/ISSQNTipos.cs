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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Municipal
{
    /// <summary>
    ///     <para>1=Sim;</para>
    ///     <para>2=Não;</para>
    /// </summary>
    public enum indIncentivo
    {
        /// <summary>
        /// 1=Sim
        /// </summary>
        [Description("Sim")]
        [XmlEnum("1")]
        iiSim = 1,

        /// <summary>
        /// 2=Não
        /// </summary>
        [Description("Não")]
        [XmlEnum("2")]
        iiNao = 2
    }

    /// <summary>
    ///     <para>1=Exigível,</para>
    ///     <para>2=Não incidência;</para>
    ///     <para>3=Isenção;</para>
    ///     <para>4=Exportação;</para>
    ///     <para>5=Imunidade;</para>
    ///     <para>6=Exigibilidade Suspensa por Decisão Judicial;</para>
    ///     <para>7=Exigibilidade Suspensa por Processo Administrativo;</para>
    /// </summary>
    public enum IndicadorISS
    {
        /// <summary>
        /// 1=Exigível
        /// </summary>
        [Description("Exigível")]
        [XmlEnum("1")]
        iiExigivel = 1,

        /// <summary>
        /// 2=Não incidência
        /// </summary>
        [Description("Não incidência")]
        [XmlEnum("2")]
        iiNaoIncidencia = 2,

        /// <summary>
        /// 3=Isenção
        /// </summary>
        [Description("Isenção")]
        [XmlEnum("3")]
        iiIsencao = 3,

        /// <summary>
        /// 4=Exportação
        /// </summary>
        [Description("Exportação")]
        [XmlEnum("4")]
        iiExportacao = 4,

        /// <summary>
        /// 5=Imunidade
        /// </summary>
        [Description("Imunidade")]
        [XmlEnum("5")]
        iiImunidade = 5,

        /// <summary>
        /// 6=Exigibilidade Suspensa por Decisão Judicial
        /// </summary>
        [Description("Exigibilidade Suspensa por Decisão Judicial")]
        [XmlEnum("6")]
        iiExigSuspDecisaoJudicial = 6,

        /// <summary>
        /// 7=Exigibilidade Suspensa por Processo Administrativo
        /// </summary>
        [Description("Exigibilidade Suspensa por Processo Administrativo")]
        [XmlEnum("7")]
        iiExigSuspProcessoAdm = 7
    }
}