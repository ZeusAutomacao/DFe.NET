using System.ComponentModel;

namespace DFe.Utils.Flags
{
    public enum TipoCertificado
    {
        [Description("Certificado A1")]
        A1Repositorio,

        [Description("Certificado A1 em arquivo")]
        A1Arquivo,

        [Description("Certificado A3")]
        A3
    }
}