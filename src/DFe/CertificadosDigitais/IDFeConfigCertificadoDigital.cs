using DFe.Utils.Flags;

namespace DFe.CertificadosDigitais
{
    public interface IDFeConfigCertificadoDigital
    {
        TipoCertificado TipoCertificado { get; set; }
        string Serial { get; set; }
        byte[] ArrayBytesArquivo { get; set; }
        string LocalArquivo { get; set; }
        string Senha { get; set; }

        IProxyCertificadoDigitalA1Arquivo ProxyCertificadoDigitalA1Arquivo { get; set; }
        IProxyCertificadoDigitalA1ArrayBytes ProxyCertificadoDigitalA1ArrayBytes { get; set; }
        IProxyCertificadoDigitalA1Repositorio ProxyCertificadoDigitalA1Repositorio { get; set; }
        IProxyCertificadoDigitalA3 ProxyCertificadoDigitalA3 { get; set; }
    }
}