using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Transporte
{
    /// <summary>
    ///     Modalidade do frete
    ///     <para>0 - Contratação do Frete por conta do Remetente (CIF);</para>
    ///     <para>1 - Contratação do Frete por conta do Destinatário(FOB);</para>
    ///     <para>2 - Contratação do Frete por conta de Terceiros;</para>
    ///     <para>3 - Transporte Próprio por conta do Remetente;</para>
    ///     <para>4 - Transporte Próprio por conta do Destinatário;</para>
    ///     <para>9 - Sem Ocorrência de Transporte.</para>
    /// </summary>
    public enum ModalidadeFrete
    {
        /// <summary>
        /// 0 - Contratação do Frete por conta do Remetente (CIF)
        /// </summary>
        [Description("Contratação do Frete por conta do Remetente (CIF)")]
        [XmlEnum("0")]
        mfContaEmitenteOumfContaRemetente = 0,

        /// <summary>
        /// 1 - Contratação do Frete por conta do Destinatário(FOB)
        /// </summary>
        [Description("Contratação do Frete por conta do Destinatário(FOB)")]
        [XmlEnum("1")]
        mfContaDestinatario = 1,

        /// <summary>
        /// 2 - Contratação do Frete por conta de Terceiros
        /// </summary>
        [Description("Contratação do Frete por conta de Terceiros")]
        [XmlEnum("2")]
        mfContaTerceiros = 2,

        /// <summary>
        /// 3 - Transporte Próprio por conta do Remetente
        /// </summary>
        [Description("Transporte Próprio por conta do Remetente")]
        [XmlEnum("3")]
        mfProprioContaRemente = 3,

        /// <summary>
        /// 4 - Transporte Próprio por conta do Destinatário
        /// </summary>
        [Description("Transporte Próprio por conta do Destinatário")]
        [XmlEnum("4")]
        mfProprioContaDestinatario = 4,

        /// <summary>
        /// 9 - Sem Ocorrência de Transporte
        /// </summary>
        [Description("Sem Ocorrência de Transporte")]
        [XmlEnum("9")]
        mfSemFrete = 9
    }
}