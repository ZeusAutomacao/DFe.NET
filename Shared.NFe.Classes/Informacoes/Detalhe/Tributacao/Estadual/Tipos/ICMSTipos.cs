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
        /// <summary>
        /// 0-Nacional exceto as indicadas nos códigos 3, 4, 5 e 8
        /// </summary>
        [Description("Nacional exceto as indicadas nos códigos 3, 4, 5 e 8")]
        [XmlEnum("0")]
        OmNacional = 0,

        /// <summary>
        /// 1-Estrangeira - Importação direta
        /// </summary>
        [Description("Estrangeira - Importação direta")]
        [XmlEnum("1")]
        OmEstrangeiraImportacaoDireta = 1,

        /// <summary>
        /// 2-Estrangeira - Adquirida no mercado interno
        /// </summary>
        [Description("Estrangeira - Adquirida no mercado interno")]
        [XmlEnum("2")]
        OmEstrangeiraAdquiridaBrasil = 2,

        /// <summary>
        /// 3-Nacional, conteudo superior 40% e inferior ou igual a 70%
        /// </summary>
        [Description("Nacional, conteudo superior 40% e inferior ou igual a 70%")]
        [XmlEnum("3")]
        OmNacionalConteudoImportacaoSuperior40 = 3,

        /// <summary>
        /// 4-Nacional, processos produtivos básicos
        /// </summary>
        [Description("Nacional, processos produtivos básicos")]
        [XmlEnum("4")]
        OmNacionalProcessosBasicos = 4,

        /// <summary>
        /// 5-Nacional, conteudo inferior 40%
        /// </summary>
        [Description("Nacional, conteudo inferior 40%")]
        [XmlEnum("5")]
        OmNacionalConteudoImportacaoInferiorIgual40 = 5,

        /// <summary>
        /// 6-Estrangeira - Importação direta, com similar nacional, lista CAMEX
        /// </summary>
        [Description("Estrangeira - Importação direta, com similar nacional, lista CAMEX")]
        [XmlEnum("6")]
        OmEstrangeiraImportacaoDiretaSemSimilar = 6,

        /// <summary>
        /// 7-Estrangeira - mercado interno, sem simular,lista CAMEX
        /// </summary>
        [Description("Estrangeira - mercado interno, sem simular,lista CAMEX")]
        [XmlEnum("7")]
        OmEstrangeiraAdquiridaBrasilSemSimilar = 7,

        /// <summary>
        /// 8-Nacional, Conteúdo de Importação superior a 70%
        /// </summary>
        [Description("Nacional, Conteúdo de Importação superior a 70%")]
        [XmlEnum("8")]
        OmNacionalConteudoImportacaoSuperior70 = 8
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
    public enum Csticms
    {
        /// <summary>
        /// 00 - Tributada integralmente
        /// </summary>
        [Description("Tributada integralmente")]
        [XmlEnum("00")]
        Cst00,

        /// <summary>
        /// 10 - Tributada e com cobrança do ICMS por substituição tributária
        /// </summary>
        [Description("Tributada e com cobrança do ICMS por substituição tributária")]
        [XmlEnum("10")]
        Cst10,

        /// <summary>
        /// 10 - Tributada e com cobrança do ICMS por substituição tributária
        /// </summary>
        [Description("Tributada e com cobrança do ICMS por substituição tributária")]
        [XmlEnum("10")]
        CstPart10,

        /// <summary>
        /// 20 - Com redução de base de cálculo
        /// </summary>
        [Description("Com redução de base de cálculo")]
        [XmlEnum("20")]
        Cst20,

        /// <summary>
        /// 30 - Isenta ou não tributada e com cobrança do ICMS por substituição tributária
        /// </summary>
        [Description("Isenta ou não tributada e com cobrança do ICMS por substituição tributária")]
        [XmlEnum("30")]
        Cst30,

        /// <summary>
        /// 40 - Isenta
        /// </summary>
        [Description("Isenta")]
        [XmlEnum("40")]
        Cst40,

        /// <summary>
        /// 41 - Não tributada
        /// </summary>
        [Description("Não tributada")]
        [XmlEnum("41")]
        Cst41,

        /// <summary>
        /// 41 - Não tributada
        /// </summary>
        [Description("Não tributada")]
        [XmlEnum("41")]
        CstRep41,

        /// <summary>
        /// 50 - Suspensão
        /// </summary>
        [Description("Suspensão")]
        [XmlEnum("50")]
        Cst50,

        /// <summary>
        /// 51 - Diferimento
        /// </summary>
        [Description("Diferimento")]
        [XmlEnum("51")]
        Cst51,

        /// <summary>
        /// 60 - ICMS cobrado anteriormente por substituição tributária
        /// </summary>
        [Description("ICMS cobrado anteriormente por substituição tributária")]
        [XmlEnum("60")]
        Cst60,

        /// <summary>
        /// 60 - ICMS cobrado anteriormente por substituição tributária
        /// </summary>
        [XmlEnum("60")] CstRep60,

        /// <summary>
        /// 70 - Com redução de base de cálculo e cobrança do ICMS por substituição tributária
        /// </summary>
        [Description("Com redução de base de cálculo e cobrança do ICMS por substituição tributária")]
        [XmlEnum("70")]
        Cst70,

        /// <summary>
        /// 90 - Outras
        /// </summary>
        [Description("Outras")]
        [XmlEnum("90")]
        Cst90,

        /// <summary>
        /// 90 - Outras
        /// </summary>
        [Description("Outras")]
        [XmlEnum("90")]
        CstPart90
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
        /// <summary>
        /// 0 - Margem Valor Agregado (%)
        /// </summary>
        [Description("Margem Valor Agregado (%)")]
        [XmlEnum("0")]
        DbiMargemValorAgregado = 0,

        /// <summary>
        /// 1 - Pauta (valor)
        /// </summary>
        [Description("Pauta (valor)")]
        [XmlEnum("1")]
        DbiPauta = 1,

        /// <summary>
        /// 2 - Preço Tabelado Máximo (valor)
        /// </summary>
        [Description("Preço Tabelado Máximo (valor)")]
        [XmlEnum("2")]
        DbiPrecoTabelado = 2,

        /// <summary>
        /// 3 - Valor da Operação
        /// </summary>
        [Description("Valor da Operação")]
        [XmlEnum("3")]
        DbiValorOperacao = 3
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
    ///     <para>6 - Valor da Operação;</para>
    /// </summary>
    public enum DeterminacaoBaseIcmsSt
    {
        /// <summary>
        /// 0 – Preço tabelado ou máximo  sugerido
        /// </summary>
        [Description("Preço tabelado ou máximo  sugerido")]
        [XmlEnum("0")]
        DbisPrecoTabelado = 0,

        /// <summary>
        /// 1 - Lista Negativa (valor)
        /// </summary>
        [Description("Lista Negativa (valor)")]
        [XmlEnum("1")]
        DbisListaNegativa = 1,

        /// <summary>
        /// 2 - Lista Positiva (valor)
        /// </summary>
        [Description("Lista Positiva (valor)")]
        [XmlEnum("2")]
        DbisListaPositiva = 2,

        /// <summary>
        /// 3 - Lista Neutra (valor)
        /// </summary>
        [Description("Lista Neutra (valor)")]
        [XmlEnum("3")]
        DbisListaNeutra = 3,

        /// <summary>
        /// 4 - Margem Valor Agregado (%)
        /// </summary>
        [Description("Margem Valor Agregado (%)")]
        [XmlEnum("4")]
        DbisMargemValorAgregado = 4,

        /// <summary>
        /// 5 - Pauta (valor)
        /// </summary>
        [Description("Pauta (valor)")]
        [XmlEnum("5")]
        DbisPauta = 5,

        /// <summary>
        /// 6 - Valor da Operação
        /// </summary>
        [Description("Valor da Operação")]
        [XmlEnum("6")]
        DbisValordaOperacao = 6
    }

    #endregion

    #region Situação Tributária do CSOSN

    /// <summary>
    ///     <para>101 - Tributada pelo Simples Nacional com permissão de crédito.(v.2.0)</para>
    ///     <para>102 - Tributada pelo Simples Nacional sem permissão de crédito.</para>
    ///     <para>103 – Isenção do ICMS  no Simples Nacional para faixa de receita bruta.</para>
    ///     <para>201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária (v.2.0)</para>
    ///     <para>202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária</para>
    ///     <para>203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária (v.2.0)</para>
    ///     <para>300 – Imune.</para>
    ///     <para>400 – Não tributda pelo Simples Nacional (v.2.0)</para>
    ///     <para>500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação (v.2.0)</para>
    ///     <para>Tributação pelo ICMS 900 - Outros(v2.0)</para>
    /// </summary>
    public enum Csosnicms
    {
        /// <summary>
        /// 101 - Tributada pelo Simples Nacional com permissão de crédito
        /// </summary>
        [Description("Tributada pelo Simples Nacional com permissão de crédito")]
        [XmlEnum("101")]
        Csosn101 = 101,

        /// <summary>
        /// 102 - Tributada pelo Simples Nacional sem permissão de crédito
        /// </summary>
        [Description("Tributada pelo Simples Nacional sem permissão de crédito")]
        [XmlEnum("102")]
        Csosn102 = 102,

        /// <summary>
        /// 103 – Isenção do ICMS  no Simples Nacional para faixa de receita bruta
        /// </summary>
        [Description("Isenção do ICMS  no Simples Nacional para faixa de receita bruta")]
        [XmlEnum("103")]
        Csosn103 = 103,

        /// <summary>
        /// 201 - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária
        /// </summary>
        [Description("Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por Substituição Tributária")]
        [XmlEnum("201")]
        Csosn201 = 201,

        /// <summary>
        /// 202 - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária
        /// </summary>
        [Description("Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por Substituição Tributária")]
        [XmlEnum("202")]
        Csosn202 = 202,

        /// <summary>
        /// 203 - Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária
        /// </summary>
        [Description("Isenção do ICMS nos Simples Nacional para faixa de receita bruta e com cobrança do ICMS por Substituição Tributária")]
        [XmlEnum("203")]
        Csosn203 = 203,

        /// <summary>
        /// 300 – Imune
        /// </summary>
        [Description("Imune")]
        [XmlEnum("300")]
        Csosn300 = 300,

        /// <summary>
        /// 400 – Não tributada pelo Simples Nacional
        /// </summary>
        [Description("Não tributada pelo Simples Nacional")]
        [XmlEnum("400")]
        Csosn400 = 400,

        /// <summary>
        /// 500 – ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação
        /// </summary>
        [Description("ICMS cobrado anterirmente por substituição tributária (substituído) ou por antecipação")]
        [XmlEnum("500")]
        Csosn500 = 500,

        /// <summary>
        /// 900 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("900")]
        Csosn900 = 900
    }

    #endregion

    #region Motivo da desoneração do ICMS

    /// <summary>
    ///     <para>1 – Táxi;</para>
    ///     <para>2 – Deficiente Físico;</para>
    ///     <para>3 – Produtor Agropecuário;</para>
    ///     <para>4 – Frotista/Locadora;</para>
    ///     <para>5 – Diplomático/Consular;</para>
    ///     <para>6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações);</para>
    ///     <para>7 – SUFRAMA;</para>
    ///     <para>8 – Venda a Orgãos Publicos;</para>
    ///     <para>9 – Outros. (v2.0)</para>
    ///     <para>10 – Deficiente Condutor (Convênio ICMS 38/12). (v3.1)</para>
    ///     <para>11 – Deficiente não Condutor (Convênio ICMS 38/12). (v3.1)</para>
    ///     <para>12 – Fomento agropecuário</para>
    ///     <para>16 - Olimpíadas Rio 2016</para>
    ///     <para>90 - Solicitado pelo Fisco</para>
    /// </summary>
    public enum MotivoDesoneracaoIcms
    {
        /// <summary>
        /// 1 – Táxi
        /// </summary>
        [Description("Táxi")]
        [XmlEnum("1")]
        MdiTaxi = 1,

        /// <summary>
        /// 2 – Deficiente Físico
        /// </summary>
        [Description("Deficiente Físico")]
        [XmlEnum("2")]
        MdiDeficienteFisico = 2,

        /// <summary>
        /// 3 – Produtor Agropecuário
        /// </summary>
        [Description("Produtor Agropecuário")]
        [XmlEnum("3")]
        MdiProdutorAgropecuario = 3,

        /// <summary>
        /// 4 – Frotista/Locadora
        /// </summary>
        [Description("Frotista/Locadora")]
        [XmlEnum("4")]
        MdiFrotistaLocadora = 4,

        /// <summary>
        /// 5 – Diplomático/Consular
        /// </summary>
        [Description("Diplomático/Consular")]
        [XmlEnum("5")]
        MdiDiplomaticoConsular = 5,

        /// <summary>
        /// 6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações)
        /// </summary>
        [Description("Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações)")]
        [XmlEnum("6")]
        MdiAmazoniaLivreComercio = 6,

        /// <summary>
        /// 7 – SUFRAMA
        /// </summary>
        [Description("SUFRAMA")]
        [XmlEnum("7")]
        MdiSuframa = 7,

        /// <summary>
        /// 8 – Venda a Orgãos Publicos
        /// </summary>
        [Description("Venda a Orgãos Publicos")]
        [XmlEnum("8")]
        MdiVendaOrgaosPublicos = 8,

        /// <summary>
        /// 9 – Outros. (v2.0)
        /// </summary>
        [Description("Outros")]
        [XmlEnum("9")]
        MdiOutros = 9,

        /// <summary>
        /// 10 – Deficiente Condutor (Convênio ICMS 38/12). (v3.1)
        /// </summary>
        [Description("Deficiente Condutor (Convênio ICMS 38/12)")]
        [XmlEnum("10")]
        MdiDeficienteCondutor = 10,

        /// <summary>
        /// 11 – Deficiente não Condutor (Convênio ICMS 38/12). (v3.1)
        /// </summary>
        [Description("Deficiente não Condutor (Convênio ICMS 38/12)")]
        [XmlEnum("11")]
        MdiDeficienteNaoCondutor = 11,

        /// <summary>
        /// 12 – Fomento agropecuário
        /// </summary>
        [Description("Fomento agropecuário")]
        [XmlEnum("12")]
        MdiFomentoAgropecuario = 12,

        /// <summary>
        /// 16 - Olimpíadas Rio 2016
        /// </summary>
        [Description("Olimpíadas Rio 2016")]
        [XmlEnum("16")]
        MdiOlimpiadasRio2016 = 16,

        /// <summary>
        /// 90 - Solicitado pelo Fisco
        /// </summary>
        [Description("Solicitado pelo Fisco")]
        [XmlEnum("90")]
        MdiSolicitadoPeloFisco = 90
    }

    #endregion
}
