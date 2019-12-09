namespace DFeFacadeBase
{
    public class CertificadoDigitalA1Repositorio : ICertificadoDigital
    {
        public string Serial { get; }

        public CertificadoDigitalA1Repositorio(string serial)
        {
            Serial = serial;
        }
    }
}