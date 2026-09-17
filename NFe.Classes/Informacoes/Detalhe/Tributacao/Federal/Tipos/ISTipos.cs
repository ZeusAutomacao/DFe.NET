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
    ///     CST para o Imposto Seletivo (IS) — MANTIDO APENAS COMO REFERÊNCIA/DOCUMENTAÇÃO.
    /// </summary>
    /// <remarks>
    ///     NÃO usar este enum para serialização: <see cref="Federal.IS.CSTIS"/> é
    ///     <c>string</c>, alinhado ao XSD oficial (DFeTiposBasicos_v1.00.xsd), onde CSTIS é do
    ///     tipo TCST — <c>xs:string</c> restrito somente pelo pattern <c>\d{3}</c>. A SEFAZ não
    ///     define lista fechada; a tabela é publicada por Informe Técnico e segue em revisão.
    ///
    ///     Enquanto isto era um enum, qualquer código não mapeado (caso real: '002') lançava
    ///     "Instance validation error: '002' is not a valid value for CSTIS" e derrubava a
    ///     consulta NFeDistribuicaoDFe inteira.
    /// </remarks>
    public enum CSTIS
    {
        /// <summary>
        ///     000 - Tributada integralmente
        /// </summary>
        [Description("Tributada integralmente")]
        [XmlEnum("000")]
        Is000
    }
}