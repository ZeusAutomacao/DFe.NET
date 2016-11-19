using DFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Wsdl.Configuracao;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;

namespace MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsulta;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsulta.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeConsulta(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao)
        {
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
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