using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Informações relativas ao ICMS
    ///     <para>00 - Tributação normal ICMS;</para>
    ///     <para>20 - Tributação com BC reduzida do ICMS;</para>
    ///     <para>40 - ICMS isenção;</para>
    ///     <para>41 - ICMS não tributada;</para>
    ///     <para>51 - ICMS diferido;</para>
    ///     <para>60 - ICMS cobrado por substituição tributária;</para>
    ///     <para>90 - Outros</para>
    /// </summary>
    public enum CST
    {
        /// <summary>
        /// 00 - Tributação normal ICMS
        /// </summary>
        [Description("Tributação normal ICMS")]
        [XmlEnum("00")]
        ICMS00 = 00,

        /// <summary>
        /// 20 - Tributação com BC reduzida do ICMS
        /// </summary>
        [Description("Tributação com BC reduzida do ICMS")]
        [XmlEnum("20")]
        ICMS20 = 20,

        /// <summary>
        /// 40 - ICMS isenção
        /// </summary>
        [Description("ICMS isenção")]
        [XmlEnum("40")]
        ICMS40,

        /// <summary>
        /// 41 - ICMS não tributada
        /// </summary>
        [Description("ICMS não tributada")]
        [XmlEnum("41")]
        ICMS41,

        /// <summary>
        /// 51 - ICMS diferido
        /// </summary>
        [Description("ICMS diferido")]
        [XmlEnum("51")]
        ICMS51,

        /// <summary>
        /// 60 - ICMS cobrado por substituição tributária
        /// </summary>
        [Description("ICMS cobrado por substituição tributária")]
        [XmlEnum("60")]
        ICMS60,

        /// <summary>
        /// 90 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("90")]
        ICMS90
    }
}