using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.ConsultaCadastro
{
    /// <summary>
    ///     Situação do contribuinte:
    ///     <para>0 - Não habilitado;</para>
    ///     <para>1 - Habilitado.</para>
    /// </summary>
    public enum SituacaoContribuinte
    {
        /// <summary>
        /// 0 - Não habilitado
        /// </summary>
        [Description("Não habilitado")]
        [XmlEnum("0")]
        NaoHabilitado = 0,

        /// <summary>
        /// 1 - Habilitado
        /// </summary>
        [Description("Habilitado")]
        [XmlEnum("1")]
        Habilitado = 1
    }

    /// <summary>
    ///     Indicador de contribuinte credenciado a emitir NF-e.
    ///     <para>0 - Não credenciado para emissão da NF-e;</para>
    ///     <para>1 - Credenciado;</para>
    ///     <para>2 - Credenciado com obrigatoriedade para todas operações;</para>
    ///     <para>3 - Credenciado com obrigatoriedade parcial;</para>
    ///     <para>4 – A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir NF-e na SEFAZ consultada.</para>
    /// </summary>
    public enum IndicadorCredenciamentoNfe
    {
        /// <summary>
        /// 0 - Não credenciado para emissão da NF-e
        /// </summary>
        [Description("Não credenciado para emissão da NF-e")]
        [XmlEnum("0")]
        NaoCredenciado = 0,

        /// <summary>
        /// 1 - Credenciado
        /// </summary>
        [Description("Credenciado")]
        [XmlEnum("1")]
        Credenciado = 1,

        /// <summary>
        /// 2 - Credenciado com obrigatoriedade para todas operações
        /// </summary>
        [Description("Credenciado com obrigatoriedade para todas operações")]
        [XmlEnum("2")]
        CredenciadoTodasOperacoes = 2,

        /// <summary>
        /// 3 - Credenciado com obrigatoriedade parcial
        /// </summary>
        [Description("Credenciado com obrigatoriedade parcial")]
        [XmlEnum("3")]
        CredenciadoParcial = 3,

        /// <summary>
        /// 4 – A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir NF-e na SEFAZ consultada
        /// </summary>
        [Description("A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir NF-e na SEFAZ consultada")]
        [XmlEnum("4")]
        SemInformacaoSefaz = 4
    }

    /// <summary>
    ///     Indicador de contribuinte credenciado a emitir CT-e.
    ///     <para>0 - Não credenciado para emissão da CT-e;</para>
    ///     <para>1 - Credenciado;</para>
    ///     <para>2 - Credenciado com obrigatoriedade para todas operações;</para>
    ///     <para>3 - Credenciado com obrigatoriedade parcial;</para>
    ///     <para>4 – A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir CT-e na SEFAZ consultada.</para>
    /// </summary>
    public enum IndicadorCredenciamentoCte
    {
        /// <summary>
        /// 0 - Não credenciado para emissão da CT-e
        /// </summary>
        [Description("Não credenciado para emissão da CT-e")]
        [XmlEnum("0")] NaoCredenciado = 0,

        /// <summary>
        /// 1 - Credenciado
        /// </summary>
        [Description("Credenciado")]
        [XmlEnum("1")] Credenciado = 1,

        /// <summary>
        /// 2 - Credenciado com obrigatoriedade para todas operações
        /// </summary>
        [Description("Credenciado com obrigatoriedade para todas operações")]
        [XmlEnum("2")] CredenciadoTodasOperacoes = 2,

        /// <summary>
        /// 3 - Credenciado com obrigatoriedade parcial
        /// </summary>
        [Description("Credenciado com obrigatoriedade parcial")]
        [XmlEnum("3")] CredenciadoParcial = 3,

        /// <summary>
        /// 4 – A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir CT-e na SEFAZ consultada
        /// </summary>
        [Description("A SEFAZ não fornece a informação. Este indicador significa apenas que o contribuinte é credenciado para emitir CT-e na SEFAZ consultada")]
        [XmlEnum("4")] SemInformacaoSefaz = 4
    }

    /// <summary>
    ///     Tipo de documento a ser utilizado na consulta de cadastro.
    ///     <para>Ie - Inscrição Estadual</para>
    ///     <para>Cnpj - CNPJ</para>
    ///     <para>Cpf - CPF</para>
    /// </summary>
    public enum ConsultaCadastroTipoDocumento
    {
        Ie = 0,
        Cnpj = 1,
        Cpf = 2
    }
}