using System;
using System.Xml.Serialization;

namespace SMDFe.TestesServicos.Entidades
{
    [Serializable]
    [XmlRoot(ElementName = "Configuracao")]
    public class ConfiguracaoMdfe
    {
        public ConfiguracaoMdfe()
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