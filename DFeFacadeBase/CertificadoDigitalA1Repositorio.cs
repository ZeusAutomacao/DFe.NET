namespace DFeFacadeBase
{
    public class CertificadoDigitalA1Repositorio : ICertificadoDigital
    {
        public string Serial { get; }

        public CertificadoDigitalA1Repositorio(string serial)
        {
            Serial = serial;
            TipoCertificado = DFeTipoCertificado.A1Repositorio;
        }

        public DFeTipoCertificado TipoCertificado { get; }
        public bool ManterEmCache { get; set; }
        public string CacheId { get; set; }
    }
}