using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Destinatario
{
    /// <summary>
    ///     <para>1 – Contribuinte ICMS;</para>
    ///     <para>2 – Contribuinte isento de inscrição;</para>
    ///     <para>9 – Não Contribuinte</para>
    /// </summary>
    public enum indIEDest
    {
        /// <summary>
        /// 1 – Contribuinte ICMS
        /// </summary>
        [Description("Contribuinte ICMS")]
        [XmlEnum("1")]
        ContribuinteICMS = 1,

        /// <summary>
        /// 2 – Contribuinte isento de inscrição
        /// </summary>
        [Description("Contribuinte isento de inscrição")]
        [XmlEnum("2")]
        Isento = 2,

        /// <summary>
        /// 9 – Não Contribuinte
        /// </summary>
        [Description("Não Contribuinte")]
        [XmlEnum("9")]
        NaoContribuinte = 9
    }
}