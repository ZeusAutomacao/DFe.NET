using CTe.Classes;
using CTe.Classes.Ext;
using CTe.Servicos.Enderecos.Helpers;
using CTe.Wsdl.ConsultaProtocolo;
using CTe.Wsdl.DistribuicaoDFe;
using CTe.Wsdl.Evento;
using CTe.Wsdl.Inutilizacao;
using CTe.Wsdl.Recepcao;
using CTe.Wsdl.RetRecepcao;
using CTe.Wsdl.Status;
using DFe.Classes.Extensoes;
using CTe.CTeOSDocumento.Common;
using CTe.Wsdl.ConsultaProtocolo.V4;
using CTe.Wsdl.Evento.V4;
using CTe.Wsdl.Recepcao.Sincrono;
using System.Security.Cryptography.X509Certificates;

namespace CTe.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteStatusServico;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteStatusServico(configuracaoWsdl);
        }

        public static CteConsulta CriaWsdlConsultaProtocolo(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteConsulta(configuracaoWsdl);
        }

        public static CteConsultaV4 CriaWsdlConsultaProtocoloV4(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteConsultaV4(configuracaoWsdl);
        }

        public static CteInutilizacao CriaWsdlCteInutilizacao(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteInutilizacao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteInutilizacao(configuracaoWsdl);
        }

        public static CteRetRecepcao CriaWsdlCteRetRecepcao(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRetRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteRetRecepcao(configuracaoWsdl);
        }

        public static CteRecepcao CriaWsdlCteRecepcao(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteRecepcao(configuracaoWsdl);
        }

        public static CteRecepcaoSincronoV4 CriaWsdlCteRecepcaoSincronoV4(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcaoSinc;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteRecepcaoSincronoV4(configuracaoWsdl);
        }
        public static CteRecepcaoSincronoOSV4 CriaWsdlCteRecepcaoSincronoOSV4(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcaoOs;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteRecepcaoSincronoOSV4(configuracaoWsdl);
        }

        public static CteRecepcaoEvento CriaWsdlCteEvento(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcaoEvento;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CteRecepcaoEvento(configuracaoWsdl);
        }

        public static CteRecepcaoEventoV4 CriaWsdlCteEventoV4(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var url = UrlHelper.ObterUrlServico(configServico).CteRecepcaoEvento;

            var configuracaoWsdl = CriaConfiguracao(url, configServico, certificado);

            return new CteRecepcaoEventoV4(configuracaoWsdl);
        }


        public static CTeDistDFeInteresse CriaWsdlCTeDistDFeInteresse(ConfiguracaoServico configuracaoServico = null, X509Certificate2 certificado = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CTeDistribuicaoDFe;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico, certificado);

            return new CTeDistDFeInteresse(configuracaoWsdl);
        }


        private static WsdlConfiguracao CriaConfiguracao(string url, ConfiguracaoServico configuracaoServico, X509Certificate2 certificado)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var codigoEstado = configServico.cUF.GetCodigoIbgeEmString();
            var certificadoDigital = certificado ?? configServico.X509Certificate2;
            var versaoEmString = configServico.VersaoLayout.GetString();
            var timeOut = configServico.TimeOut;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versaoEmString,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = timeOut
            };
        }
    }
}