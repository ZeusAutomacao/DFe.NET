using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public class WsdlFactory
    {
        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital.ObterCertificadoDigital(),
                Versao = versao,
                Url = url,
                TimeOut = dfeConfig.TimeOut
            };
        }
    }
}