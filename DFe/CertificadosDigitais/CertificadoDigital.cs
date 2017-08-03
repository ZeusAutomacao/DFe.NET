using System;
using System.Security.Cryptography.X509Certificates;
using DFe.Utils;

namespace DFe.CertificadosDigitais
{
    public abstract class CertificadoDigital
    {
        private readonly IDFeConfigCertificadoDigital _configCertificadoDigital;

        protected CertificadoDigital(IDFeConfigCertificadoDigital configCertificadoDigital)
        {
            _configCertificadoDigital = configCertificadoDigital;
        }

        protected virtual X509Certificate2 ObterCertificadoDigital()
        {
            switch (_configCertificadoDigital.TipoCertificado)
            {
                case TipoCertificado.A1Arquivo:
                    return _configCertificadoDigital.ProxyCertificadoDigitalA1Arquivo.Obter(_configCertificadoDigital);
                case TipoCertificado.A1Repositorio:
                    return _configCertificadoDigital.ProxyCertificadoDigitalA1Repositorio.Obter(_configCertificadoDigital);
                case TipoCertificado.A3:
                    return _configCertificadoDigital.ProxyCertificadoDigitalA3.Obter(_configCertificadoDigital);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}