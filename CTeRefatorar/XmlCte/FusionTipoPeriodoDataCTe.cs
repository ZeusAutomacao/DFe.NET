using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoPeriodoDataCTe
    {
        [XmlEnum("0")]
        SemDataDefinida,

        [XmlEnum("1")]
        NaData,

        [XmlEnum("2")]
        AteAData,

        [XmlEnum("3")]
        APartirDaData,

        [XmlEnum("4")]
        NoPeriodo
    }
}