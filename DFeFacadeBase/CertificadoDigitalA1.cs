namespace DFeFacadeBase
{
    public class CertificadoDigitalA1 : ICertificadoDigital
    {
        private string LocalArquivoPrfx { get; }
        private string Senha { get; }

        public CertificadoDigitalA1(string localArquivoPfx, string senha)
        {
            LocalArquivoPrfx = localArquivoPfx;
            Senha = senha;
        }
    }
}