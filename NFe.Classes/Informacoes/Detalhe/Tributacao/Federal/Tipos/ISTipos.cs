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
    ///     <para>000 - Tributação integral</para>
    ///     <para>010 - Tributação com alíquotas uniformes</para>
    ///     <para>011 - Tributação com alíquotas uniformes reduzidas</para>
    ///     <para>200 - Alíquota reduzida</para>
    ///     <para>220 - Alíquota fixa</para>
    ///     <para>221 - Alíquota fixa proporcional</para>
    ///     <para>222 - Redução de base de cálculo</para>
    ///     <para>400 - Isenção</para>
    ///     <para>410 - Imunidade e não incidência</para>
    ///     <para>510 - Diferimento</para>
    ///     <para>515 - Diferimento com redução de alíquota</para>
    ///     <para>550 - Suspensão</para>
    ///     <para>620 - Tributação monofásica</para>
    ///     <para>800 - Transferência de crédito</para>
    ///     <para>810 - Ajuste de IBS na Zona Franca de Manaus (ZFM)</para>
    ///     <para>811 - Ajustes</para>
    ///     <para>820 - Tributação em declaração de regime específico</para>
    ///     <para>830 - Exclusão da base de cálculo</para>
    /// </summary>
    public enum CSTIS
    {
        /// <summary>
        ///     000 - Tributação integral: operação tributada pela alíquota integral do Imposto Seletivo
        /// </summary>
        [Description("Tributação integral: operação tributada pela alíquota integral do Imposto Seletivo")]
        [XmlEnum("000")]
        Is000,

        /// <summary>
        ///     010 - Tributação com alíquotas uniformes: aplica-se a alíquota geral definida para o produto/serviço
        /// </summary>
        [Description("Tributação com alíquotas uniformes: aplica-se a alíquota geral definida para o produto/serviço")]
        [XmlEnum("010")]
        Is010,

        /// <summary>
        ///     011 - Tributação com alíquotas uniformes reduzidas: alíquotas gerais reduzidas, conforme previsão legal
        /// </summary>
        [Description("Tributação com alíquotas uniformes reduzidas: alíquotas gerais reduzidas, conforme previsão legal")]
        [XmlEnum("011")]
        Is011,

        /// <summary>
        ///     200 - Alíquota reduzida: operações com alíquota do Imposto Seletivo reduzida em relação à alíquota padrão
        /// </summary>
        [Description("Alíquota reduzida: operações com alíquota do Imposto Seletivo reduzida em relação à alíquota padrão")]
        [XmlEnum("200")]
        Is200,

        /// <summary>
        ///     220 - Alíquota fixa: tributação com alíquota fixa (valor específico)
        /// </summary>
        [Description("Alíquota fixa: tributação com alíquota fixa (valor específico)")]
        [XmlEnum("220")]
        Is220,

        /// <summary>
        ///     221 - Alíquota fixa proporcional: alíquota específica proporcional à base de cálculo
        /// </summary>
        [Description("Alíquota fixa proporcional: alíquota específica proporcional à base de cálculo")]
        [XmlEnum("221")]
        Is221,

        /// <summary>
        ///     222 - Redução de base de cálculo: incidência do IS com redução na base de cálculo
        /// </summary>
        [Description("Redução de base de cálculo: incidência do IS com redução na base de cálculo")]
        [XmlEnum("222")]
        Is222,

        /// <summary>
        ///     400 - Isenção: operações isentas de Imposto Seletivo
        /// </summary>
        [Description("Isenção: operações isentas de Imposto Seletivo")]
        [XmlEnum("400")]
        Is400,

        /// <summary>
        ///     410 - Imunidade e não incidência: operações imunes ou não sujeitas ao Imposto Seletivo
        /// </summary>
        [Description("Imunidade e não incidência: operações imunes ou não sujeitas ao Imposto Seletivo")]
        [XmlEnum("410")]
        Is410,

        /// <summary>
        ///     510 - Diferimento: cobrança do Imposto Seletivo diferida para etapa posterior
        /// </summary>
        [Description("Diferimento: cobrança do Imposto Seletivo diferida para etapa posterior")]
        [XmlEnum("510")]
        Is510,

        /// <summary>
        ///     515 - Diferimento com redução de alíquota: diferimento com alíquota reduzida
        /// </summary>
        [Description("Diferimento com redução de alíquota: diferimento com alíquota reduzida")]
        [XmlEnum("515")]
        Is515,

        /// <summary>
        ///     550 - Suspensão: suspensão da cobrança do Imposto Seletivo
        /// </summary>
        [Description("Suspensão: suspensão da cobrança do Imposto Seletivo")]
        [XmlEnum("550")]
        Is550,

        /// <summary>
        ///     620 - Tributação monofásica: incidência em regime monofásico (cobrança concentrada em determinado elo da cadeia)
        /// </summary>
        [Description("Tributação monofásica: incidência em regime monofásico (cobrança concentrada em determinado elo da cadeia)")]
        [XmlEnum("620")]
        Is620,

        /// <summary>
        ///     800 - Transferência de crédito: operações que geram transferência de crédito do Imposto Seletivo
        /// </summary>
        [Description("Transferência de crédito: operações que geram transferência de crédito do Imposto Seletivo")]
        [XmlEnum("800")]
        Is800,

        /// <summary>
        ///     810 - Ajuste de IBS na Zona Franca de Manaus (ZFM): ajustes de crédito para operações envolvendo IBS/CBS na ZFM
        /// </summary>
        [Description("Ajuste de IBS na Zona Franca de Manaus (ZFM): ajustes de crédito para operações envolvendo IBS/CBS na ZFM; pode também se aplicar ao IS")]
        [XmlEnum("810")]
        Is810,

        /// <summary>
        ///     811 - Ajustes: outras hipóteses de ajuste de crédito ou débito
        /// </summary>
        [Description("Ajustes: outras hipóteses de ajuste de crédito ou débito")]
        [XmlEnum("811")]
        Is811,

        /// <summary>
        ///     820 - Tributação em declaração de regime específico: operações sujeitas a regime específico de tributação
        /// </summary>
        [Description("Tributação em declaração de regime específico: operações sujeitas a regime específico de tributação")]
        [XmlEnum("820")]
        Is820,

        /// <summary>
        ///     830 - Exclusão da base de cálculo: exclusão do valor da base de cálculo do Imposto Seletivo
        /// </summary>
        [Description("Exclusão da base de cálculo: exclusão do valor da base de cálculo do Imposto Seletivo")]
        [XmlEnum("830")]
        Is830
    }
}