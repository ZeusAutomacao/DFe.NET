using CTe.CTeOSDocumento.Common;
using DFe.Classes.Extensoes;
using MDFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Wsdl.Gerado.MDFeStatusServico;
using MDFe.Wsdl.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.MDFeConsultaProtoloco;
using MDFe.Wsdl.MDFeEventos;
using MDFe.Wsdl.MDFeRecepcao;
using MDFe.Wsdl.MDFeRecepcao.Sincrono;
using MDFe.Wsdl.MDFeRetRecepcao;

namespace MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var versao = config.VersaoWebService.VersaoLayout.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeConsulta;
            var versao = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            return new MDFeConsulta(configuracaoWsdl);
        }

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeRecepcao;
            var versaoServico = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico, config);

            return new MDFeRecepcao(configuracaoWsdl);
        }

        public static MDFeRecepcaoSinc CriaWsdlMDFeRecepcaoSinc(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeRecepcaoSinc;
            var versaoServico = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico, config);

            return new MDFeRecepcaoSinc(configuracaoWsdl);
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var versao = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico(MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var url = UrlHelper.ObterUrlServico(config.VersaoWebService.TipoAmbiente).MDFeStatusServico;
            var versao = config.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            return new MDFeStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var codigoEstado = config.VersaoWebService.UfEmitente.GetCodigoIbgeEmString();
            var certificadoDigital = config.X509Certificate2;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = config.VersaoWebService.TimeOut
            };
        }
    }
}