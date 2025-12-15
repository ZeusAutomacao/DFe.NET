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

namespace CTe.Classes.Informacoes.Impostos.Tipos
{
    /// <summary>
    /// Código de Situação Tributária do IBS/CBS
    /// </summary>
    public enum CstIbsCbs
    {
        /// <summary>
        /// 00 - Tributação integral
        /// </summary>
        [XmlEnum("00")]
        [Description("Tributação integral")]
        Cst00 = 0,

        /// <summary>
        /// 01 - Tributação com alíquotas uniformes
        /// </summary>
        [XmlEnum("01")]
        [Description("Tributação com alíquotas uniformes")]
        Cst01 = 1,

        /// <summary>
        /// 02 - Tributação com alíquotas uniformes reduzidas
        /// </summary>
        [XmlEnum("02")]
        [Description("Tributação com alíquotas uniformes reduzidas")]
        Cst02 = 2,

        /// <summary>
        /// 20 - Alíquota reduzida
        /// </summary>
        [XmlEnum("20")]
        [Description("Alíquota reduzida")]
        Cst20 = 20,

        /// <summary>
        /// 22 - Alíquota fixa
        /// </summary>
        [XmlEnum("22")]
        [Description("Alíquota fixa")]
        Cst22 = 22,

        /// <summary>
        /// 23 - Alíquota fixa rateada
        /// </summary>
        [XmlEnum("23")]
        [Description("Alíquota fixa rateada")]
        Cst23 = 23,

        /// <summary>
        /// 24 - Redução de Base de Cálculo
        /// </summary>
        [XmlEnum("24")]
        [Description("Redução de Base de Cálculo")]
        Cst24 = 24,

        /// <summary>
        /// 40 - Isenção
        /// </summary>
        [XmlEnum("40")]
        [Description("Isenção")]
        Cst40 = 40,

        /// <summary>
        /// 41 - Imunidade / Não incidência
        /// </summary>
        [XmlEnum("41")]
        [Description("Imunidade / Não incidência")]
        Cst41 = 41,

        /// <summary>
        /// 51 - Diferimento
        /// </summary>
        [XmlEnum("51")]
        [Description("Diferimento")]
        Cst51 = 51,

        /// <summary>
        /// 52 - Diferimento com redução de alíquota
        /// </summary>
        [XmlEnum("52")]
        [Description("Diferimento com redução de alíquota")]
        Cst52 = 52,

        /// <summary>
        /// 55 - Suspensão
        /// </summary>
        [XmlEnum("55")]
        [Description("Suspensão")]
        Cst55 = 55,

        /// <summary>
        /// 62 - Tributação monofásica
        /// </summary>
        [XmlEnum("62")]
        [Description("Tributação monofásica")]
        Cst62 = 62,

        /// <summary>
        /// 80 - Transferência de crédito
        /// </summary>
        [XmlEnum("80")]
        [Description("Transferência de crédito")]
        Cst80 = 80,

        /// <summary>
        /// 81 - Ajuste de IBS em ZFM
        /// </summary>
        [XmlEnum("81")]
        [Description("Ajuste de IBS em ZFM")]
        Cst81 = 81,

        /// <summary>
        /// 82 - Ajustes
        /// </summary>
        [XmlEnum("82")]
        [Description("Ajustes")]
        Cst82 = 82,

        /// <summary>
        /// 83 - Tributação em declaração de regime específico
        /// </summary>
        [XmlEnum("83")]
        [Description("Tributação em declaração de regime específico")]
        Cst83 = 83,

        /// <summary>
        /// 84 - Exclusão da Base de Cálculo
        /// </summary>
        [XmlEnum("84")]
        [Description("Exclusão da Base de Cálculo")]
        Cst84 = 84
    }
}
