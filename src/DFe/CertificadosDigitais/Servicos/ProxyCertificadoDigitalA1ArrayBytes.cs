using System;
using System.Security.Cryptography.X509Certificates;

namespace DFe.CertificadosDigitais.Servicos
{
    public class ProxyCertificadoDigitalA1ArrayBytes : ProxyBase, IProxyCertificadoDigitalA1ArrayBytes
    {
        public X509Certificate2 Obter(IDFeConfigCertificadoDigital configCertificadoDigital)
        {
            var certificadoEmBytes = configCertificadoDigital.ArrayBytesArquivo;
            var senha = configCertificadoDigital.Senha;

            if (certificadoEmBytes == null)
                throw new ArgumentException("Adicionar certificado digital!");

            var certificado = new X509Certificate2(certificadoEmBytes, senha, X509KeyStorageFlags.MachineKeySet);
            return certificado;
        }
    }
}