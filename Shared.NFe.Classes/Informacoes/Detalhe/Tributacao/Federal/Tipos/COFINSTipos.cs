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
    ///     <para>01 - Operação Tributável (base de cálculo = valor da operação (alíquota normal (cumulativo/não cumulativo)))</para>
    ///     <para>02 - Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))</para>
    ///     <para>03 - Operação Tributável (base de cálculo = quantidade vendida (alíquota por unidade de produto))</para>
    ///     <para>04 - Operação Tributável (tributação monofásica (alíquota zero))</para>
    ///     <para>05 - Operação Tributável (Substituição Tributária)</para>
    ///     <para>06 - Operação Tributável (alíquota zero)</para>
    ///     <para>07 - Operação Isenta da Contribuição</para>
    ///     <para>08 - Operação Sem Incidência da Contribuição</para>
    ///     <para>09 - Operação com Suspensão da Contribuição</para>
    ///     <para>49 - Outras Operações de Saída</para>
    ///     <para>50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno</para>
    ///     <para>51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno</para>
    ///     <para>52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação</para>
    ///     <para>53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno</para>
    ///     <para>54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação</para>
    ///     <para>55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação</para>
    ///     <para>56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação</para>
    ///     <para>60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno</para>
    ///     <para>61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada no Mercado Interno</para>
    ///     <para>62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação</para>
    ///     <para>63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno</para>
    ///     <para>64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação</para>
    ///     <para>65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação</para>
    ///     <para>66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação</para>
    ///     <para>67 - Crédito Presumido - Outras Operações</para>
    ///     <para>70 - Operação de Aquisição sem Direito a Crédito</para>
    ///     <para>71 - Operação de Aquisição com Isenção</para>
    ///     <para>72 - Operação de Aquisição com Suspensão</para>
    ///     <para>73 - Operação de Aquisição a Alíquota Zero</para>
    ///     <para>74 - Operação de Aquisição sem Incidência da Contribuição</para>
    ///     <para>75 - Operação de Aquisição por Substituição Tributária</para>
    ///     <para>98 - Outras Operações de Entrada</para>
    ///     <para>99 - Outras Operações</para>
    /// </summary>
    public enum CSTCOFINS
    {
        /// <summary>
        /// 01 - Operação Tributável (base de cálculo = valor da operação (alíquota normal (cumulativo/não cumulativo)))
        /// </summary>
        [Description("Operação Tributável (base de cálculo = valor da operação (alíquota normal (cumulativo/não cumulativo)))")]
        [XmlEnum("01")]
        cofins01 = 01,

        /// <summary>
        /// 02 - Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))
        /// </summary>
        [Description("Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))")]
        [XmlEnum("02")]
        cofins02 = 02,

        /// <summary>
        /// 03 - Operação Tributável (base de cálculo = quantidade vendida (alíquota por unidade de produto))
        /// </summary>
        [Description("Operação Tributável (base de cálculo = quantidade vendida (alíquota por unidade de produto))")]
        [XmlEnum("03")]
        cofins03 = 03,

        /// <summary>
        /// 04 - Operação Tributável (tributação monofásica (alíquota zero))
        /// </summary>
        [Description("Operação Tributável (tributação monofásica (alíquota zero))")]
        [XmlEnum("04")]
        cofins04 = 04,

        /// <summary>
        /// 05 - Operação Tributável (Substituição Tributária)
        /// </summary>
        [Description("Operação Tributável (Substituição Tributária)")]
        [XmlEnum("05")]
        cofins05 = 05,

        /// <summary>
        /// 06 - Operação Tributável (alíquota zero)
        /// </summary>
        [Description("Operação Tributável (alíquota zero)")]
        [XmlEnum("06")]
        cofins06 = 06,

        /// <summary>
        /// 07 - Operação Isenta da Contribuição
        /// </summary>
        [Description("Operação Isenta da Contribuição")]
        [XmlEnum("07")]
        cofins07 = 07,

        /// <summary>
        /// 08 - Operação Sem Incidência da Contribuição
        /// </summary>
        [Description("Operação Sem Incidência da Contribuição")]
        [XmlEnum("08")]
        cofins08 = 08,

        /// <summary>
        /// 09 - Operação com Suspensão da Contribuição
        /// </summary>
        [Description("Operação com Suspensão da Contribuição")]
        [XmlEnum("09")]
        cofins09 = 09,

        /// <summary>
        /// 49 - Outras Operações de Saída
        /// </summary>
        [Description("Outras Operações de Saída")]
        [XmlEnum("49")]
        cofins49 = 49,

        /// <summary>
        /// 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno")]
        [XmlEnum("50")]
        cofins50 = 50,

        /// <summary>
        /// 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno")]
        [XmlEnum("51")]
        cofins51 = 51,

        /// <summary>
        /// 52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação")]
        [XmlEnum("52")]
        cofins52 = 52,

        /// <summary>
        /// 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno")]
        [XmlEnum("53")]
        cofins53 = 53,

        /// <summary>
        /// 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        [XmlEnum("54")]
        cofins54 = 54,

        /// <summary>
        /// 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação")]
        [XmlEnum("55")]
        cofins55 = 55,

        /// <summary>
        /// 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação
        /// </summary>
        [Description("Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação")]
        [XmlEnum("56")]
        cofins56 = 56,

        /// <summary>
        /// 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno")]
        [XmlEnum("60")]
        cofins60 = 60,

        /// <summary>
        /// 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada no Mercado Interno
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada no Mercado Interno")]
        [XmlEnum("61")]
        cofins61 = 61,

        /// <summary>
        /// 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação")]
        [XmlEnum("62")]
        cofins62 = 62,

        /// <summary>
        /// 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno")]
        [XmlEnum("63")]
        cofins63 = 63,

        /// <summary>
        /// 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        [XmlEnum("64")]
        cofins64 = 64,

        /// <summary>
        /// 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação")]
        [XmlEnum("65")]
        cofins65 = 65,

        /// <summary>
        /// 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação
        /// </summary>
        [Description("Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação")]
        [XmlEnum("66")]
        cofins66 = 66,

        /// <summary>
        /// 67 - Crédito Presumido - Outras Operações
        /// </summary>
        [Description("Crédito Presumido - Outras Operações")]
        [XmlEnum("67")]
        cofins67 = 67,

        /// <summary>
        /// 70 - Operação de Aquisição sem Direito a Crédito
        /// </summary>
        [Description("Operação de Aquisição sem Direito a Crédito")]
        [XmlEnum("70")]
        cofins70 = 70,

        /// <summary>
        /// 71 - Operação de Aquisição com Isenção
        /// </summary>
        [Description("Operação de Aquisição com Isenção")]
        [XmlEnum("71")]
        cofins71 = 71,

        /// <summary>
        /// 72 - Operação de Aquisição com Suspensão
        /// </summary>
        [Description("Operação de Aquisição com Suspensão")]
        [XmlEnum("72")]
        cofins72 = 72,

        /// <summary>
        /// 73 - Operação de Aquisição a Alíquota Zero
        /// </summary>
        [Description("Operação de Aquisição a Alíquota Zero")]
        [XmlEnum("73")]
        cofins73 = 73,

        /// <summary>
        /// 74 - Operação de Aquisição sem Incidência da Contribuição
        /// </summary>
        [Description("Operação de Aquisição sem Incidência da Contribuição")]
        [XmlEnum("74")]
        cofins74 = 74,

        /// <summary>
        /// 75 - Operação de Aquisição por Substituição Tributária
        /// </summary>
        [Description("Operação de Aquisição por Substituição Tributária")]
        [XmlEnum("75")]
        cofins75 = 75,

        /// <summary>
        /// 98 - Outras Operações de Entrada
        /// </summary>
        [Description("Outras Operações de Entrada")]
        [XmlEnum("98")]
        cofins98 = 98,

        /// <summary>
        /// 99 - Outras Operações
        /// </summary>
        [Description("Outras Operações")]
        [XmlEnum("99")]
        cofins99 = 99
    }
}