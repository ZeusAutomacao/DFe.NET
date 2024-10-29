using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
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
}