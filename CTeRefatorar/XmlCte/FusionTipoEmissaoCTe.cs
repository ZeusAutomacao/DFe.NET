using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoEmissaoCTe
    {
        [XmlEnum("1")]
        Normal = 1,

        [XmlEnum("4")]
        EpecPelaSvc = 4,

        [XmlEnum("5")]
        ContingenciaFsda = 5,

        [XmlEnum("7")]
        AutorizacaoSvcRs = 7,

        [XmlEnum("8")]
        AutorizacaoSvcSp = 8
    }
}