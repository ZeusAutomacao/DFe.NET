using System.ComponentModel;
using System.Xml.Serialization;

namespace DFe.Classes.Flags
{
    /// <summary>
    ///     Identificação do Ambiente
    ///     <para>1 - Produção</para>
    ///     <para>2 - Homologação</para>
    /// </summary>
    public enum TipoAmbiente
    {
        [XmlEnum("1")]
        [Description("Produção")]
        Producao = 1,

        [XmlEnum("2")]
        [Description("Homologação")]
        Homologacao = 2
    }
}