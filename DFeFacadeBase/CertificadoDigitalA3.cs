namespace DFeFacadeBase
{
    public class CertificadoDigitalA3 : ICertificadoDigital
    {
        public string Serial { get; }
        public string Senha { get; }

        public CertificadoDigitalA3(string serial, string senha)
        {
            Serial = serial;
            Senha = senha;
            TipoCertificado = DFeTipoCertificado.A3;
        }

        public DFeTipoCertificado TipoCertificado { get; }
        public bool ManterEmCache { get; set; }
        public string CacheId { get; set; }
    }
}