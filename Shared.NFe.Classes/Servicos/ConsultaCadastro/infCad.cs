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
namespace NFe.Classes.Servicos.ConsultaCadastro
{
    public class infCad
    {
        /// <summary>
        ///     GR08 - Inscrição estadual do contribuinte
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     GR09 - CNPJ do contribuinte
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     GR10 - CPF em caso de pessoa física com IE
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        ///     GR11 - O campo deve ser preenchido com a sigla da UF de localização do contribuinte. Em algumas situações, a UF de localização pode ser diferente da UF consultada. Ex. IE de
        ///     contribuinte inscrito como Substituto Tributário.
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     GR12 - Situação do contribuinte: 0 - não habilitado; 1 - habilitado.
        /// </summary>
        public SituacaoContribuinte cSit { get; set; }

        /// <summary>
        ///     GR12a - Indicador de contribuinte credenciado a emitir NF-e.
        ///     <para>0 - Não credenciado para emissão da NF-e;</para>
        ///     <para>1 - Credenciado;</para>
        ///     <para>2 - Credenciado com obrigatoriedade para todas operações;</para>
        ///     <para>3 - Credenciado com obrigatoriedade parcial;</para>
        ///     <para>4 – a SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir NF-e na SEFAZ consultada.</para>
        /// </summary>
        public IndicadorCredenciamentoNfe indCredNFe { get; set; }

        /// <summary>
        ///     GR12a - Indicador de contribuinte credenciado a emitir CT-e.
        ///     <para>0 - Não credenciado para emissão da CT-e;</para>
        ///     <para>1 - Credenciado;</para>
        ///     <para>2 - Credenciado com obrigatoriedade para todas operações;</para>
        ///     <para>3 - Credenciado com obrigatoriedade parcial;</para>
        ///     <para>4 – a SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir CT-e na SEFAZ consultada.</para>
        /// </summary>
        public IndicadorCredenciamentoCte indCredCTe { get; set; }

        /// <summary>
        ///     GR13 - Razão Social ou nome do Contribuinte
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        ///     GR13a - Nome Fantasia
        /// </summary>
        public string xFant { get; set; }

        /// <summary>
        ///     GR14 - Regime de Apuração do ICMS do Contribuinte
        /// </summary>
        public string xRegApur { get; set; }

        /// <summary>
        ///     GR15 - CNAE principal do contribuinte
        /// </summary>
        public string CNAE { get; set; }

        /// <summary>
        ///     GR16 - Data de Início da Atividade do Contribuinte
        /// </summary>
        public string dIniAtiv { get; set; }

        /// <summary>
        ///     GR17 - Data da última modificação da situação cadastral do contribuinte.
        /// </summary>
        public string dUltSit { get; set; }

        /// <summary>
        ///     GR18 - Data de ocorrência da baixa do contribuinte
        /// </summary>
        public string dBaixa { get; set; }

        /// <summary>
        ///     GR20 - IE única, este campo será informado quando o contribuinte possuir IE única.
        /// </summary>
        public string IEUnica { get; set; }

        /// <summary>
        ///     GR21 - IE atual (em caso de IE antiga consultada)
        /// </summary>
        public string IEAtual { get; set; }

        /// <summary>
        ///     GR22 - Endereço - grupo de informações opcionais.
        /// </summary>
        public ender ender { get; set; }
    }
}