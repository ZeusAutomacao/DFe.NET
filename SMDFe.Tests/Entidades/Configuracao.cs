using System;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using SMDFe.Utils.Flags;

namespace SMDFe.Tests.Entidades
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