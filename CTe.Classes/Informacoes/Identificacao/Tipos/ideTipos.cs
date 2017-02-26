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
    public enum tpCTe
    {
        [XmlEnum("0")]
        Normal,
        [XmlEnum("1")]
        Complemento,
        [XmlEnum("2")]
        Anulacao,
        [XmlEnum("3")]
        Substituto
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
    ///     Formato de impressão do DACTE (1-DANFe Retrato; 2-DANFe Paisagem)
    /// </summary>
    public enum tpImp
    {
        [XmlEnum("1")]
        Retrado = 1,
        [XmlEnum("2")]
        Paisagem = 2
    }

    /// <summary>
    ///     Forma de emissão da CT-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>4 - Contingência EPEC pela SVC</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>8 - Contingência SVC-SP (SEFAZ Virtual de Contingência de SP)</para>
    /// </summary>
    public enum tpEmis
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
    ///     <para>Versão 2.0 / 1 - emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>Versão 2.0 / 2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3- emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum procEmi
    {
        [XmlEnum("0")]
        AplicativoContribuinte,
        [XmlEnum("1")]
        AvulsaFisco,
        [XmlEnum("2")]
        AvulsaContribuinte,
        [XmlEnum("3")]
        ContribuinteAplicativoFisco
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

    public enum modal
    {
        [XmlEnum("01")]
        rodoviario = 01,
        [XmlEnum("02")]
        aereo = 02,
        [XmlEnum("03")]
        aquaviario = 03,
        [XmlEnum("04")]
        ferroviario = 04,
        [XmlEnum("05")]
        dutoviario = 05,
        [XmlEnum("06")]
        multimodal = 06        
    }

    public enum tpServ
    {
        [XmlEnum("0")]
        normal = 0,
        [XmlEnum("1")]
        subcontratacao,
        [XmlEnum("2")]
        redespacho,
        [XmlEnum("3")]
        redespachoIntermediario,
        [XmlEnum("4")]
        servicoVinculadoMultimodal,
        [XmlEnum("6")]
        transportePessoas = 6,
        [XmlEnum("7")]
        transporteValores = 7,
        [XmlEnum("8")]
        excessoBagagem = 8
    }

    public enum retira
    {
        [XmlEnum("0")]
        Sim = 0,
        [XmlEnum("1")]
        Nao = 1
    }

    public enum forPag
    {
        [XmlEnum("0")]
        Pago,
        [XmlEnum("1")]
        Apagar,
        [XmlEnum("2")]
        Outros
    }

    public enum toma
    {
        [XmlEnum("0")]
        Remetente,
        [XmlEnum("1")]
        Expedidor,
        [XmlEnum("2")]
        Recebedor,
        [XmlEnum("3")]
        Destinatario,
        [XmlEnum("4")]
        Outros
    }

    public enum indIEToma
    {
        [XmlEnum("1")]
        ContribuinteIcms,
        [XmlEnum("2")]
        ContribuinteIsentoDeInscricao,
        [XmlEnum("9")]
        NaoContribuinte
    }

    public enum tpPer
    {
        [XmlEnum("0")]
        SemDataDefinida,
        [XmlEnum("1")]
        NaData,
        [XmlEnum("2")]
        AteAData,
        [XmlEnum("3")]
        ApartirDaData,
        [XmlEnum("4")]
        NoPeriodo
    }

    public enum tpHor
    {
        [XmlEnum("0")]
        SemHoraDefinida,
        [XmlEnum("1")]
        NoHorario,
        [XmlEnum("2")]
        AteOHorario,
        [XmlEnum("3")]
        ApartirDoHorario,
        [XmlEnum("4")]
        NoIntervaloDeTempo
    }

    public enum indSN
    {
        [XmlEnum("1")]
        Sim
    }

    public enum cUnid
    {
        [XmlEnum("00")]
        M3 = 00,
        [XmlEnum("01")]
        KG = 01,
        [XmlEnum("02")]
        TON = 02,
        [XmlEnum("03")]
        UNIDADE = 03,
        [XmlEnum("04")]
        LITROS = 04,
        [XmlEnum("05")]
        MMBTU = 05
    }

    public enum mod
    {
        [XmlEnum("01")]
        NFModelo01A1eAvulsa,
        [XmlEnum("04")]
        NFdeProdutor
    }

    public enum tpUnidCarga
    {
        [XmlEnum("1")]
        Container = 1,
        [XmlEnum("2")]
        ULD = 2,
        [XmlEnum("3")]
        Pallet = 3,
        [XmlEnum("4")]
        Outros = 4
    }

    public enum tpUnidTransp
    {
        [XmlEnum("1")]
        RodoviarioTracao = 1,
        [XmlEnum("2")]
        RodoviarioReboque = 2,
        [XmlEnum("3")]
        Navio = 3,
        [XmlEnum("4")]
        Balsa = 4,
        [XmlEnum("5")]
        Aeronave = 5,
        [XmlEnum("6")]
        Vagao = 6,
        [XmlEnum("7")]
        Outros = 7
    }

    public enum tpDoc
    {
        [XmlEnum("00")]
        Declaracao = 00,
        [XmlEnum("10")]
        Dutoviario = 10,
        [XmlEnum("59")]
        CFeSAT = 59,
        [XmlEnum("65")]
        NFCe = 65,
        [XmlEnum("99")]
        Outros = 99
    }

    public enum tpDocAnterior
    {
        [XmlEnum("00")]
        CTRC = 00,
        [XmlEnum("01")]
        CTAC = 01,
        [XmlEnum("02")]
        ACT = 02,
        [XmlEnum("03")]
        NFModelo7 = 03,
        [XmlEnum("04")]
        NFModelo27 = 04,
        [XmlEnum("05")]
        ConhecimentoAereoNacional = 05,
        [XmlEnum("06")]
        CTMC = 06,
        [XmlEnum("07")]
        ARTE = 07,
        [XmlEnum("08")]
        DTA = 08,
        [XmlEnum("09")]
        ConhecimentoAereoInternacional = 09,
        [XmlEnum("10")]
        ConhecimentoCartaDePorteInternacional = 10,
        [XmlEnum("11")]
        ConhecimentoAvulso = 11,
        [XmlEnum("12")]
        TIF = 12,
        [XmlEnum("99")]
        Outros = 99
    }

    public enum respSeg
    {
        [XmlEnum("0")]
        Remetente = 0,
        [XmlEnum("1")]
        Expedidor = 1,
        [XmlEnum("2")]
        Recebedor = 2,
        [XmlEnum("3")]
        Destinatario = 3,
        [XmlEnum("4")]
        EmitenteDoCTe = 4,
        [XmlEnum("5")]
        TomadorDoServico = 5
    }

    public enum versaoModal
    {
        [XmlEnum("2.00")]
        veM200,
        [XmlEnum("3.00")]
        veM300
    }
}