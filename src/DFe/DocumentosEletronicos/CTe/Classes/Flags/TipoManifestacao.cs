using System.ComponentModel;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.CTe.Classes.Flags
{
    public enum TipoManifestacao
    {
        [Description("Confirmacao da Operacao")]
        [XmlEnum("Confirmacao da Operacao")]
        e210200,

        [Description("Ciencia da Operacao")]
        [XmlEnum("Ciencia da Operacao")]
        e210210,

        [Description("Desconhecimento da Operacao")]
        [XmlEnum("Desconhecimento da Operacao")]
        e210220,

        [Description("Operacao nao Realizada")]
        [XmlEnum("Operacao nao Realizada")]
        e210240
    }
}