using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Municipal
{
    /// <summary>
    ///     <para>1=Sim;</para>
    ///     <para>2=Não;</para>
    /// </summary>
    public enum indIncentivo
    {
        /// <summary>
        /// 1=Sim
        /// </summary>
        [Description("Sim")]
        [XmlEnum("1")]
        iiSim = 1,

        /// <summary>
        /// 2=Não
        /// </summary>
        [Description("Não")]
        [XmlEnum("2")]
        iiNao = 2
    }

    /// <summary>
    ///     <para>1=Exigível,</para>
    ///     <para>2=Não incidência;</para>
    ///     <para>3=Isenção;</para>
    ///     <para>4=Exportação;</para>
    ///     <para>5=Imunidade;</para>
    ///     <para>6=Exigibilidade Suspensa por Decisão Judicial;</para>
    ///     <para>7=Exigibilidade Suspensa por Processo Administrativo;</para>
    /// </summary>
    public enum IndicadorISS
    {
        /// <summary>
        /// 1=Exigível
        /// </summary>
        [Description("Exigível")]
        [XmlEnum("1")]
        iiExigivel = 1,

        /// <summary>
        /// 2=Não incidência
        /// </summary>
        [Description("Não incidência")]
        [XmlEnum("2")]
        iiNaoIncidencia = 2,

        /// <summary>
        /// 3=Isenção
        /// </summary>
        [Description("Isenção")]
        [XmlEnum("3")]
        iiIsencao = 3,

        /// <summary>
        /// 4=Exportação
        /// </summary>
        [Description("Exportação")]
        [XmlEnum("4")]
        iiExportacao = 4,

        /// <summary>
        /// 5=Imunidade
        /// </summary>
        [Description("Imunidade")]
        [XmlEnum("5")]
        iiImunidade = 5,

        /// <summary>
        /// 6=Exigibilidade Suspensa por Decisão Judicial
        /// </summary>
        [Description("Exigibilidade Suspensa por Decisão Judicial")]
        [XmlEnum("6")]
        iiExigSuspDecisaoJudicial = 6,

        /// <summary>
        /// 7=Exigibilidade Suspensa por Processo Administrativo
        /// </summary>
        [Description("Exigibilidade Suspensa por Processo Administrativo")]
        [XmlEnum("7")]
        iiExigSuspProcessoAdm = 7
    }
}