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

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Pagamento
{
    /// <summary>
    ///     <para>01=Dinheiro</para>
    ///     <para>02=Cheque</para>
    ///     <para>03=Cartão de Crédito</para>
    ///     <para>04=Cartão de Débito</para>
    ///     <para>05=Crédito Loja</para>
    ///     <para>10=Vale Alimentação</para>
    ///     <para>11=Vale Refeição</para>
    ///     <para>12=Vale Presente</para>
    ///     <para>13=Vale Combustível</para>
    ///     <para>14=Duplicata Mercantil (versão 4.00)</para>
    ///     <para>15=Boleto Bancário (versão 4.00)</para>
    ///     <para>90=Sem pagamento (versão 4.00)</para>
    ///     <para>99=Outros</para>
    /// </summary>
    public enum FormaPagamento
    {
        [Description("Dinheiro")] [XmlEnum("01")] fpDinheiro,

        [Description("Cheque")] [XmlEnum("02")] fpCheque,

        [Description("Cartão de Crédito")] [XmlEnum("03")] fpCartaoCredito,

        [Description("Cartão de Débito")] [XmlEnum("04")] fpCartaoDebito,

        [Description("Crédito Loja")] [XmlEnum("05")] fpCreditoLoja,

        [Description("Vale Alimentação")] [XmlEnum("10")] fpValeAlimentacao,

        [Description("Vale Refeição")] [XmlEnum("11")] fpValeRefeicao,

        [Description("Vale Presente")] [XmlEnum("12")] fpValePresente,

        [Description("Vale Combustível")] [XmlEnum("13")] fpValeCombustivel,

        /// <summary>
        /// Foi excluido pela NT 2016. 002 v1.50
        /// Continuara aqui pois a mesma alguém já pode ter utilizado
        /// </summary>
        [Obsolete("Foi excluido pela NT 2016. 002 v1.50, Continua pois a mesma pode ter sido utilizada já")]
        [Description("Duplicata Mercantil")] [XmlEnum("14")] fpDuplicataMercantil, // VERSÃO 4.00

        [Description("Boleto Bancário")] [XmlEnum("15")] fpBoletoBancario, // VERSÃO 4.00

        [Description("Sem pagamento")] [XmlEnum("90")] fpSemPagamento, // VERSÃO 4.00

        [Description("Outros")] [XmlEnum("99")] fpOutro
    }

    /// <summary>
    ///     <para>01=Visa</para>
    ///     <para>02=Mastercard</para>
    ///     <para>03=American Express</para>
    ///     <para>04=Sorocred</para>
    ///     <para>05=Diners Club (versão 4.00)</para>
    ///     <para>06=Elo (versão 4.00)</para>
    ///     <para>07=Hipercard (versão 4.00)</para>
    ///     <para>08=Aura (versão 4.00)</para>
    ///     <para>09=Cabal (versão 4.00)</para>
    ///     <para>99=Outros</para>
    /// </summary>
    public enum BandeiraCartao
    {
        [Description("Visa")] [XmlEnum("01")] bcVisa,

        [Description("Mastercard")] [XmlEnum("02")] bcMasterCard,

        [Description("American Express")] [XmlEnum("03")] bcAmericanExpress,

        [Description("Sorocred")] [XmlEnum("04")] bcSorocred,

        [Description("Diners Club")] [XmlEnum("05")] bcDinersClub,

        [Description("Elo")] [XmlEnum("06")] Elo,

        [Description("Hipercard")] [XmlEnum("07")] Hipercard,

        [Description("Aura")] [XmlEnum("08")] Aura,

        [Description("Cabal")] [XmlEnum("09")] Cabal,

        [Description("Outros")] [XmlEnum("99")] bcOutros
    }

    /// <summary>
    ///     <para>1=Pagamento integrado com o sistema de automação da empresa(Ex.: equipamento TEF, Comércio Eletrônico);</para>
    ///     <para>Pagamento não integrado com o sistema de automação da empresa(Ex.: equipamento POS);</para>
    /// </summary>
    public enum TipoIntegracaoPagamento
    {
        [XmlEnum("1")]
        TipIntegradoAutomacao = 1,

        [XmlEnum("2")]
        TipNaoIntegrado = 2
    }
}