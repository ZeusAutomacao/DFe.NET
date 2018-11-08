using System;

namespace SMDFe.Tests.Entidades
{
    [Serializable]
    public class ConfigCertificadoDigital
    {
        public string NumeroDeSerie { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Senha { get; set; }
        public bool ManterEmCache { get; set; }
    }
}