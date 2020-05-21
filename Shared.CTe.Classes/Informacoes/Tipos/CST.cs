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

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Informações relativas ao ICMS
    ///     <para>00 - Tributação normal ICMS;</para>
    ///     <para>20 - Tributação com BC reduzida do ICMS;</para>
    ///     <para>40 - ICMS isenção;</para>
    ///     <para>41 - ICMS não tributada;</para>
    ///     <para>51 - ICMS diferido;</para>
    ///     <para>60 - ICMS cobrado por substituição tributária;</para>
    ///     <para>90 - Outros</para>
    /// </summary>
    public enum CST
    {
        /// <summary>
        /// 00 - Tributação normal ICMS
        /// </summary>
        [Description("Tributação normal ICMS")]
        [XmlEnum("00")]
        ICMS00 = 00,

        /// <summary>
        /// 20 - Tributação com BC reduzida do ICMS
        /// </summary>
        [Description("Tributação com BC reduzida do ICMS")]
        [XmlEnum("20")]
        ICMS20 = 20,

        /// <summary>
        /// 40 - ICMS isenção
        /// </summary>
        [Description("ICMS isenção")]
        [XmlEnum("40")]
        ICMS40,

        /// <summary>
        /// 41 - ICMS não tributada
        /// </summary>
        [Description("ICMS não tributada")]
        [XmlEnum("41")]
        ICMS41,

        /// <summary>
        /// 51 - ICMS diferido
        /// </summary>
        [Description("ICMS diferido")]
        [XmlEnum("51")]
        ICMS51,

        /// <summary>
        /// 60 - ICMS cobrado por substituição tributária
        /// </summary>
        [Description("ICMS cobrado por substituição tributária")]
        [XmlEnum("60")]
        ICMS60,

        /// <summary>
        /// 90 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("90")]
        ICMS90
    }
}