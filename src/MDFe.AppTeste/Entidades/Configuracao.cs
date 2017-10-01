using System;

namespace MDFe.AppTeste.Entidades
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