using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionFormaPagamentoCTe
    {
        [XmlEnum("0")]
        Pago = 0,

        [XmlEnum("1")]
        Apagar = 1,

        [XmlEnum("2")]
        Outros = 2
    }
}