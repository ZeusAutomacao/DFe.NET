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
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsulta;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeConsulta(configuracaoWsdl);
        }

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcao;
            var versaoServico = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico);

            return new MDFeRecepcao(configuracaoWsdl);
        }

        public static MDFeRecepcaoSinc CriaWsdlMDFeRecepcaoSinc()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcaoSinc;
            var versaoServico = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico);

            return new MDFeRecepcaoSinc(configuracaoWsdl);
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeStatusServico;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao)
        {
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfEmitente.GetCodigoIbgeEmString();
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