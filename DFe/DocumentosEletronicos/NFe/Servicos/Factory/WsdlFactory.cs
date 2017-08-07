using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.NFe.Classes.Servicos.Status;
using DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public class WsdlFactory
    {
        public consStatServ CriaConsStatServ(DFeConfig config)
        {
            var consStatServ = new consStatServ
            {
                versao = config.VersaoServico.GetVersaoString(),
                tpAmb = config.TipoAmbiente,

            };

            return consStatServ;
        }


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