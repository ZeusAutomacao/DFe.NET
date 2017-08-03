using System.Security.Cryptography.X509Certificates;
using DFe.CertificadosDigitais.Ext;

namespace DFe.CertificadosDigitais.Servicos
{
    public class ProxyCertificadoDigitalA3 : ProxyBase, IProxyCertificadoDigitalA3
    {
        public X509Certificate2 Obter(IDFeConfigCertificadoDigital configCertificadoDigital)
        {
            var certificado = ObterDoRepositorio(configCertificadoDigital.Serial, OpenFlags.ReadOnly);

            var senha = configCertificadoDigital.Senha;

            if (string.IsNullOrEmpty(senha)) return certificado;

            certificado.DefinirPinParaChavePrivada(senha);

            return certificado;
        }
    }
}