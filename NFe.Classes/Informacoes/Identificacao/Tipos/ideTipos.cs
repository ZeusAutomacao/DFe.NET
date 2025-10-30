using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Identificacao.Tipos
{
    /// <summary>
    ///     Indicador da forma de pagamento
    ///     <para>0 – Pagamento à vista;</para>
    ///     <para>1 – Pagamento à prazo;</para>
    ///     <para>2 - Outros.</para>
    /// </summary>
    public enum IndicadorPagamento
    {
        /// <summary>
        /// 0 – Pagamento à vista
        /// </summary>
        [Description("Pagamento à vista")]
        [XmlEnum("0")]
        ipVista = 0,

        /// <summary>
        /// 1 – Pagamento à prazo
        /// </summary>
        [Description("Pagamento à prazo")]
        [XmlEnum("1")]
        ipPrazo = 1,

        /// <summary>
        /// 2 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("2")]
        ipOutras = 2
    }

    /// <summary>
    ///     Indicador da Forma de Pagamento
    ///     <para>0 – Pagamento à vista;</para>
    ///     <para>1 – Pagamento à prazo;</para>
    /// </summary>
    public enum IndicadorPagamentoDetalhePagamento
    {
        /// <summary>
        /// 0 – Pagamento à vista
        /// </summary>
        [Description("Pagamento à vista")]
        [XmlEnum("0")]
        ipDetPgVista = 0,

        /// <summary>
        /// 1 – Pagamento à prazo
        /// </summary>
        [Description("Pagamento à prazo")]
        [XmlEnum("1")]
        ipDetPgPrazo = 1,
    }

    /// <summary>
    ///     Tipo do Documento Fiscal
    ///     <para>0 - Entrada;</para>
    ///     <para>1 - Saída</para>
    /// </summary>
    public enum TipoNFe
    {
        /// <summary>
        /// 0 - Entrada
        /// </summary>
        [Description("Entrada")]
        [XmlEnum("0")]
        tnEntrada = 0,

        /// <summary>
        /// 1 - Saída
        /// </summary>
        [Description("Saída")]
        [XmlEnum("1")]
        tnSaida = 1
    }

    /// <summary>
    ///     Identificador de Local de destino da operação
    ///     <para>1 - Operação interna;</para>
    ///     <para>2 - Operação interestadual;</para>
    ///     <para>3 - Operação com exterior.</para>
    /// </summary>
    public enum DestinoOperacao
    {
        /// <summary>
        /// 1 - Operação interna
        /// </summary>
        [Description("Operação interna")]
        [XmlEnum("1")]
        doInterna = 1,

        /// <summary>
        /// 2 - Operação interestadual
        /// </summary>
        [Description("Operação interestadual")]
        [XmlEnum("2")]
        doInterestadual = 2,

        /// <summary>
        /// 3 - Operação com exterior
        /// </summary>
        [Description("Operação com exterior")]
        [XmlEnum("3")]
        doExterior = 3
    }

    /// <summary>
    ///     Formato de impressão do DANFE
    ///     <para>0 - Sem DANFE;</para>
    ///     <para>1 - DANFe Retrato;</para>
    ///     <para>2 - DANFe Paisagem;</para>
    ///     <para>3 - DANFe Simplificado;</para>
    ///     <para>4 - DANFe NFC-e;</para>
    ///     <para>5 - DANFe NFC-e em mensagem eletrônica</para>
    /// </summary>
    public enum TipoImpressao
    {
        /// <summary>
        /// 0 - Sem DANFE
        /// </summary>
        [Description("Sem DANFE")]
        [XmlEnum("0")]
        tiSemGeracao = 0,

        /// <summary>
        /// >1 - DANFe Retrato
        /// </summary>
        [Description("DANFe Retrato")]
        [XmlEnum("1")]
        tiRetrato = 1,

        /// <summary>
        /// 2 - DANFe Paisagem
        /// </summary>
        [Description("DANFe Paisagem")]
        [XmlEnum("2")]
        tiPaisagem = 2,

        /// <summary>
        /// 3 - DANFe Simplificado
        /// </summary>
        [Description("DANFe Simplificado")]
        [XmlEnum("3")]
        tiSimplificado = 3,

        /// <summary>
        /// 4 - DANFe NFC-e
        /// </summary>
        [Description("DANFe NFC-e")]
        [XmlEnum("4")]
        tiNFCe = 4,

        /// <summary>
        /// 5 - DANFe NFC-e em mensagem eletrônica
        /// </summary>
        [Description("DANFe NFC-e em mensagem eletrônica")]
        [XmlEnum("5")]
        tiMsgEletronica = 5
    }

    /// <summary>
    ///     Forma de emissão da NF-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)</para>
    ///     <para>4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>9 - Contingência off-line da NFC-e</para>
    ///     <para>Nota: Para a NFC-e somente estão disponíveis e são válidas as opções de contingência 5 e 9</para>
    /// </summary>
    public enum TipoEmissao
    {
        /// <summary>
        /// 1 - Emissão normal (não em contingência)
        /// </summary>
        [Description("Normal")]
        [XmlEnum("1")]
        teNormal = 1,

        /// <summary>
        /// 2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança
        /// </summary>
        [Description("Contingência FS-IA")]
        [XmlEnum("2")]
        teFSIA = 2,

        /// <summary>
        /// 3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)
        /// </summary>
        [Description("Contingência SCAN")]
        [XmlEnum("3")]
        teSCAN = 3,

        /// <summary>
        /// 4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)
        /// </summary>
        [Description("Contingência DPEC")]
        [XmlEnum("4")]
        teEPEC = 4,

        /// <summary>
        /// 5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança
        /// </summary>
        [Description("Contingência FS-DA")]
        [XmlEnum("5")]
        teFSDA = 5,

        /// <summary>
        /// 6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)
        /// </summary>
        [Description("Contingência SVC-AN")]
        [XmlEnum("6")]
        teSVCAN = 6,

        /// <summary>
        /// 7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)
        /// </summary>
        [Description("Contingência SVC-RS")]
        [XmlEnum("7")]
        teSVCRS = 7,

        /// <summary>
        /// 9 - Contingência off-line da NFC-e
        /// </summary>
        [Description("Contingência off-line")]
        [XmlEnum("9")]
        teOffLine = 9
    }

    public enum TipoNFeCredito
    {
        [Description("Multa e juros")]
        [XmlEnum("01")]
        tcMultaEJuros = 01,

        [Description("Apropriação de crédito presumido de IBS sobre o saldo devedor na ZFM (art. 450, § 1º, LC 214/25)")]
        [XmlEnum("02")]
        tcApropiacaoCreditoPresumido = 02,

        [Description("Retorno por recusa na entrega ou por não localização do destinatário na tentativa de entrega")]
        [XmlEnum("03")]
        tcRetornoRecusaNaoLocalizacaoEntrega = 03,

        [Description("Redução de valores")]
        [XmlEnum("04")]
        tcReducaoValores = 04,

        [Description("Transferência de crédito na sucessão")]
        [XmlEnum("05")]
        tcTransferenciaCreditoSucessao = 05
    }

    public enum TipoNFeDebito
    {
        [Description("Transferência de créditos para Cooperativas;")]
        [XmlEnum("01")]
        tdTransferenciaCreditoCooperativa = 01,

        [Description("Anulação de Crédito por Saídas Imunes/Isentas;")]
        [XmlEnum("02")]
        tdAnulacao = 02,

        [Description("Débitos de notas fiscais não processadas na apuração;")]
        [XmlEnum("03")]
        tdDebitosNaoProcessadas = 03,

        [Description("Multa e juros;")]
        [XmlEnum("04")]
        tdMultaJuros = 04,

        [Description("Transferência de crédito de sucessão;")]
        [XmlEnum("05")]
        tdTransferenciaCreditoSucessao = 05,

        [Description("Pagamento antecipado")]
        [XmlEnum("06")]
        tdPagamentoAntecipado = 06,

        [Description("Perda em estoque")]
        [XmlEnum("07")]
        tdPerdaEmEstoque = 07,

        [Description("Desenquadramento do SN")]
        [XmlEnum("08")]
        tdDesenquadramentoSN = 08
    }

    /// <summary>
    ///     Finalidade da emissão da NF-e
    ///     <para>1 - NFe normal</para>
    ///     <para>2 - NFe complementar</para>
    ///     <para>3 - NFe de ajuste</para>
    ///     <para>4 - Devolução de mercadoria</para>
    /// </summary>
    public enum FinalidadeNFe
    {
        /// <summary>
        /// 1 - NFe normal
        /// </summary>
        [Description("NFe normal")]
        [XmlEnum("1")]
        fnNormal = 1,

        /// <summary>
        /// 2 - NFe complementar
        /// </summary>
        [Description("NFe complementar")]
        [XmlEnum("2")]
        fnComplementar = 2,

        /// <summary>
        /// 3 - NFe de ajuste
        /// </summary>
        [Description("NFe de ajuste")]
        [XmlEnum("3")]
        fnAjuste = 3,

        /// <summary>
        /// 4 - Devolução de mercadoria
        /// </summary>
        [Description("Devolução de mercadoria")]
        [XmlEnum("4")]
        fnDevolucao = 4,

        [Description("Nota de Crédito")]
        [XmlEnum("5")]
        fnNotaCredito = 5,

        [Description("Nota de Débito")]
        [XmlEnum("6")]
        fnNotaDebito = 6,
    }

    /// <summary>
    ///     Processo de emissão utilizado com a seguinte codificação:
    ///     <para>0 - Emissão de NF-e com aplicativo do contribuinte;</para>
    ///     <para>1 - Emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>2 - Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3 - Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum ProcessoEmissao
    {
        /// <summary>
        /// 0 - Emissão de NF-e com aplicativo do contribuinte
        /// </summary>
        [Description("Emissão de NF-e com aplicativo do contribuinte")]
        [XmlEnum("0")]
        peAplicativoContribuinte = 0,

        /// <summary>
        /// 1 - Emissão de NF-e avulsa pelo Fisco
        /// </summary>
        [Description("Emissão de NF-e avulsa pelo Fisco")]
        [XmlEnum("1")]
        peAvulsaFisco = 1,

        /// <summary>
        /// 2 - Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco
        /// </summary>
        [Description("Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco")]
        [XmlEnum("2")]
        peAvulsaContribuinte = 2,

        /// <summary>
        /// 3 - Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco
        /// </summary>
        [Description("Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco")]
        [XmlEnum("3")]
        peContribuinteAplicativoFisco = 3
    }

    /// <summary>
    ///     Indica operação com Consumidor final
    ///     <para>0 - Normal;</para>
    ///     <para>1 - Consumidor final;</para>
    /// </summary>
    public enum ConsumidorFinal
    {
        /// <summary>
        /// 0 - Normal
        /// </summary>
        [Description("Normal")]
        [XmlEnum("0")]
        cfNao = 0,

        /// <summary>
        /// 1 - Consumidor final
        /// </summary>
        [Description("Consumidor final")]
        [XmlEnum("1")]
        cfConsumidorFinal = 1
    }

    /// <summary>
    ///     Indicador de presença do comprador no estabelecimento comercial no momento da operação
    ///     <para>0 - Não se aplica</para>
    ///     <para>1 - Operação presencial;</para>
    ///     <para>2 - Operação não presencial, pela Internet;</para>
    ///     <para>3 - Operação não presencial, Teleatendimento;</para>
    ///     <para>4 - NFC-e em operação com entrega a domicílio;</para>
    ///     <para>5 - Operação presencial, fora do estabelecimento;</para>
    ///     <para>9 - Operação não presencial, outros.</para>
    /// </summary>
    public enum PresencaComprador
    {
        /// <summary>
        /// 0 - Não se aplica
        /// </summary>
        [Description("Não se aplica")]
        [XmlEnum("0")]
        pcNao = 0,

        /// <summary>
        /// 1 - Operação presencial
        /// </summary>
        [Description("Operação presencial")]
        [XmlEnum("1")]
        pcPresencial = 1,

        /// <summary>
        /// 2 - Operação não presencial, pela Internet
        /// </summary>
        [Description("Operação não presencial, pela Internet")]
        [XmlEnum("2")]
        pcInternet = 2,

        /// <summary>
        /// 3 - Operação não presencial, Teleatendimento
        /// </summary>
        [Description("Operação não presencial, Teleatendimento")]
        [XmlEnum("3")]
        pcTeleatendimento = 3,

        /// <summary>
        /// 4 - NFC-e em operação com entrega a domicílio
        /// </summary>
        [Description("NFC-e em operação com entrega a domicílio")]
        [XmlEnum("4")]
        pcEntregaDomicilio = 4,

        /// <summary>
        /// 5 - Operação presencial, fora do estabelecimento
        /// </summary>
        [Description("Operação presencial, fora do estabelecimento")]
        [XmlEnum("5")]
        pcPresencialForaEstabelecimento = 5,

        /// <summary>
        /// 9 - Operação não presencial, outros
        /// </summary>
        [Description("Operação não presencial, outros")]
        [XmlEnum("9")]
        pcOutros = 9
    }

    /// <summary>
    ///     Indicador de intermediador/marketplace
    ///     <para>0 - Operação sem intermediador (em site ou plataforma própria)</para>
    ///     <para>1 - Operação em site ou plataforma de terceiros (intermediadores / marketplace)</para>
    /// </summary>
    public enum IndicadorIntermediador
    {
        /// <summary>
        /// 0 - Operação sem intermediador (em site ou plataforma própria)
        /// </summary>
        [Description("Operação sem intermediador (em site ou plataforma própria)")]
        [XmlEnum("0")]
        iiSemIntermediador = 0,

        /// <summary>
        ///  1 - Operação em site ou plataforma de terceiros (intermediadores / marketplace)
        /// </summary>
        [Description("Operação em site ou plataforma de terceiros (intermediadores / marketplace)")]
        [XmlEnum("1")]
        iiSitePlataformaTerceiros = 1
    }

    /// <summary>
    ///     Indicador do tipo de Operação do CSC
    ///     <para>1 - Consulta CSC Ativos;</para>
    ///     <para>2 - Solicita novo CSC;</para>
    ///     <para>3 - Revoga CSC Ativo</para>
    /// </summary>
    public enum IdentificadorOperacaoCsc
    {
        /// <summary>
        /// 1 - Consulta CSC Ativos
        /// </summary>
        [Description("Consulta CSC Ativos")]
        [XmlEnum("1")]
        ioConsultaCscAtivos = 1,

        /// <summary>
        /// 2 - Solicita novo CSC
        /// </summary>
        [Description("Solicita novo CSC")]
        [XmlEnum("2")]
        ioSolicitaNovoCsc = 2,

        /// <summary>
        /// 3 - Revoga CSC Ativo
        /// </summary>
        [Description("Revoga CSC Ativo")]
        [XmlEnum("3")]
        ioRevogaCscAtivo = 3
    }

    /// <summary>
    ///     Modelo do Documento Fiscal
    ///     <para>01 - Modelo 01</para>
    ///     <para>02 - Modelo 02</para>
    /// </summary>
    public enum refMod
    {
        /// <summary>
        /// 01 - Modelo 01
        /// </summary>
        [Description("Modelo 01")]
        [XmlEnum("01")]
        modelo = 1,

        /// <summary>
        /// 02 - Modelo 02
        /// </summary>
        [Description("Modelo 02")]
        [XmlEnum("02")]
        modelo2 = 2
    }

    
    public enum TipoEnteGov
    {
        [Description("União")]
        [XmlEnum("1")]
        Uniao = 1,

        [Description("Estado")]
        [XmlEnum("2")]
        Estado = 2,

        [Description("Distrito Federal")]
        [XmlEnum("3")]
        DistritoFederal = 3,

        [Description("Município")]
        [XmlEnum("4")]
        Municipio = 4
    }

    public enum TipoOperGov
    {
        [Description("Fornecimento")]
        [XmlEnum("1")]
        Fornecimento = 1,

        [Description("Recebimento do pagamento, conforme fato gerador do IBS/CBS definido no Art. 10 § 2º")]
        [XmlEnum("2")]
        RecebimentoPagamento = 2
    }

    public enum indBemMovelUsado
    {
        [Description("Bem Móvel Usado")]
        [XmlEnum("1")]
        BemMovelUsado = 1,
    }
}