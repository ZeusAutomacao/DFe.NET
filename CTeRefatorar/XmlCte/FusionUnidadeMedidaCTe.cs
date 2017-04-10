using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionUnidadeMedidaCTe
    {
        [XmlEnum("00")]
        M3,

        [XmlEnum("01")]
        Kg,

        [XmlEnum("02")]
        Ton,

        [XmlEnum("03")]
        Unidade,

        [XmlEnum("04")]
        Litros,

        [XmlEnum("05")]
        Mmbtu
    }
}