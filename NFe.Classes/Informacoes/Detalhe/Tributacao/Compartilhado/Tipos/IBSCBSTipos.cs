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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos
{
    public enum CST
    {
        /// <summary>
        ///     000 - Tributação integral
        /// </summary>
        [Description("Tributação integral")]
        [XmlEnum("000")]
        Cst000,
        
        /// <summary>
        ///     010 - Tributação com alíquotas uniformes
        /// </summary>
        [Description("Tributação com alíquotas uniformes")]
        [XmlEnum("010")]
        Cst010,
        
        /// <summary>
        ///     011 - Tributação com alíquotas uniformes reduzidas
        /// </summary>
        [Description("Tributação com alíquotas uniformes reduzidas")]
        [XmlEnum("011")]
        Cst011,
        
        /// <summary>
        ///     200 - Alíquota reduzida
        /// </summary>
        [Description("Alíquota reduzida")]
        [XmlEnum("200")]
        Cst200,
        
        /// <summary>
        ///     210 - Redução de alíquota com redutor de base de cálculo
        /// </summary>
        [Description("Redução de alíquota com redutor de base de cálculo")]
        [XmlEnum("210")]
        Cst210,
        
        /// <summary>
        ///     220 - Alíquota fixa
        /// </summary>
        [Description("Alíquota fixa")]
        [XmlEnum("220")]
        Cst220,
        
        /// <summary>
        ///     221 - Alíquota fixa rateada
        /// </summary>
        [Description("Alíquota fixa rateada")]
        [XmlEnum("221")]
        Cst221,
        
        /// <summary>
        ///     222 - Redução de Base de Cálculo
        /// </summary>
        [Description("Redução de Base de Cálculo")]
        [XmlEnum("222")]
        Cst222,
        
        /// <summary>
        ///     400 - Isenção
        /// </summary>
        [Description("Isenção")]
        [XmlEnum("400")]
        Cst400,
        
        /// <summary>
        ///     410 - Imunidade e não incidência
        /// </summary>
        [Description("Imunidade e não incidência")]
        [XmlEnum("410")]
        Cst410,
        
        /// <summary>
        ///     510 - Diferimento
        /// </summary>
        [Description("Diferimento")]
        [XmlEnum("510")]
        Cst510,
        
        /// <summary>
        ///     550 - Suspensão
        /// </summary>
        [Description("Suspensão")]
        [XmlEnum("550")]
        Cst550,
        
        /// <summary>
        ///     620 - Tributação Monofásica
        /// </summary>
        [Description("Tributação Monofásica")]
        [XmlEnum("620")]
        Cst620,
        
        /// <summary>
        ///     800 - Transferência de crédito
        /// </summary>
        [Description("Transferência de crédito")]
        [XmlEnum("800")]
        Cst800,
        
        /// <summary>
        ///     810 - Ajustes
        /// </summary>
        [Description("Ajustes")]
        [XmlEnum("810")]
        Cst810,
        
        /// <summary>
        ///     820 - Tributação em declaração de regime específico
        /// </summary>
        [Description("Tributação em declaração de regime específico")]
        [XmlEnum("820")]
        Cst820,
        
        /// <summary>
        ///     830 - Exclusão da Base de Cálculo
        /// </summary>
        [Description("Exclusão da Base de Cálculo")]
        [XmlEnum("830")]
        Cst830
    }

    public enum ClassificacaoCreditoPresumidoIbsZfmTipos
    {
        /// <summary>
        ///     0 - Sem Crédito Presumido
        /// </summary>
        [Description("Sem Crédito Presumido")]
        [XmlEnum("0")]
        tpCredPresIbsZfm0,
        
        /// <summary>
        ///     1 - Bens de consumo final (55%)
        /// </summary>
        [Description("Bens de consumo final (55%)")]
        [XmlEnum("1")]
        tpCredPresIbsZfm1,
        
        /// <summary>
        ///     2 - Bens de capital (75%)
        /// </summary>
        [Description("Bens de capital (75%)")]
        [XmlEnum("2")]
        tpCredPresIbsZfm2,
        
        /// <summary>
        ///     3 - Bens intermediários (90,25%)
        /// </summary>
        [Description("Bens intermediários (90,25%)")]
        [XmlEnum("3")]
        tpCredPresIbsZfm3,
        
        /// <summary>
        ///     4 - Bens de informática e outros definidos em legislação (100%)
        /// </summary>
        [Description("Bens de informática e outros definidos em legislação (100%)")]
        [XmlEnum("4")]
        tpCredPresIbsZfm4
    }
}