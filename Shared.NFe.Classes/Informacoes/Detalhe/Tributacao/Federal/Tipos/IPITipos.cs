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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos
{
    /// <summary>
    ///     <para>00-Entrada com recuperação de crédito</para>
    ///     <para>49-Outras entradas</para>
    ///     <para>50-Saída tributada</para>
    ///     <para>99-Outras saídas</para>
    ///     <para>01-Entrada tributada com alíquota zero</para>
    ///     <para>02-Entrada isenta</para>
    ///     <para>03-Entrada não-tributada</para>
    ///     <para>04-Entrada imune</para>
    ///     <para>05-Entrada com suspensão</para>
    ///     <para>51-Saída tributada com alíquota zero</para>
    ///     <para>52-Saída isenta</para>
    ///     <para>53-Saída não-tributada</para>
    ///     <para>54-Saída imune</para>
    ///     <para>55-Saída com suspensão</para>
    /// </summary>
    public enum CSTIPI
    {
        /// <summary>
        /// 00 - Entrada com recuperação de crédito
        /// </summary>
        [Description("Entrada com recuperação de crédito")]
        [XmlEnum("00")]
        ipi00,

        /// <summary>
        /// 49 - Outras entradas
        /// </summary>
        [Description("Outras entradas")]
        [XmlEnum("49")]
        ipi49,

        /// <summary>
        /// 50 - Saída tributada
        /// </summary>
        [Description("Saída tributada")]
        [XmlEnum("50")]
        ipi50,

        /// <summary>
        /// 99 - Outras saídas
        /// </summary>
        [Description("Outras saídas")]
        [XmlEnum("99")]
        ipi99,

        /// <summary>
        /// 01 - Entrada tributada com alíquota zero
        /// </summary>
        [Description("Entrada tributada com alíquota zero")]
        [XmlEnum("01")]
        ipi01,

        /// <summary>
        /// 02 - Entrada isenta
        /// </summary>
        [Description("Entrada isenta")]
        [XmlEnum("02")]
        ipi02,

        /// <summary>
        /// 03 - Entrada não-tributada
        /// </summary>
        [Description("Entrada não-tributada")]
        [XmlEnum("03")]
        ipi03,

        /// <summary>
        /// 04 - Entrada imune
        /// </summary>
        [Description("Entrada imune")]
        [XmlEnum("04")]
        ipi04,

        /// <summary>
        /// 05 - Entrada com suspensão
        /// </summary>
        [Description("Entrada com suspensão")]
        [XmlEnum("05")]
        ipi05,

        /// <summary>
        /// 51 - Saída tributada com alíquota zero
        /// </summary>
        [Description("Saída tributada com alíquota zero")]
        [XmlEnum("51")]
        ipi51,

        /// <summary>
        /// 52 - Saída isenta
        /// </summary>
        [Description("Saída isenta")]
        [XmlEnum("52")]
        ipi52,

        /// <summary>
        /// 53 - Saída não-tributada
        /// </summary>
        [Description("Saída não-tributada")]
        [XmlEnum("53")]
        ipi53,

        /// <summary>
        /// 54 - Saída imune
        /// </summary>
        [Description("Saída imune")]
        [XmlEnum("54")]
        ipi54,

        /// <summary>
        /// 55 - Saída com suspensão
        /// </summary>
        [Description("Saída com suspensão")]
        [XmlEnum("55")]
        ipi55
    }
}