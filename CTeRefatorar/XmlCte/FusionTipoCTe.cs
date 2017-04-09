using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoCTe
    {
        [XmlEnum("0")]
        Normal = 0,

        [XmlEnum("1")]
        ComplementoDeValores = 1,

        [XmlEnum("2")]
        Anulacao = 2,

        [XmlEnum("3")]
        Substituto = 3
    }
}