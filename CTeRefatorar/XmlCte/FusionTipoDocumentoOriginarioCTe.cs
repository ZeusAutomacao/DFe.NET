using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    public enum FusionTipoDocumentoOriginarioCTe
    {
        [XmlEnum("00")]
        Declaracao,

        [XmlEnum("10")]
        Dutoviario,

        [XmlEnum("99")]
        Outros
    }
}