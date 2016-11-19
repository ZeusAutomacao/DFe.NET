using DFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Wsdl.Configuracao;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;

namespace MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;

            var configuracaoWsdl = CriaConfiguracao(url);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        private static WsdlConfiguracao CriaConfiguracao(string url)
        {
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = MDFeConfiguracao.VersaoWebService.TimeOut
            };
        }
    }
}