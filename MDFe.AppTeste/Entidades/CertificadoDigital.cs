using System;

namespace MDFe.AppTeste.Entidades
{
    [Serializable]
    public class CertificadoDigital
    {
        public string NumeroDeSerie { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Senha { get; set; } 
    }
}