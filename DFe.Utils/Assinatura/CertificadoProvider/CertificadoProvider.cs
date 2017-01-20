using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using DFe.Utils.Assinatura.CertificadoProvider.Contratos;

namespace DFe.Utils.Assinatura.CertificadoProvider
{
    public class CertificadoProvider : ICertificadoProvider
    {
        public X509Certificate2 Provider(string numeroSerial, string senha = null)
        {
            var certificado = X509StoreHelper.ObterPeloSerial(numeroSerial, OpenFlags.MaxAllowed);

            if (string.IsNullOrEmpty(senha)) return certificado;

            //Se a senha for passada no parâmetro
            var senhaSegura = new SecureString();
            var passPhrase = senha.ToCharArray();
            foreach (var t in passPhrase)
            {
                senhaSegura.AppendChar(t);
            }

            var chavePrivada = certificado.PrivateKey as RSACryptoServiceProvider;
            if (chavePrivada == null) return certificado;

            var cspParameters = new CspParameters(chavePrivada.CspKeyContainerInfo.ProviderType,
                chavePrivada.CspKeyContainerInfo.ProviderName,
                chavePrivada.CspKeyContainerInfo.KeyContainerName,
                null,
                senhaSegura);
            var rsaCsp = new RSACryptoServiceProvider(cspParameters);
            certificado.PrivateKey = rsaCsp;

            return certificado;
        }
    }
}