using System.Security.Cryptography.X509Certificates;

namespace NFeFacade
{
    public class CertificadoDigitalA1 : ICertificadoDigital
    {
        private readonly string _localArquivoPfx;
        private readonly string _senha;

        public CertificadoDigitalA1(string localArquivoPfx, string senha)
        {
            _localArquivoPfx = localArquivoPfx;
            _senha = senha;
        }

        public X509Certificate2 ObterCertificado()
        {
            if (string.IsNullOrEmpty(_localArquivoPfx))
            {
                throw new CertificadoDigitalException("Adicionar um arquivo PFX do certificado digital");
            }

            if (string.IsNullOrEmpty(_senha))
            {
                throw new CertificadoDigitalException("Adicionar uma senha para o certificado digital");
            }

            return new X509Certificate2(_localArquivoPfx, _senha);
        }
    }
}