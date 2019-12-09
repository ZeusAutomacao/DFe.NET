using System.Security.Cryptography.X509Certificates;

namespace NFeFacade
{
    public interface ICertificadoDigital
    {
        X509Certificate2 ObterCertificado();
    }
}