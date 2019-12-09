namespace DFeFacadeBase
{
    public class CertificadoDigitalA1ByteArray
    {
        public byte[] CertificadoBytes { get; }
        public string Senha { get; }

        public CertificadoDigitalA1ByteArray(byte[] certificadoBytes, string senha)
        {
            CertificadoBytes = certificadoBytes;
            Senha = senha;
        }
    }
}