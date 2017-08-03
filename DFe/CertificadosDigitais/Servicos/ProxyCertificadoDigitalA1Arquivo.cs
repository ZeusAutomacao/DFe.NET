using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace DFe.CertificadosDigitais.Servicos
{
    public class ProxyCertificadoDigitalA1Arquivo : IProxyCertificadoDigitalA1Arquivo
    {
        public X509Certificate2 Obter(IDFeConfigCertificadoDigital configCertificadoDigital)
        {
            var arquivo = configCertificadoDigital.LocalArquivo;
            var senha = configCertificadoDigital.Senha;

            if (string.IsNullOrEmpty(arquivo))
                throw new ArgumentException("Adicionar LocalArquivo do certificado digital!");

            if (!File.Exists(arquivo))
            {
                throw new FileNotFoundException(String.Format("Certificado digital {0} não encontrado!", arquivo));
            }

            var certificado = new X509Certificate2(arquivo, senha, X509KeyStorageFlags.MachineKeySet);
            return certificado;
        }
    }
}