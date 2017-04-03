using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoTomadorCTe
    {
        [XmlEnum("0")]
        Remetente = 0,

        [XmlEnum("1")]
        Expedidor = 1,

        [XmlEnum("2")]
        Recebedor = 2,

        [XmlEnum("3")]
        Destinatario = 3,

        [XmlEnum("4")]
        Outro = 4
    }
}