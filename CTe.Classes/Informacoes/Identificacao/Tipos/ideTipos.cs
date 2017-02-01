using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Identificacao.Tipos
{
    /// <summary>
    ///     Indicador da forma de pagamento
    ///     <para>0 – pagamento à vista;</para>
    ///     <para>1 – pagamento à prazo;</para>
    ///     <para>2 - outros.</para>
    /// </summary>
    public enum IndicadorPagamento
    {
        [XmlEnum("0")]
        ipVista,
        [XmlEnum("1")]
        ipPrazo,
        [XmlEnum("2")]
        ipOutras
    }

    /// <summary>
    ///     Tipo do Documento Fiscal (0 - CT-e Normal; 1 - CT-e de Complemento de Valores; 2 - CT-e de Anulação; 3 - CT-e Substituto)
    /// </summary>
    public enum TipoCTe
    {
        [XmlEnum("0")]
        tnNormal,
        [XmlEnum("1")]
        tnComplemento,
        [XmlEnum("2")]
        tnAnulacao,
        [XmlEnum("3")]
        tnSubstituto
    }

    /// <summary>
    ///     Identificador de Local de destino da operação (1-Interna;2-Interestadual;3-Exterior)
    /// </summary>
    public enum DestinoOperacao
    {
        [XmlEnum("1")]
        doInterna,
        [XmlEnum("2")]
        doInterestadual,
        [XmlEnum("3")]
        doExterior
    }

    /// <summary>
    ///     Formato de impressão do DANFE (0-sem DANFE;1-DANFe Retrato; 2-DANFe Paisagem;3-DANFe Simplificado; 4-DANFe
    ///     NFC-e;5-DANFe NFC-e em mensagem eletrônica)
    /// </summary>
    public enum TipoImpressao
    {
        [XmlEnum("0")]
        tiSemGeracao = 0,
        [XmlEnum("1")]
        tiRetrato = 1,
        [XmlEnum("2")]
        tiPaisagem = 2,
        [XmlEnum("3")]
        tiSimplificado = 3,
        [XmlEnum("4")]
        tiNFCe = 4,
        [XmlEnum("5")]
        tiMsgEletronica = 5
    }

    /// <summary>
    ///     Forma de emissão da CT-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>8 - Contingência SVC-SP (SEFAZ Virtual de Contingência de SP)</para>
    /// </summary>
    public enum TipoEmissao
    {
        [XmlEnum("1")]
        teNormal = 1,
        [XmlEnum("4")]
        teEPEC = 4,
        [XmlEnum("5")]
        teFSDA = 5,
        [XmlEnum("7")]
        teSVCRS = 7,
        [XmlEnum("8")]
        teSVCSP = 8
    }

    /// <summary>
    ///     Finalidade da emissão da NF-e
    ///     <para>1 - NFe normal</para>
    ///     <para>2 - NFe complementar</para>
    ///     <para>3 - NFe de ajuste</para>
    ///     <para>4 - Devolução/Retorno</para>
    /// </summary>
    public enum FinalidadeNFe
    {
        [XmlEnum("1")]
        fnNormal,
        [XmlEnum("2")]
        fnComplementar,
        [XmlEnum("3")]
        fnAjuste,
        [XmlEnum("4")]
        fnDevolucao
    }

    /// <summary>
    ///     Processo de emissão utilizado com a seguinte codificação:
    ///     <para>0 - emissão de NF-e com aplicativo do contribuinte;</para>
    ///     <para>1 - emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3- emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum ProcessoEmissao
    {
        [XmlEnum("0")]
        peAplicativoContribuinte,
        [XmlEnum("1")]
        peAvulsaFisco,
        [XmlEnum("2")]
        peAvulsaContribuinte,
        [XmlEnum("3")]
        peContribuinteAplicativoFisco
    }

    /// <summary>
    ///     Indica operação com Consumidor final
    ///     <para>0=Normal;</para>
    ///     <para>1=Consumidor final;</para>
    /// </summary>
    public enum ConsumidorFinal
    {
        [XmlEnum("0")]
        cfNao,
        [XmlEnum("1")]
        cfConsumidorFinal
    }

    public enum PresencaComprador
    {
        [XmlEnum("0")]
        pcNao,
        [XmlEnum("1")]
        pcPresencial,
        [XmlEnum("2")]
        pcInternet,
        [XmlEnum("3")]
        pcTeleatendimento,
        [XmlEnum("4")]
        pcEntregaDomicilio,
        [XmlEnum("9")]
        pcOutros
    }

    /// <summary>
    /// Indicador do tipo de Operação do CSC
    /// <para>1 - Consulta CSC Ativos;</para>
    /// <para>2 - Solicita novo CSC;</para>
    /// <para>3 - Revoga CSC Ativo</para>
    /// </summary>
    public enum IdentificadorOperacaoCsc
    {
        [XmlEnum("1")]
        ioConsultaCscAtivos = 1,
        [XmlEnum("2")]
        ioSolicitaNovoCsc = 2,
        [XmlEnum("3")]
        ioRevogaCscAtivo = 3
    }
}