using System;

namespace SMDFe.TestesServicos
{
    [Serializable]
    public class Configuracao
    {
        public Configuracao()
        {
            Empresa = new Empresa();
            CertificadoDigital = new ConfigCertificadoDigital();
            ConfigWebService = new ConfigWebService();
        }
        public Empresa Empresa { get; set; }
        public ConfigCertificadoDigital CertificadoDigital { get; set; }
        public ConfigWebService ConfigWebService { get; set; }

        public string DiretorioSalvarXml { get; set; }
        public bool IsSalvarXml { get; set; }
    }
}