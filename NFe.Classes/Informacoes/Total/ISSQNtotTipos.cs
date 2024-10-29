using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    /// <summary>
    ///     Código do Regime Especial de Tributação
    ///     <para>1 - Microempresa Municipal;</para>
    ///     <para>2 - Estimativa;</para>
    ///     <para>3 - Sociedade de Profissionais;</para>
    ///     <para>4 - Cooperativa;</para>
    ///     <para>5 - Microempresário Individual (MEI);</para>
    ///     <para>6 - Microempresário e Empresa de Pequeno Porte (ME/EPP)</para>
    /// </summary>
    public enum RegTribISSQN
    {
        /// <summary>
        /// 1 - Microempresa Municipal
        /// </summary>
        [Description("Microempresa Municipal")]
        [XmlEnum("1")]
        TISSMicroempresaMunicipal = 1,

        /// <summary>
        /// 2 - Estimativa
        /// </summary>
        [Description("Estimativa")]
        [XmlEnum("2")]
        RTISSEstimativa = 2,

        /// <summary>
        /// 3 - Sociedade de Profissionais
        /// </summary>
        [Description("Sociedade de Profissionais")]
        [XmlEnum("3")]
        RTISSSociedadeProfissionais = 3,

        /// <summary>
        /// 4 - Cooperativa
        /// </summary>
        [Description("Cooperativa")]
        [XmlEnum("4")]
        RTISSCooperativa = 4,

        /// <summary>
        /// 5 - Microempresário Individual (MEI)
        /// </summary>
        [Description("Microempresário Individual (MEI)")]
        [XmlEnum("5")]
        RTISSMEI = 5,

        /// <summary>
        /// 6 - Microempresário e Empresa de Pequeno Porte (ME/EPP)
        /// </summary>
        [Description("Microempresário e Empresa de Pequeno Porte (ME/EPP)")]
        [XmlEnum("6")]
        RTISSMEEPP = 6
    }
}