using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Agropecuario
{
    /// <summary>
    ///     Tipo da Guia
    ///     <para>1 - GTA - Guia de Trânsito Animal</para>
    ///     <para>2 - TTA - Termo de Trânsito Animal</para>
    ///     <para>3 - DTA - Documento de Transferência Animal</para>
    ///     <para>4 - ATV - Autorização de Trânsito Vegetal</para>
    ///     <para>5 - PTV - Permissão de Trânsito Vegetal</para>
    ///     <para>6 - GTV - Guia de Trânsito Vegetal</para>
    ///     <para>7 - Guia Florestal (DOF, SisFlora - PA e MT ou SIAM - MG)</para>
    /// </summary>
    public enum TipoGuia
    {
        /// <summary>
        /// 1 - GTA - Guia de Trânsito Animal
        /// </summary>
        [Description("GTA - Guia de Trânsito Animal")]
        [XmlEnum("1")]
        GTA = 1,

        /// <summary>
        /// 2 - TTA - Termo de Trânsito Animal
        /// </summary>
        [Description("TTA - Termo de Trânsito Animal")]
        [XmlEnum("2")]
        TTA = 2,

        /// <summary>
        /// 3 - DTA - Documento de Transferência Animal
        /// </summary>
        [Description("DTA - Documento de Transferência Animal")]
        [XmlEnum("3")]
        DTA = 3,

        /// <summary>
        /// 4 - ATV - Autorização de Trânsito Vegetal
        /// </summary>
        [Description("ATV - Autorização de Trânsito Vegetal")]
        [XmlEnum("4")]
        ATV = 4,

        /// <summary>
        /// 5 - PTV - Permissão de Trânsito Vegetal
        /// </summary>
        [Description("PTV - Permissão de Trânsito Vegetal")]
        [XmlEnum("5")]
        PTV = 5,

        /// <summary>
        /// 6 - GTV - Guia de Trânsito Vegetal
        /// </summary>
        [Description("GTV - Guia de Trânsito Vegetal")]
        [XmlEnum("6")]
        GTV = 6,

        /// <summary>
        /// 7 - Guia Florestal (DOF, SisFlora - PA e MT ou SIAM - MG)
        /// </summary>
        [Description("Guia Florestal (DOF, SisFlora - PA e MT ou SIAM - MG)")]
        [XmlEnum("7")]
        GuiaFlorestal = 7,
    }
}
