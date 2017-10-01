using System;
using System.Security.Cryptography.X509Certificates;

namespace DFe.CertificadosDigitais.Servicos
{
    public class ProxyCertificadoDigitalA1Repositorio : ProxyBase, IProxyCertificadoDigitalA1Repositorio
    {
        public X509Certificate2 Obter(IDFeConfigCertificadoDigital configCertificadoDigital)
        {
            return ObterDoRepositorio(configCertificadoDigital.Serial, OpenFlags.MaxAllowed);
        }
    }
}