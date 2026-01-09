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
    ///     CST para o Imposto Seletivo (IS)
    ///     <para>000 - Tributada integralmente</para>
    ///     <para>001 - Tributada com alíquota por unidade de medida de produto</para>
    ///     <para>002 - Tributada com alíquota ad valorem</para>
    ///     <para>003 - Tributada com redução de base de cálculo</para>
    ///     <para>050 - Tributação monofásica sobre combustíveis com retenção do IS</para>
    ///     <para>051 - Tributação monofásica sobre combustíveis sem retenção do IS</para>
    ///     <para>090 - Outras operações de tributação integral</para>
    ///     <para>100 - Imune</para>
    ///     <para>200 - Isenta</para>
    ///     <para>300 - Não tributada</para>
    ///     <para>400 - Não incidência</para>
    ///     <para>500 - Suspensão</para>
    /// </summary>
    public enum CSTIS
    {
        /// <summary>
        ///     000 - Tributada integralmente
        /// </summary>
        [Description("Tributada integralmente")]
        [XmlEnum("000")]
        Is000,

        /// <summary>
        ///     001 - Tributada com alíquota por unidade de medida de produto
        /// </summary>
        [Description("Tributada com alíquota por unidade de medida de produto")]
        [XmlEnum("001")]
        Is001,

        /// <summary>
        ///     002 - Tributada com alíquota ad valorem
        /// </summary>
        [Description("Tributada com alíquota ad valorem")]
        [XmlEnum("002")]
        Is002,

        /// <summary>
        ///     003 - Tributada com redução de base de cálculo
        /// </summary>
        [Description("Tributada com redução de base de cálculo")]
        [XmlEnum("003")]
        Is003,

        /// <summary>
        ///     050 - Tributação monofásica sobre combustíveis com retenção do IS
        /// </summary>
        [Description("Tributação monofásica sobre combustíveis com retenção do IS")]
        [XmlEnum("050")]
        Is050,

        /// <summary>
        ///     051 - Tributação monofásica sobre combustíveis sem retenção do IS
        /// </summary>
        [Description("Tributação monofásica sobre combustíveis sem retenção do IS")]
        [XmlEnum("051")]
        Is051,

        /// <summary>
        ///     090 - Outras operações de tributação integral
        /// </summary>
        [Description("Outras operações de tributação integral")]
        [XmlEnum("090")]
        Is090,

        /// <summary>
        ///     100 - Imune
        /// </summary>
        [Description("Imune")]
        [XmlEnum("100")]
        Is100,

        /// <summary>
        ///     200 - Isenta
        /// </summary>
        [Description("Isenta")]
        [XmlEnum("200")]
        Is200,

        /// <summary>
        ///     300 - Não tributada
        /// </summary>
        [Description("Não tributada")]
        [XmlEnum("300")]
        Is300,

        /// <summary>
        ///     400 - Não incidência
        /// </summary>
        [Description("Não incidência")]
        [XmlEnum("400")]
        Is400,

        /// <summary>
        ///     500 - Suspensão
        /// </summary>
        [Description("Suspensão")]
        [XmlEnum("500")]
        Is500
    }
}