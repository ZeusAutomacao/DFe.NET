using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionResponsavelSeguroCTe
    {
        [XmlEnum("0")]
        Remetente,

        [XmlEnum("1")]
        Expedidor,

        [XmlEnum("2")]
        Recebedor,

        [XmlEnum("3")]
        Destinatario,

        [XmlEnum("4")]
        Emitente,

        [XmlEnum("5")]
        Tomador
    }
}