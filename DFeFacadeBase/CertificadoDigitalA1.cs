namespace DFeFacadeBase
{
    public class CertificadoDigitalA1 : ICertificadoDigital
    {
        public string LocalArquivoPrfx { get; }
        public string Senha { get; }

        public CertificadoDigitalA1(string localArquivoPfx, string senha)
        {
            LocalArquivoPrfx = localArquivoPfx;
            Senha = senha;
            TipoCertificado = DFeTipoCertificado.A1;
        }

        public DFeTipoCertificado TipoCertificado { get; }
        public bool ManterEmCache { get; set; }
        public string CacheId { get; set; }
    }
}