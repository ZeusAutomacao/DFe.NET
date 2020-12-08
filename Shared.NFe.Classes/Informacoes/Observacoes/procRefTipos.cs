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

namespace NFe.Classes.Informacoes.Observacoes
{
    /// <summary>
    ///     Indicador da origem do processo
    ///     <para>0 - SEFAZ;</para>
    ///     <para>1 - Justiça Federal;</para>
    ///     <para>2 - Justiça Estadual;</para>
    ///     <para>3 - Secex/RFB;</para>
    ///     <para>9 - Outros</para>
    /// </summary>
    public enum IndicadorProcesso
    {
        /// <summary>
        /// 0 - SEFAZ
        /// </summary>
        [Description("SEFAZ")]
        [XmlEnum("0")]
        ipSEFAZ = 0,

        /// <summary>
        /// 1 - Justiça Federal
        /// </summary>
        [Description("Justiça Federal")]
        [XmlEnum("1")]
        ipJusticaFederal = 1,

        /// <summary>
        /// 2 - Justiça Estadual
        /// </summary>
        [Description("Justiça Estadual")]
        [XmlEnum("2")]
        ipJusticaEstadual = 2,

        /// <summary>
        /// 3 - Secex/RFB
        /// </summary>
        [Description("Secex/RFB")]
        [XmlEnum("3")]
        ipSecexRFB = 3,

        /// <summary>
        /// 9 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("9")]
        ipOutros = 9
    }
}