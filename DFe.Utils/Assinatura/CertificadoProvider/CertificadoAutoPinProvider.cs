using System.Security.Cryptography.X509Certificates;
using DFe.Utils.Assinatura.CertificadoProvider.Contratos;
using DFe.Utils.Assinatura.CertificadoProvider.Extencoes;

namespace DFe.Utils.Assinatura.CertificadoProvider
{
    public class CertificadoAutoPinProvider : ICertificadoProvider
    {
        public X509Certificate2 Provider(string numeroSerial, string senha = null)
        {
            var certificado = X509StoreHelper.ObterPeloSerial(numeroSerial, OpenFlags.ReadOnly);

            if (string.IsNullOrEmpty(senha)) return certificado;

            certificado.SetPinForPrivateKey(senha);

            return certificado;
        }
    }
}