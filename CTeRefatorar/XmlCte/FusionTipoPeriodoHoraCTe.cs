using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoPeriodoHoraCTe
    {
        [XmlEnum("0")]
        SemHoraDefinida,

        [XmlEnum("1")]
        NoHorario,

        [XmlEnum("2")]
        AteOHorario,

        [XmlEnum("3")]
        APartirDoHorario,

        [XmlEnum("4")]
        NoIntervaloDeTempo
    }
}