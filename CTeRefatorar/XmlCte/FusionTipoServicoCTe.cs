using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoServicoCTe
    {
        [XmlEnum("0")]
        Normal = 0,

        [XmlEnum("1")]
        Subcontratacao = 1,

        [XmlEnum("2")]
        Redespacho = 2,

        [XmlEnum("3")]
        RedespachoIntermediario = 3,

        [XmlEnum("4")]
        ServicoVinculadoAMultimodal = 4
    }
}