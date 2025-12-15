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
    /// Conforme NT 2025.001 v1.07
    /// </summary>
    public enum CstIbsCbs
    {
        /// <summary>
        /// 200 - Tributação integral
        /// </summary>
        [XmlEnum("200")]
        [Description("Tributação integral")]
        Cst200 = 200,

        /// <summary>
        /// 210 - Tributação com alíquota uniforme
        /// </summary>
        [XmlEnum("210")]
        [Description("Tributação com alíquota uniforme")]
        Cst210 = 210,

        /// <summary>
        /// 220 - Tributação com alíquota uniforme reduzida
        /// </summary>
        [XmlEnum("220")]
        [Description("Tributação com alíquota uniforme reduzida")]
        Cst220 = 220,

        /// <summary>
        /// 300 - Alíquota reduzida
        /// </summary>
        [XmlEnum("300")]
        [Description("Alíquota reduzida")]
        Cst300 = 300,

        /// <summary>
        /// 400 - Isenção
        /// </summary>
        [XmlEnum("400")]
        [Description("Isenção")]
        Cst400 = 400,

        /// <summary>
        /// 410 - Imunidade / Não incidência
        /// </summary>
        [XmlEnum("410")]
        [Description("Imunidade / Não incidência")]
        Cst410 = 410,

        /// <summary>
        /// 500 - Suspensão / Diferimento
        /// </summary>
        [XmlEnum("500")]
        [Description("Suspensão / Diferimento")]
        Cst500 = 500
    }
}
