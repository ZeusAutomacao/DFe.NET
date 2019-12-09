namespace DFeFacadeBase
{
    public class CertificadoDigitalA1ByteArray : ICertificadoDigital
    {
        public byte[] CertificadoBytes { get; }
        public string Senha { get; }

        public CertificadoDigitalA1ByteArray(byte[] certificadoBytes, string senha)
        {
            CertificadoBytes = certificadoBytes;
            Senha = senha;
            TipoCertificado = DFeTipoCertificado.A1Byte;
        }

        public DFeTipoCertificado TipoCertificado { get; }
        public bool ManterEmCache { get; set; }
        public string CacheId { get; set; }
    }
}