using System.Security.Cryptography.X509Certificates;
using DFe.Configuracao;

namespace DFe.CertificadosDigitais
{
    public interface IProxyCertificadoDigitalA1Arquivo
    {
        X509Certificate2 Obter(IDFeConfigCertificadoDigital configCertificadoDigital);
    }
}