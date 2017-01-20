using System.Security.Cryptography.X509Certificates;

namespace DFe.Utils.Assinatura.CertificadoProvider.Contratos
{
    public interface ICertificadoProvider
    {
        X509Certificate2 Provider(string numeroSerial, string senha = null);
    }
}