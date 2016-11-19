using DFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Wsdl.Configuracao;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;
using MDFe.Wsdl.Gerado.MDFeRecepcao;
using MDFe.Wsdl.Gerado.MDFeRetRecepcao;
using MDFe.Wsdl.Gerado.MDFeStatusServico;

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

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRecepcaoEvento.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcao;
            var versaoServico = MDFeConfiguracao.VersaoWebService.VersaoMDFeRecepcao.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico);

            return new MDFeRecepcao(configuracaoWsdl); ;
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRetRecepcao.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeStatusServico;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeStatusServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeStatusServico(configuracaoWsdl);
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