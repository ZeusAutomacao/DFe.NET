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
        }
    }
}