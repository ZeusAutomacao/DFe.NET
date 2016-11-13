using System;

namespace MDFe.AppTeste.Entidades
{
    [Serializable]
    public class Configuracao
    {
        public Empresa Empresa { get; set; }
        public CertificadoDigital CertificadoDigital { get; set; }
        public ConfigWebService ConfigWebService { get; set; }
    }
}