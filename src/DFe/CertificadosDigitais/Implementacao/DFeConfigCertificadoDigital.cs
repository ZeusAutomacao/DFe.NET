using DFe.CertificadosDigitais.Servicos;
using DFe.Utils.Flags;

namespace DFe.CertificadosDigitais.Implementacao
{
    public class DFeConfigCertificadoDigital : IDFeConfigCertificadoDigital
    {
        public DFeConfigCertificadoDigital()
        {
            ProxyCertificadoDigitalA1Arquivo = new ProxyCertificadoDigitalA1Arquivo();
            ProxyCertificadoDigitalA1Repositorio = new ProxyCertificadoDigitalA1Repositorio();
            ProxyCertificadoDigitalA3 = new ProxyCertificadoDigitalA3();
        }

        public TipoCertificado TipoCertificado { get; set; }
        public string Serial { get; set; }
        public string LocalArquivo { get; set; }
        public string Senha { get; set; }

        public IProxyCertificadoDigitalA1Arquivo ProxyCertificadoDigitalA1Arquivo { get; set; }
        public IProxyCertificadoDigitalA1Repositorio ProxyCertificadoDigitalA1Repositorio { get; set; }
        public IProxyCertificadoDigitalA3 ProxyCertificadoDigitalA3 { get; set; }
    }
}