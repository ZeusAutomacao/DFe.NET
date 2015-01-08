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
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos
{

    #region Origem da Mercadoria

    /// <summary>
    ///     <para>0-Nacional exceto as indicadas nos códigos 3, 4, 5 e 8;</para>
    ///     <para>1-Estrangeira - Importação direta;</para>
    ///     <para>2-Estrangeira - Adquirida no mercado interno;</para>
    ///     <para>3-Nacional, conteudo superior 40% e inferior ou igual a 70%;</para>
    ///     <para>4-Nacional, processos produtivos básicos;</para>
    ///     <para>5-Nacional, conteudo inferior 40%;</para>
    ///     <para>6-Estrangeira - Importação direta, com similar nacional, lista CAMEX;</para>
    ///     <para>7-Estrangeira - mercado interno, sem simular,lista CAMEX;</para>
    ///     <para>8-Nacional, Conteúdo de Importação superior a 70%.</para>
    /// </summary>
    public enum OrigemMercadoria
    {
        [XmlEnum("0")] omNacional,
        [XmlEnum("1")] omEstrangeiraImportacaoDireta,
        [XmlEnum("2")] omEstrangeiraAdquiridaBrasil,
        [XmlEnum("3")] omNacionalConteudoImportacaoSuperior40,
        [XmlEnum("4")] omNacionalProcessosBasicos,
        [XmlEnum("5")] omNacionalConteudoImportacaoInferiorIgual40,
        [XmlEnum("6")] omEstrangeiraImportacaoDiretaSemSimilar,
        [XmlEnum("7")] omEstrangeiraAdquiridaBrasilSemSimilar,
        [XmlEnum("8")] omNacionalConteudoImportacaoSuperior70
    }

    #endregion

    #region Situação Tributária do ICMS

    /// <summary>
    ///     <para>00 - Tributada integralmente</para>
    ///     <para>10 - Tributada e com cobrança do ICMS por substituição tributária</para>
    ///     <para>20 - Com redução de base de cálculo</para>
    ///     <para>30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária</para>
    ///     <para>40 - Isenta</para>
    ///     <para>41 - Não tributada</para>
    ///     <para>50 - Suspensão</para>
    ///     <para>51 - Diferimento</para>
    ///     <para>60 - ICMS cobrado anteriormente por substituição tributária</para>
    ///     <para>70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária</para>
    ///     <para>90 - Outras</para>
    /// </summary>
    public enum CSTICMS
    {
        [XmlEnum("00")] cst00,
        [XmlEnum("10")] cst10,
        [XmlEnum("20")] cst20,
        [XmlEnum("30")] cst30,
        [XmlEnum("40")] cst40,
        [XmlEnum("41")] cst41,
        [XmlEnum("50")] cst50,
        [XmlEnum("51")] cst51,
        [XmlEnum("60")] cst60,
        [XmlEnum("70")] cst70,
        [XmlEnum("90")] cst90
    }

    #endregion

    #region Modalidade de determinação da BC do ICMS

    /// <summary>
    ///     <para>0 - Margem Valor Agregado (%);</para>
    ///     <para>1 - Pauta (valor);</para>
    ///     <para>2 - Preço Tabelado Máximo (valor);</para>
    ///     <para>3 - Valor da Operação.</para>
    /// </summary>
    public enum DeterminacaoBaseIcms
    {
        [XmlEnum("0")] dbiMargemValorAgregado,
        [XmlEnum("1")] dbiPauta,
        [XmlEnum("2")] dbiPrecoTabelado,
        [XmlEnum("3")] dbiValorOperacao
    }

    #endregion

    #region Modalidade de determinação da BC do ICMS ST

    /// <summary>
    ///     <para>0 – Preço tabelado ou máximo  sugerido;</para>
    ///     <para>1 - Lista Negativa (valor);</para>
    ///     <para>2 - Lista Positiva (valor);</para>
    ///     <para>3 - Lista Neutra (valor);</para>
    ///     <para>4 - Margem Valor Agregado (%);</para>
    ///     <para>5 - Pauta (valor);</para>
    /// </summary>
    public enum DeterminacaoBaseIcmsST
    {
        [XmlEnum("0")] dbisPrecoTabelado,
        [XmlEnum("1")] dbisListaNegativa,
        [XmlEnum("2")] dbisListaPositiva,
        [XmlEnum("3")] dbisListaNeutra,
        [XmlEnum("4")] dbisMargemValorAgregado,
        [XmlEnum("5")] dbisPauta
    }

    #endregion

    #region Situação Tributária do CSOSN

    /// <summary>
    ///     <para>101 - Tributada pelo Simples Nacional com permissão de crédito. (v.2.0)</para>
    ///     <para>102 - Tributada pelo Simples Nacional sem permissão de crédito. </para>
    ///     <para>103 – Isenção do ICMS  no Simples Nacional para faixa de receita bruta.</para>
    ///     <para>
    ///         201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição
    ///         Tributária (v.2.0)
    ///     </para>
    ///     <para>
    ///         202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição
    ///         Tributária
    ///     </para>
    ///     <para>
    ///         203 -  Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por
    ///         Substituição Tributária (v.2.0)
    ///     </para>
    ///     <para>300 – Imune.</para>
    ///     <para>400 – Não tributda pelo Simples Nacional (v.2.0)</para>
    ///     <para>500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação (v.2.0)</para>
    ///     <para>Tributação pelo ICMS 900 - Outros(v2.0)</para>
    /// </summary>
    public enum CSOSNICMS
    {
        [XmlEnum("101")] csosn101,
        [XmlEnum("102")] csosn102,
        [XmlEnum("103")] csosn103,
        [XmlEnum("201")] csosn201,
        [XmlEnum("202")] csosn202,
        [XmlEnum("203")] csosn203,
        [XmlEnum("300")] csosn300,
        [XmlEnum("400")] csosn400,
        [XmlEnum("500")] csosn500,
        [XmlEnum("900")] csosn900
    }

    #endregion

    #region Motivo da desoneração do ICMS

    /// <summary>
    ///     <para>1 – Táxi;</para>
    ///     <para>2 – Deficiente Físico;</para>
    ///     <para>3 – Produtor Agropecuário;</para>
    ///     <para>4 – Frotista/Locadora;</para>
    ///     <para>5 – Diplomático/Consular;</para>
    ///     <para>
    ///         6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 –
    ///         CONTRAN e suas alterações);
    ///     </para>
    ///     <para>7 – SUFRAMA;</para>
    ///     <para>8 – Venda a Orgãos Publicos;</para>
    ///     <para>9 – outros. (v2.0)</para>
    ///     <para>10 – Deficiente Condutor (Convênio ICMS 38/12). (v3.1)</para>
    ///     <para>11 – Deficiente não Condutor (Convênio ICMS 38/12). (v3.1)</para>
    ///     <para>12 - Orgão Fomento</para>
    /// </summary>
    public enum MotivoDesoneracaoICMS
    {
        [XmlEnum("1")] mdiTaxi,

        [XmlEnum("2")] mdiDeficienteFisico,
        [XmlEnum("3")] mdiProdutorAgropecuario,
        [XmlEnum("4")] mdiFrotistaLocadora,
        [XmlEnum("5")] mdiDiplomaticoConsular,
        [XmlEnum("6")] mdiAmazoniaLivreComercio,
        [XmlEnum("7")] mdiSuframa,
        [XmlEnum("8")] mdiVendaOrgaosPublicos,
        [XmlEnum("9")] mdiOutros,
        [XmlEnum("10")] mdiDeficienteCondutor,
        [XmlEnum("11")] mdiDeficienteNaoCondutor,
        [XmlEnum("12")] mdiOrgaoFomento
    }

    #endregion
}