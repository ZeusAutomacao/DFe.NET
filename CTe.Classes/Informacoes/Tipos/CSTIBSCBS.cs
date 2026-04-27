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
    public enum CSTIBSCBS
    {
        [Description("Tributação integral")]
        [XmlEnum("000")]
        cst000 = 000,

        [Description("Tributação com alíquotas uniformes")]
        [XmlEnum("010")]
        cst010 = 010,

        [Description("Tributação com alíquotas uniformes reduzidas")]
        [XmlEnum("011")]
        cst011 = 011,

        [Description("Alíquota reduzida")]
        [XmlEnum("200")]
        cst200 = 200,

        [Description("Alíquota fixa")]
        [XmlEnum("220")]
        cst220 = 220,

        [Description("Alíquota fixa rateada")]
        [XmlEnum("221")]
        cst221 = 221,

        [Description("Redução de Base de Cálculo")]
        [XmlEnum("222")]
        cst222 = 222,

        [Description("Isenção")]
        [XmlEnum("400")]
        cst400 = 400,

        [Description("Imunidade e não incidência")]
        [XmlEnum("410")]
        cst410 = 410,

        [Description("Diferimento")]
        [XmlEnum("510")]
        cst510 = 510,

        [Description("Diferimento com redução de alíquota")]
        [XmlEnum("515")]
        cst515 = 515,

        [Description("Suspensão")]
        [XmlEnum("550")]
        cst550 = 550,

        [Description("Tributação Monofásica")]
        [XmlEnum("620")]
        cst620 = 620,

        [Description("Transferência de crédito")]
        [XmlEnum("800")]
        cst800 = 800,

        [Description("Ajuste de IBS na ZFM")]
        [XmlEnum("810")]
        cst810 = 810,

        [Description("Ajustes")]
        [XmlEnum("811")]
        cst811 = 811,

        [Description("Tributação em declaração de regime específico")]
        [XmlEnum("820")]
        cst820 = 820,

        [Description("Exclusão da Base de Cálculo")]
        [XmlEnum("830")]
        cst830 = 830,
    }
}
