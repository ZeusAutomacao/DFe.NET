using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Emitente
{
    /// <summary>
    ///     <para>1 – Simples Nacional;</para>
    ///     <para>2 – Simples Nacional – excesso de sublimite de receita bruta;</para>
    ///     <para>3 – Regime Normal. (v2.0).</para>
    /// </summary>
    public enum CRT
    {
        /// <summary>
        /// 1 – Simples Nacional
        /// </summary>
        [Description("Simples Nacional")]
        [XmlEnum("1")]
        SimplesNacional = 1,

        /// <summary>
        /// 2 – Simples Nacional – excesso de sublimite de receita bruta
        /// </summary>
        [Description("Simples Nacional – excesso de sublimite de receita bruta")]
        [XmlEnum("2")]
        SimplesNacionalExcessoSublimite = 2,

        /// <summary>
        /// 3 – Regime Normal
        /// </summary>
        [Description("Regime Normal")]
        [XmlEnum("3")]
        RegimeNormal = 3,

        [Description("Simples Nacional MEI")]
        [XmlEnum("4")]
        SimplesNacionalMei = 4,
    }
}